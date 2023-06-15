using Migration.Domain.Domain.DTOs;
using System.Data;

namespace Migration.Domain.Domain.Services
{
    public static class DocumentService
    {
        public static string UploadDocuments(EmployeeMigrationDto employee, string document)
        {
            var directories = Directory.GetDirectories(GeneralData.DOCUMENTS_PATH).ToList();

            string personType = string.Empty;

            string? emailDirectory = directories
                .FirstOrDefault(directory => Path.GetFileName(directory)
                .Equals(employee.Email, StringComparison.OrdinalIgnoreCase));

            if (emailDirectory is null)
            {
                if (GeneralData.CREATE_DUMMY_DOCUMENT)
                {
                    return DummyDocument(document);
                }
                else
                {
                    return $"No existe ningun documento para el registro, error de documento";
                }
            }

            string[] typePersonDirectory = Directory.GetDirectories(emailDirectory);

            string[] folderDocuments = Directory.GetDirectories(typePersonDirectory.FirstOrDefault()!);

            List<string> requiredDocuments;
            if (employee.PersonType.Contains(GeneralData.ENTITY_PERSON, StringComparison.InvariantCultureIgnoreCase))
            {
                requiredDocuments = GeneralData.REQUIRED_DOCUMENTS_LEGAL_ENTITY_REPRESENTATIVE;
            }
            else
            {
                requiredDocuments = GeneralData.REQUIRED_DOCUMENTS_NATURAL;
            }

            if (!requiredDocuments.Any(required => required.Contains(document)))
            {
                return string.Empty;
            }

            IEnumerable<string> requiredDocumentsByType = from string folder in folderDocuments
                                                          where folder.Contains(document, StringComparison.InvariantCultureIgnoreCase)
                                                          select folder;

            if (!requiredDocumentsByType.Any() && !GeneralData.CREATE_DUMMY_DOCUMENT)
            {
                return $"Documento requerido no existe para registro: {document}, error de documento";
            }

            string folderDoc = requiredDocumentsByType.FirstOrDefault()!;

            List<string> folderFiles = Directory.GetFiles(folderDoc).ToList();

            if (!folderFiles.Any() && !GeneralData.CREATE_DUMMY_DOCUMENT)
            {
                return $"Documento requerido no existe para registro: {document}, error de documento";
            }

            if (GeneralData.CREATE_DUMMY_DOCUMENT)
            {
                DummyDocument(document);
            }

            return ConvertPDFtoBase64(folderFiles.FirstOrDefault()!);

        }

        private static string DummyDocument(string documentName)
        {
            var directories = Directory.GetDirectories(GeneralData.DUMMY_PDF).ToList();

            IEnumerable<string> dummyPDF = from string folderDummy in directories
                                           where folderDummy.Contains(documentName, StringComparison.InvariantCultureIgnoreCase)
                                           select folderDummy;

            var folderFiles = Directory.GetFiles(dummyPDF.FirstOrDefault()!).ToList();

            return ConvertPDFtoBase64(folderFiles.FirstOrDefault()!);
        }

        public static string ConvertPDFtoBase64(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                return string.Empty;
            }

            byte[] pdfBytes = File.ReadAllBytes(filePath);

            string base64String = Convert.ToBase64String(pdfBytes);

            return $"data:application/pdf;base64,{base64String}";
        }



    }
}
