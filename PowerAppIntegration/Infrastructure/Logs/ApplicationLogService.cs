using Migration.Domain.Domain.DTOs.MigracionUsuarios;
using System.Data;

namespace Migration.Domain.Infrastructure.Logs
{
    public static class ApplicationLogService
    {
        public static void GenerateLogError(DataRow lineError, string messageError, string fileError)
        {

            List<Fileld> fields = new()
                        {
                            new()
                            {
                                Required = messageError,
                                Check = false
                            }
                        };

            LogReport(lineError, fields, fileError);
        }

        public static void GenerateLogByMessage(string email, string messageLog, string fileError)
        {

            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string logFolderPath = Path.Combine(desktopPath, "MigrationLogs", fileError);
            Directory.CreateDirectory(logFolderPath);

            string logFilePath = Path.Combine(logFolderPath, $"{fileError}_{DateTime.Now:hh}.csv");

            bool logFileExists = File.Exists(logFilePath);

            using (StreamWriter writer = new StreamWriter(logFilePath, true))
            {
                if (!logFileExists)
                {

                    List<string> headerFields = new()
                    {
                        "Email",
                        "Message"
                    };


                    writer.WriteLine(string.Join(',', headerFields));
                }

                List<string> newFields = new()
                {
                     $"{email}",
                     $"{messageLog}"
                };


                writer.WriteLine(string.Join(',', newFields));
            }
        }

        public static void LogReport(DataRow dataRow, IEnumerable<Fileld> validateFields, string fileType)
        {
            string message = "Registro creado de manera exitosa.";

            IEnumerable<string> invalidFields = from Fileld field in validateFields
                                                where field.Check is false
                                                select field.Required;

            if (invalidFields.Any())
            {
                message = $"El registro no puede ser creado por que no tiene los campos: {string.Join('-', invalidFields)}.";
            }

            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string logFolderPath = Path.Combine(desktopPath, "MigrationLogs", fileType);
            Directory.CreateDirectory(logFolderPath);

            string logFilePath = Path.Combine(logFolderPath, $"{fileType}_{DateTime.Now:hh}.csv");

            bool logFileExists = File.Exists(logFilePath);

            using (StreamWriter writer = new StreamWriter(logFilePath, true))
            {
                if (!logFileExists)
                {

                    List<string> headerFields = (from DataColumn column in dataRow.Table.Columns
                                                 select column.ColumnName.ToString()).ToList();

                    headerFields.Add("Fecha,Evento");

                    writer.WriteLine(string.Join(',', headerFields));
                }

                DateTime hourDate = DateTime.Now;

                List<string> newFields = (from DataColumn column in dataRow.Table.Columns
                                          select dataRow[column.ColumnName].ToString().Replace(",", " ")).ToList();

                newFields.Add($"{hourDate.ToShortDateString().Replace(",", " ")}");

                newFields.Add(message);

                writer.WriteLine(string.Join(',', newFields));
            }
        }

    }
}
