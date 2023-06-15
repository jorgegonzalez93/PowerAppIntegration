using Migration.Domain.Domain.Enums;
using Migration.Domain.Domain.Helpers;
using Migration.Domain.Infrastructure.Logs;
using System.Linq;
using static PowerAppIntegration.PowerAppForm;

namespace Migration.Domain.Domain.Services.FinalModelService
{
    public class DocumentModelService
    {
        private readonly ReportLogAsyncDelegate reportLogAsync;

        public DocumentModelService(ReportLogAsyncDelegate reportLogAsync)
        {
            this.reportLogAsync = reportLogAsync;
        }

        public async Task<IEnumerable<EmailDocuments>> GetAllEmailFolders()
        {
            var stringPath = GeneralData.DOCUMENTS_PATH;

            stringPath = "C:\\Users\\jorge.gonzalez\\Desktop\\SoportesReales\\Laboratorio";
            var emailFolders = Directory.GetDirectories(stringPath);

            List<EmailDocuments> lstEmailDocs = new();

            foreach (string emailFolder in emailFolders)
            {
                var userEmail = Path.GetFileName(emailFolder);

                var personTypeFolderName = string.Empty;
                var userRolFolderName = string.Empty;

                List<string> personTypeFolder = Directory.GetDirectories(emailFolder).ToList();

                if (!personTypeFolder.Any())
                {
                    var messageLog = $"Usuario con correo: {userEmail} sin soportes";

                    await reportLogAsync(messageLog, Color.Red);
                    ApplicationLogService.GenerateLogByMessage(userEmail, messageLog, "email");
                    continue;
                }

                personTypeFolderName = Path.GetFileName(personTypeFolder.FirstOrDefault()!);

                List<string> folderFilesUserRol = Directory.GetDirectories(personTypeFolder.FirstOrDefault()!).ToList();


                if (!folderFilesUserRol.Any())
                {
                    var messageLog = $"Usuario con correo: {userEmail} sin soportes";

                    await reportLogAsync(messageLog, Color.Red);
                    ApplicationLogService.GenerateLogByMessage(userEmail, messageLog, "email");
                    continue;
                }

                userRolFolderName = Path.GetFileName(folderFilesUserRol.FirstOrDefault()!);


                List<string> folderFilesDocuments = Directory.GetDirectories(folderFilesUserRol.FirstOrDefault()!).ToList();
                if (!folderFilesDocuments.Any())
                {
                    var messageLog = $"Usuario con correo: {userEmail} sin soportes dentro de la carpeta de rol";

                    await reportLogAsync(messageLog, Color.Red);
                    ApplicationLogService.GenerateLogByMessage(userEmail, messageLog, "email");
                    continue;
                }

                EmailDocuments newEmailDocuments = new()
                {
                    Email = userEmail!
                };


                List<Document> documentsByEmail = new();
                var folderName = string.Empty;
                foreach (string folderfile in folderFilesDocuments)
                {
                    var filesByFolder = Directory.GetFiles(folderfile);
                    folderName = Path.GetFileName(folderfile);

                    await reportLogAsync($"Usuario con correo: {userEmail} carpeta: {folderfile} Archivos: {filesByFolder.Count()}", Color.Green);


                    foreach (string file in filesByFolder)
                    {
                        if (folderName.Contains("identid", StringComparison.InvariantCultureIgnoreCase))
                        {
                            folderName = DocumentTypeEnum.DocumentoIdentidad.GetDescription();
                        }
                        else if (folderName.Contains("sarl", StringComparison.InvariantCultureIgnoreCase))
                        {
                            folderName = DocumentTypeEnum.Sarlaft.GetDescription();
                        }
                        else if (folderName.Contains("rut", StringComparison.InvariantCultureIgnoreCase))
                        {
                            folderName = DocumentTypeEnum.Rut.GetDescription();
                        }
                        else if (folderName.Contains("existen", StringComparison.InvariantCultureIgnoreCase))
                        {
                            folderName = DocumentTypeEnum.CertificadoExistencia.GetDescription();
                        }
                        else
                        {
                            break;
                        }

                        documentsByEmail.Add(
                            new Document()
                            {
                                FileType = folderName,
                                FileName = Path.GetFileName(file),
                                Base64Document = DocumentService.ConvertPDFtoBase64(file),
                                DocumentPath = file
                            });
                    }
                }

                IEnumerable<Document> queryExtension = documentsByEmail
                    .Where(document => Path.GetExtension(document.DocumentPath).ToLower() != ".pdf");

                if (queryExtension.Any())
                {
                    List<string> extensions = queryExtension.Select(doc => doc.FileType).ToList();

                    string messageLog = $"Usuario con correo: {userEmail} con soportes en otro formato {string.Join("-", extensions)}";

                    await reportLogAsync(messageLog, Color.Red);
                    ApplicationLogService.GenerateLogByMessage(userEmail, messageLog, "email");
                    continue;
                }

                newEmailDocuments.Documents = documentsByEmail;
                newEmailDocuments.PersonTypeFolder = CleanDataHelper.CleanPersonTypeByName(personTypeFolderName);
                newEmailDocuments.UserRolFolder = CleanDataHelper.DefaultUserRol(userRolFolderName);

                lstEmailDocs.Add(newEmailDocuments);
            }

            return lstEmailDocs;
        }

        public class EmailDocuments
        {
            public string Email { get; set; }
            public string PersonTypeFolder { get; set; }
            public string UserRolFolder { get; set; }
            public IEnumerable<Document> Documents { get; set; }
        }

        public class Document
        {
            public string FileType { get; set; }
            public string FileName { get; set; }
            public string DocumentPath { get; set; }
            public string Base64Document { get; set; }
        }
    }
}
