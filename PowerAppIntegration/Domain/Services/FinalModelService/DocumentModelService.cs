using Migration.Domain.Domain.DTOs.MigracionActividad;
using Migration.Domain.Domain.Enums;
using Migration.Domain.Domain.Helpers;
using Migration.Domain.Infrastructure.Adapters;
using Migration.Domain.Infrastructure.Logs;
using System.Data;
using System.Text.RegularExpressions;
using static PowerAppIntegration.PowerAppForm;

namespace Migration.Domain.Domain.Services.FinalModelService
{
    public class DocumentModelService
    {
        private readonly ReportLogAsyncDelegate reportLogAsync;
        private readonly MechanismService MechanismService;

        public DocumentModelService(ReportLogAsyncDelegate reportLogAsync)
        {
            this.reportLogAsync = reportLogAsync;
            this.MechanismService = new();
        }

        public async Task<IEnumerable<EmailDocuments>> GetAllEmailFolders()
        {
            string stringPath = GeneralData.DOCUMENTS_PATH;

            stringPath = @"C:\\Users\\jorge.gonzalez\\Desktop\\SoportesReales\\Laboratorio";

            var stateFolders = Directory.GetDirectories(stringPath);

            List<EmailDocuments> lstEmailDocs = new();
            List<RequisitosLegalesDocuments> activityDocuments = new();

            List<ParticipationDataInputDto> participaciones = new();

            foreach (string stateFolderParent in stateFolders)
            {
                try
                {
                    string state = Path.GetFileName(stateFolderParent);

                    if (StatusRegisterEnum.Approbed.GetDescription().Contains(state, StringComparison.InvariantCultureIgnoreCase))
                    {
                        state = StatusRegisterEnum.Approbed.GetDescription();
                    }
                    else if (StatusRegisterEnum.Pending.GetDescription().IndexOf(state, StringComparison.CurrentCultureIgnoreCase) >= 0)
                    {
                        state = StatusRegisterEnum.Pending.GetDescription();
                    }
                    else
                    {
                        string messageLog = $"El estado esta mal!";

                        await reportLogAsync(messageLog, Color.Red);
                        ApplicationLogService.GenerateLogByMessage("Error general", messageLog, "LogRequisitosLegales");
                        continue;
                    }

                    List<string> NitFoldersParent = Directory.GetDirectories(stateFolderParent).ToList();


                    if (!NitFoldersParent.Any())
                    {
                        string messageLog = $"Carpeta {stateFolderParent} sin correos con soportes";

                        await reportLogAsync(messageLog, Color.Red);
                        ApplicationLogService.GenerateLogByMessage("Error general", messageLog, "LogRequisitosLegales");
                        continue;
                    }

                    foreach (string nitFolder in NitFoldersParent)
                    {
                        RequisitosLegalesDocuments requisitosLegalesDocuments = new RequisitosLegalesDocuments();
                        string nit = Path.GetFileName(nitFolder);
                        string emailName = string.Empty;


                        DataRow? queryCompany = GeneralData.CONTACT_LIST
                               .FirstOrDefault(user => user.GetValueData(Contact.CompanyIdentification.GetDescription()) == nit);


                        List<string> Contacts = new List<string>();

                        IEnumerable<DataRow> userByNit = GeneralData.CONTACT_LIST
                               .Where(user => user.GetValueData(Contact.CompanyIdentification.GetDescription()) == nit);


                        if (userByNit.Any())
                        {

                            Contacts = userByNit
                                .Where(user => user.GetValueData(Contact.JobTitle.GetDescription()).Contains("contact", StringComparison.InvariantCultureIgnoreCase))
                                .Select(userFilter => userFilter.GetValueData(Contact.Email.GetDescription())).ToList();
                        }

                        if (queryCompany is null)
                        {
                            string messageLog = $"No se encuentra relacion de contacto empresa";

                            await reportLogAsync(messageLog, Color.Red);
                            ApplicationLogService.GenerateLogByMessage(nit, messageLog, "LogRequisitosLegales");
                            continue;
                        }

                        string EspCompany = queryCompany.GetValueData(Contact.ESP.GetDescription());

                        if (!Regex.IsMatch(nit, @"^[0-9]+$"))
                        {
                            string messageLog = $"Carpeta con NIT {nitFolder} no es valido";

                            await reportLogAsync(messageLog, Color.Red);
                            ApplicationLogService.GenerateLogByMessage(nit, messageLog, "LogRequisitosLegales");
                            continue;
                        }

                        List<string> apoderadorFolderParent = Directory.GetDirectories(nitFolder).ToList();

                        if (!apoderadorFolderParent.Any())
                        {
                            string messageLog = $"Carpeta con NIT {nitFolder} no  tiene carpeta para definir si es apoderado o no";

                            await reportLogAsync(messageLog, Color.Red);
                            ApplicationLogService.GenerateLogByMessage(nit, messageLog, "LogRequisitosLegales");
                            continue;
                        }

                        if (apoderadorFolderParent.Count > 1)
                        {
                            string messageLog = $"Carpeta  con multiples tipos de actividad ";

                            await reportLogAsync(messageLog, Color.Red);
                            ApplicationLogService.GenerateLogByMessage(nit, messageLog, "LogRequisitosLegales");
                            continue;
                        }

                        string? apoderadoFolderParent = apoderadorFolderParent.FirstOrDefault();

                        if (string.IsNullOrEmpty(apoderadoFolderParent))
                        {
                            string messageLog = $"Carpeta con NIT {nitFolder} error al obtener la carpeta de apoderado";

                            await reportLogAsync(messageLog, Color.Red);
                            ApplicationLogService.GenerateLogByMessage(nit, messageLog, "LogRequisitosLegales");
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
                            ApplicationLogService.GenerateLogByMessage(nit, messageLog, "LogRequisitosLegales");
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
                                    ApplicationLogService.GenerateLogByMessage(nit, messageLog, "LogRequisitosLegales");
                                    continue;
                                }

                                if (file.Contains("Autoriza", StringComparison.InvariantCultureIgnoreCase))
                                {
                                    string pathAutorizacion = documentsInFolderNoApoderado.FirstOrDefault()!;

                                    SetAutorizacionDocument(requisitosLegalesDocuments, pathAutorizacion);
                                }

                                if (file.Contains("Opcion", StringComparison.InvariantCultureIgnoreCase))
                                {
                                    SetOptionalDocument(docsOpcionales, documentsInFolderNoApoderado);
                                }
                            }

                            SetPowerDocument(requisitosLegalesDocuments, string.Empty);
                            SetCertificadoDoument(requisitosLegalesDocuments, string.Empty);

                        }
                        else
                        {
                            List<string> FolderParentApoderado = Directory.GetDirectories(apoderadoFolderParent).ToList();


                            if (FolderParentApoderado.Count > 1)
                            {
                                string messageLog = $"Existe mas de una carpeta con el correo de apoderado nit | {nit}";

                                await reportLogAsync(messageLog, Color.Red);
                                ApplicationLogService.GenerateLogByMessage(nit, messageLog, "LogRequisitosLegales");
                                continue;
                            }

                            if (!FolderParentApoderado.Any())
                            {
                                string messageLog = $"No existen carpetas para definir el apoderado";

                                await reportLogAsync(messageLog, Color.Red);
                                ApplicationLogService.GenerateLogByMessage(nit, messageLog, "LogRequisitosLegales");
                                continue;
                            }

                            string? folderApoderado = FolderParentApoderado.FirstOrDefault();

                            if (string.IsNullOrEmpty(folderApoderado))
                            {
                                string messageLog = $"Error al obtener el correo del apoderado";

                                await reportLogAsync(messageLog, Color.Red);
                                ApplicationLogService.GenerateLogByMessage(nit, messageLog, "LogRequisitosLegales");
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
                                ApplicationLogService.GenerateLogByMessage(nit, messageLog, "LogRequisitosLegales");
                                continue;
                            }

                            queryValidateContact = queryValidateContact
                                .Where(user => user.GetValueData(Contact.CompanyIdentification.GetDescription()).ToLower() == nit);


                            if (!queryValidateContact.Any())
                            {
                                string messageLog = $"No se encuentran datos del contacto {emailName} para el nit {nit} en el excel";

                                await reportLogAsync(messageLog, Color.Red);
                                ApplicationLogService.GenerateLogByMessage(nit, messageLog, "LogRequisitosLegales");
                                continue;
                            }


                            DataRow? userValid = queryValidateContact.FirstOrDefault();


                            if (userValid is null)
                            {
                                string messageLog = $"Error al tomar los datos del contacto  {emailName} para el nit {nit}";

                                await reportLogAsync(messageLog, Color.Red);
                                ApplicationLogService.GenerateLogByMessage(nit, messageLog, "LogRequisitosLegales");
                                continue;
                            }

                            string emailFolder = FolderParentApoderado.FirstOrDefault()!;

                            List<string> foldersByEmail = Directory.GetDirectories(emailFolder).ToList();

                            if (!foldersByEmail.Any())
                            {
                                string messageLog = $"Carpeta sin soportes para el correo {emailName}";

                                await reportLogAsync(messageLog, Color.Red);
                                ApplicationLogService.GenerateLogByMessage(nit, messageLog, "LogRequisitosLegales");
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
                                    ApplicationLogService.GenerateLogByMessage(nit, messageLog, "LogRequisitosLegales");
                                    continue;
                                }

                                string documentPath = documentsInFolderApoderado.FirstOrDefault()!;

                                if (file.Contains("Autoriza", StringComparison.InvariantCultureIgnoreCase))
                                {
                                    SetAutorizacionDocument(requisitosLegalesDocuments, documentPath);
                                }

                                if (file.Contains("Opcion", StringComparison.InvariantCultureIgnoreCase))
                                {
                                    SetOptionalDocument(docsOpcionales, documentsInFolderApoderado);

                                }

                                if (file.Contains("poder", StringComparison.InvariantCultureIgnoreCase))
                                {
                                    if (string.IsNullOrEmpty(documentPath))
                                    {
                                        string messageLog = $"El poder es obligatorio para el correo {emailName}";

                                        await reportLogAsync(messageLog, Color.Red);
                                        ApplicationLogService.GenerateLogByMessage(nit, messageLog, "LogRequisitosLegales");
                                        continue;
                                    }

                                    SetPowerDocument(requisitosLegalesDocuments, documentPath);
                                    SetCertificadoDoument(requisitosLegalesDocuments, string.Empty);

                                }
                            }
                        }

                        SetESPDocument(EspCompany, requisitosLegalesDocuments);

                        requisitosLegalesDocuments.NIT = nit;
                        requisitosLegalesDocuments.Apodera = apoderadoValidacion.Equals("Apoderado");
                        requisitosLegalesDocuments.Esp = EspCompany.Equals("1");
                        requisitosLegalesDocuments.DocumentosOpcionales = docsOpcionales;
                        requisitosLegalesDocuments.Estado = state;
                        requisitosLegalesDocuments.CorreosContactos = Contacts;
                        requisitosLegalesDocuments.Correo = emailName;


                        activityDocuments.Add(requisitosLegalesDocuments);

                        List<ParticipationFormDataDto> participationForms = new()
                        {
                            new ParticipationFormDataDto
                            {
                                Name = "Rol del registro",
                                ParticipationFormFieldsData = new List<ParticipationFormFieldDataDto>
                                {
                                    new ParticipationFormFieldDataDto
                                    {
                                        Type = "radio",
                                        Value = "true",
                                        Field = "constitutedCompany"
                                    },
                                    new ParticipationFormFieldDataDto
                                    {
                                        Type = "radio",
                                        Value = requisitosLegalesDocuments.Esp.ToString().ToLower(),
                                        Field = "isESP"
                                    }
                                }
                            },
                            new ParticipationFormDataDto
                            {
                                Name = "Asignación del apoderado",
                                ParticipationFormFieldsData = new List<ParticipationFormFieldDataDto>
                                {
                                    new ParticipationFormFieldDataDto
                                    {
                                        Type = "checkbox",
                                        Value = requisitosLegalesDocuments.Apodera.ToString().ToLower(),
                                        Field = "hasRepresentative"
                                    },
                                    new ParticipationFormFieldDataDto
                                    {
                                        Type = "select",
                                        Value = "e8111991-019d-48fd-adc2-6905ca0d9f8f",
                                        Field = "selectedRepresentative"
                                    }
                                }
                            }
                        };


                        List<ParticipationDocumentDataDto> participationDocumentDatas = new()
                        {
                            requisitosLegalesDocuments.Poder,
                            requisitosLegalesDocuments.ConstitucionFuturaESP,
                            requisitosLegalesDocuments.AutorizacionCuantia,
                            requisitosLegalesDocuments.CertificadoExistencia

                        };

                        participationDocumentDatas.AddRange(requisitosLegalesDocuments.DocumentosOpcionales);

                        participaciones.Add(new()
                        {
                            NIT = requisitosLegalesDocuments.NIT,
                            UserId = Guid.Empty,
                            AgentId = Guid.Empty,
                            AdminId = new Guid("c7b06ed9-8545-4f64-ad7c-6b73816db390"),
                            AdminName = "Edgar Cadavid",
                            CorreoApoderado = requisitosLegalesDocuments.Correo,
                            MechanismActivityId = new Guid("A32ACCEA-10C2-497C-1789-08DB69CC9FED"),
                            State = requisitosLegalesDocuments.Estado,
                            ParticipationFormData = participationForms,
                            ParticipationDocumentData = participationDocumentDatas,
                            ContactEmails  = requisitosLegalesDocuments.CorreosContactos
                        });

                        await reportLogAsync($"Registro con nit| {requisitosLegalesDocuments.NIT}", Color.Green);

                    }


                }
                catch (Exception ex)
                {

                    string messageLog = $"Error procesando: {ex}";

                    await reportLogAsync(messageLog, Color.Red);
                    ApplicationLogService.GenerateLogByMessage("Error general", messageLog, "LogRequisitosLegales");
                    continue;

                }


            }

            foreach (ParticipationDataInputDto participacion in participaciones)
            {
                var responseRequest = await MechanismService.SaveDynamicFormAsync(participacion);

                await reportLogAsync(responseRequest, Color.Purple);
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

        private static void SetAutorizacionDocument(RequisitosLegalesDocuments requisitosLegalesDocuments, string pathFileAutorizacion)
        {
            string base64Path = string.Empty;
            if (!string.IsNullOrEmpty(pathFileAutorizacion))
            {
                base64Path = DocumentService.ConvertPDFtoBase64(pathFileAutorizacion);
            }

            requisitosLegalesDocuments.AutorizacionCuantia = new()
            {
                Type = "file",
                Value = base64Path,
                Label = "Autorización para contratar en cuantía ilimitada y sin restricciones",
                Field = "authorizationToContractUnlimitedAmount"
            };
        }

        private static void SetPowerDocument(RequisitosLegalesDocuments requisitosLegalesDocuments, string pathFilePoder)
        {
            string base64Path = string.Empty;
            if (!string.IsNullOrEmpty(pathFilePoder))
            {
                base64Path = DocumentService.ConvertPDFtoBase64(pathFilePoder);
            }

            requisitosLegalesDocuments.Poder = new()
            {
                Type = "file",
                Value = base64Path,
                Label = "poder",
                Field = "signedAuthorization"
            };
        }

        private static void SetCertificadoDoument(RequisitosLegalesDocuments requisitosLegalesDocuments, string pathFile)
        {
            string base64Path = string.Empty;
            if (!string.IsNullOrEmpty(pathFile))
            {
                base64Path = DocumentService.ConvertPDFtoBase64(pathFile);
            }

            requisitosLegalesDocuments.CertificadoExistencia = new()
            {
                Type = "file",
                Value = base64Path,
                Label = "Certificado de existencia y representación legal",
                Field = "representationAndExistenceCertification"
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
