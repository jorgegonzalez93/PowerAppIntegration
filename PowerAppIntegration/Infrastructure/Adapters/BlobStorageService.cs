using Azure.Storage.Blobs;
using Migration.Domain.Domain.DTOs;

namespace Migration.Domain.Infrastructure.Adapters
{
    public class BlobStorageService
    {

        private readonly BlobServiceClient _BlobServiceClient;
        public BlobStorageService()
        {

        }

        public async Task<BlobResponse> SaveFileAsync(SaveFileDto saveFile)
        {
            var responseBlob = new BlobResponse();
            try
            {
                var blobContainer = _BlobServiceClient.GetBlobContainerClient(saveFile.ContainerName);
                var blobClient = blobContainer.GetBlobClient(string.IsNullOrWhiteSpace(saveFile.FolderName) ? saveFile.FileName : $"{saveFile.FolderName}/{saveFile.FileName}");

                await blobClient.UploadAsync(saveFile.FileData).ConfigureAwait(false);

                responseBlob.AbsolutePath = blobClient.Uri.AbsolutePath;
            }
            catch (Exception ex)
            {
                responseBlob.ExceptionError = ex.ToString();
            }

            return responseBlob;
        }

        public class BlobResponse
        {
            public string AbsolutePath { get; set; }
            public string ExceptionError { get; set; } = string.Empty;
        }

    }
}
