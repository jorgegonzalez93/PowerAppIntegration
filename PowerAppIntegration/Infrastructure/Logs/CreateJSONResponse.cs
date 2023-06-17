using Newtonsoft.Json;

namespace Migration.Domain.Infrastructure.Logs
{
    public static class CreateJsonResponse<T>
    {
        public static void CrateJSONResponse<T>(IEnumerable<T> values, string folder, string fileType)
        {

            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string logFolderPath = Path.Combine(desktopPath, "MigrationLogs", folder);
            Directory.CreateDirectory(logFolderPath);

            string logFilePath = Path.Combine(logFolderPath, $"{fileType}_{DateTime.Now:dd}.json");

            // Convertir la lista a formato JSON y escribir en el archivo
            string json = JsonConvert.SerializeObject(values);
            File.WriteAllText(logFilePath, json);
        }

    }
}
