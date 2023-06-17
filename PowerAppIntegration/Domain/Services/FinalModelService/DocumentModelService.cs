using Migration.Domain.Domain.DTOs.MigracionActividad;
using Migration.Domain.Domain.Enums;
using Migration.Domain.Domain.Helpers;
using Migration.Domain.Infrastructure.Logs;
using System.Data;
using System.Text.RegularExpressions;
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
            string stringPath = GeneralData.DOCUMENTS_PATH;

            var stateFolders = Directory.GetDirectories(stringPath);

            List<EmailDocuments> lstEmailDocs = new();
            List<RequisitosLegalesDocuments> activityDocuments = new();

            foreach (string stateFolderParent in stateFolders)
            {
                try
                {
                    string state = Path.GetFileName(stateFolderParent);

                    List<string> NitFoldersParent = Directory.GetDirectories(stateFolderParent).ToList();


                    if (!NitFoldersParent.Any())
                    {
                        string messageLog = $"Capeta {stateFolderParent} sin correos con soportes";

                        await reportLogAsync(messageLog, Color.Red);
                        ApplicationLogService.GenerateLogByMessage("Error general", messageLog, "email");
                        continue;
                    }

                    foreach (string nitFolder in NitFoldersParent)
                    {
                        string nit = Path.GetFileName(nitFolder);
                        string emailName = string.Empty;


                        DataRow? queryCompany = GeneralData.CONTACT_LIST
                               .FirstOrDefault(user => user.GetValueData(Contact.CompanyIdentification.GetDescription()) == nit);


                        List<string> Contacts = new List<string>();

                        IEnumerable<DataRow> userByNit = GeneralData.CONTACT_LIST
                               .Where(user => user.GetValueData(Contact.CompanyIdentification.GetDescription()) == nit);


                        if (userByNit.Any())
                        {
                            Contacts = userByNit.Select(user => user.GetValueData(Contact.JobTitle.GetDescription())).ToList();

                            Contacts = Contacts.Where(user => user.Contains("contact", StringComparison.InvariantCultureIgnoreCase)).ToList();
                        }

                        if (queryCompany is null)
                        {
                            string messageLog = $"No se encuentra relacion de contacto empresa";

                            await reportLogAsync(messageLog, Color.Red);
                            ApplicationLogService.GenerateLogByMessage(nit, messageLog, "email");
                            continue;
                        }

                        string EspCompany = queryCompany.GetValueData(Contact.ESP.GetDescription());

                        RequisitosLegalesDocuments requisitosLegalesDocuments = new RequisitosLegalesDocuments();

                        if (!Regex.IsMatch(nit, @"^[0-9]+$"))
                        {
                            string messageLog = $"Capeta con NIT {nitFolder} no es valido";

                            await reportLogAsync(messageLog, Color.Red);
                            ApplicationLogService.GenerateLogByMessage(nit, messageLog, "email");
                            continue;
                        }

                        List<string> apoderadorFolderParent = Directory.GetDirectories(nitFolder).ToList();

                        if (!apoderadorFolderParent.Any())
                        {
                            string messageLog = $"Capeta con NIT {nitFolder} no  tiene carpeta para definir si es apoderado o no";

                            await reportLogAsync(messageLog, Color.Red);
                            ApplicationLogService.GenerateLogByMessage(nit, messageLog, "email");
                            continue;
                        }

                        string? apoderadoFolderParent = apoderadorFolderParent.FirstOrDefault();

                        if (string.IsNullOrEmpty(apoderadoFolderParent))
                        {
                            string messageLog = $"Capeta con NIT {nitFolder} error al obtener la carpeta de apoderado";

                            await reportLogAsync(messageLog, Color.Red);
                            ApplicationLogService.GenerateLogByMessage(nit, messageLog, "email");
                            continue;
                        }


                        string apoderadoValidacion = Path.GetFileName(apoderadoFolderParent);

                        List<string> ValidationList = new List<string>
                        {
                            "Apoderado",
                            "NoApoderado"
                        };

                        if (!ValidationList.Contains(apoderadoValidacion))
                        {
                            string messageLog = $"No se pudo validar el tipo de apoderado {apoderadoFolderParent}";

                            await reportLogAsync(messageLog, Color.Red);
                            ApplicationLogService.GenerateLogByMessage(nit, messageLog, "email");
                            continue;
                        }

                        List<ParticipationDocumentDataDto> docsOpcionales = new();

                        if (apoderadoValidacion.Contains("NoApoderado"))
                        {
                            List<string> FolderParentNoApoderado = Directory.GetDirectories(apoderadoFolderParent).ToList();

                            foreach (string folderDocument in FolderParentNoApoderado)
                            {
                                string file = Path.GetFileName(folderDocument);

                                List<string> documentsInFolderNoApoderado = Directory.GetFiles(folderDocument).ToList();

                                if (documentsInFolderNoApoderado.Any() &&
                                    !documentsInFolderNoApoderado.Any(pdf => pdf != string.Empty! && Path.GetExtension(pdf).ToLowerInvariant() == ".pdf"))
                                {
                                    string messageLog = $"Existen archivos en un formato diferente";

                                    await reportLogAsync(messageLog, Color.Red);
                                    ApplicationLogService.GenerateLogByMessage(nit, messageLog, "email");
                                    continue;
                                }

                                if (file.Contains("Autoriza"))
                                {
                                    SetAutorizacionDocument(requisitosLegalesDocuments, documentsInFolderNoApoderado);
                                }

                                if (file.Contains("Opcion"))
                                {
                                    SetOptionalDocument(docsOpcionales, documentsInFolderNoApoderado);

                                }

                            }

                            SetESPDocument(EspCompany, requisitosLegalesDocuments);

                        }
                        else
                        {
                            List<string> FolderParentApoderado = Directory.GetDirectories(apoderadoFolderParent).ToList();


                            if (FolderParentApoderado.Count > 1)
                            {
                                string messageLog = $"Existe mas de una carpeta con el correo de apoderado";

                                await reportLogAsync(messageLog, Color.Red);
                                ApplicationLogService.GenerateLogByMessage(nit, messageLog, "email");
                                continue;
                            }

                            if (!FolderParentApoderado.Any())
                            {
                                string messageLog = $"No existen carpetas para definir el apoderado";

                                await reportLogAsync(messageLog, Color.Red);
                                ApplicationLogService.GenerateLogByMessage(nit, messageLog, "email");
                                continue;
                            }

                            string? folderApoderado = FolderParentApoderado.FirstOrDefault();

                            if (string.IsNullOrEmpty(folderApoderado))
                            {
                                string messageLog = $"Error al obtener el correo del apoderado";

                                await reportLogAsync(messageLog, Color.Red);
                                ApplicationLogService.GenerateLogByMessage(nit, messageLog, "email");
                                continue;
                            }


                            emailName = Path.GetFileName(folderApoderado);

                            emailName = emailName.Trim().ToLower();

                            IEnumerable<DataRow> queryValidateContact = GeneralData.CONTACT_LIST
                                .Where(user => user.GetValueData(Contact.Email.GetDescription()).ToLower() == emailName);

                            if (!queryValidateContact.Any())
                            {
                                string messageLog = $"No se encuentran datos del contacto {emailName} en el excel";

                                await reportLogAsync(messageLog, Color.Red);
                                ApplicationLogService.GenerateLogByMessage(nit, messageLog, "email");
                                continue;
                            }

                            queryValidateContact = queryValidateContact
                                .Where(user => user.GetValueData(Contact.CompanyIdentification.GetDescription()).ToLower() == nit);


                            if (!queryValidateContact.Any())
                            {
                                string messageLog = $"No se encuentran datos del contacto {emailName} para el nit {nit} en el excel";

                                await reportLogAsync(messageLog, Color.Red);
                                ApplicationLogService.GenerateLogByMessage(nit, messageLog, "email");
                                continue;
                            }


                            DataRow? userValid = queryValidateContact.FirstOrDefault();


                            if (userValid is null)
                            {
                                string messageLog = $"Error al tomar los datos del contacto  {emailName} para el nit {nit}";

                                await reportLogAsync(messageLog, Color.Red);
                                ApplicationLogService.GenerateLogByMessage(nit, messageLog, "email");
                                continue;
                            }

                            string emailFolder = FolderParentApoderado.FirstOrDefault()!;

                            List<string> foldersByEmail = Directory.GetDirectories(emailFolder).ToList();

                            if (!foldersByEmail.Any())
                            {
                                string messageLog = $"Carpeta sin soportes para el correo {emailName}";

                                await reportLogAsync(messageLog, Color.Red);
                                ApplicationLogService.GenerateLogByMessage(nit, messageLog, "email");
                                continue;
                            }


                            foreach (string folderDocument in foldersByEmail)
                            {
                                string file = Path.GetFileName(folderDocument);

                                List<string> documentsInFolderApoderado = Directory.GetFiles(folderDocument).ToList();

                                if (documentsInFolderApoderado.Any() &&
                                    !documentsInFolderApoderado.Any(pdf => pdf != string.Empty! && Path.GetExtension(pdf).ToLowerInvariant() == ".pdf"))
                                {
                                    string messageLog = $"Existen archivos en un formato diferente";

                                    await reportLogAsync(messageLog, Color.Red);
                                    ApplicationLogService.GenerateLogByMessage(nit, messageLog, "email");
                                    continue;
                                }

                                if (file.Contains("Autoriza"))
                                {
                                    SetAutorizacionDocument(requisitosLegalesDocuments, foldersByEmail);
                                }


                            }


                        }


                        requisitosLegalesDocuments.NIT = nit;
                        requisitosLegalesDocuments.Apodera = apoderadoValidacion.Equals("Apoderado");
                        requisitosLegalesDocuments.Esp = EspCompany.Equals("1");
                        requisitosLegalesDocuments.DocumentosOpcionales = docsOpcionales;
                        requisitosLegalesDocuments.Estado = state;
                        requisitosLegalesDocuments.CorreosContactos = Contacts;
                        requisitosLegalesDocuments.Correo = emailName;
                    }




                }
                catch (Exception ex)
                {

                    string messageLog = $"Error procesando: {ex}";

                    await reportLogAsync(messageLog, Color.Red);
                    ApplicationLogService.GenerateLogByMessage("Error general", messageLog, "email");
                    continue;

                }
            }

            return lstEmailDocs;
        }

        private static void SetOptionalDocument(List<ParticipationDocumentDataDto> docsOpcionales, List<string> documentsInFolderNoApoderado)
        {
            int posicionDocumento = 1;
            foreach (string documentoOpcional in documentsInFolderNoApoderado)
            {


                docsOpcionales.Add(new()
                {
                    Type = "file",
                    Value = DocumentService.ConvertPDFtoBase64(documentoOpcional),
                    Label = $"informacion complementaria {posicionDocumento}",
                    Field = $"Información complementaria {posicionDocumento}"
                });

                posicionDocumento++;
            }

            for (int i = docsOpcionales.Count + 1; i <= 10; i++)
            {
                docsOpcionales.Add(new()
                {
                    Type = "file",
                    Value = string.Empty,
                    Label = $"informacion complementaria {i}",
                    Field = $"Información complementaria {i}"
                });
            }
        }

        private static void SetESPDocument(string EspCompany, RequisitosLegalesDocuments requisitosLegalesDocuments)
        {
            string espDocument = string.Empty;
            if (!EspCompany.Equals("1"))
            {
                espDocument = DocumentService.ConvertPDFtoBase64(GeneralData.PDF_EN_BLANCO);
            }

            requisitosLegalesDocuments.ConstitucionFuturaESP = new()
            {
                Type = "file",
                Value = espDocument,
                Label = "Formato de constitución futura como ESP",
                Field = "formatFutureConstitutionAsESP"
            };
        }

        private static void SetAutorizacionDocument(RequisitosLegalesDocuments requisitosLegalesDocuments, List<string> documentsInFolderNoApoderado)
        {
            string? filePathAutorizacion = string.Empty;

            if (documentsInFolderNoApoderado.Any())
            {
                filePathAutorizacion = documentsInFolderNoApoderado.FirstOrDefault();
            }
            else
            {
                filePathAutorizacion = GeneralData.PDF_EN_BLANCO;
            }

            requisitosLegalesDocuments.AutorizacionCuantia = new()
            {
                Type = "file",
                Value = DocumentService.ConvertPDFtoBase64(filePathAutorizacion),
                Label = "Autorización para contratar en cuantía ilimitada y sin restricciones",
                Field = "authorizationToContractUnlimitedAmount"
            };
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
