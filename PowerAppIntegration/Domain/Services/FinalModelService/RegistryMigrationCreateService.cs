using Migration.Domain.Domain.DTOs.MigracionUsuarios;
using Migration.Domain.Domain.Enums;
using Migration.Domain.Domain.Helpers;
using System.Data;
using static Migration.Domain.Domain.DTOs.MigracionUsuarios.MigrationPackDto;
using static Migration.Domain.Domain.Services.FinalModelService.DocumentModelService;

namespace Migration.Domain.Domain.Services.FinalModelService
{
    public static class RegistryMigrationCreateService
    {
        public static RegistryMigrationDto? MigrateContacPlanBAsync(DataRow validContact, EmailDocuments documents)
        {
            return new RegistryMigrationDto();
        }

        private static string Base64Document(EmailDocuments documents, string documentName)
        {
            var query = from Document doc in documents.Documents
                        where documentName.Contains(doc.FileType, StringComparison.InvariantCultureIgnoreCase)
                        select doc.Base64Document;

            if (query is null || !query.Any())
            {
                return string.Empty;
            }

            if (string.IsNullOrEmpty(query.FirstOrDefault()))
            {
                return string.Empty;
            }

            return query.FirstOrDefault()!;
        }
    }
}
