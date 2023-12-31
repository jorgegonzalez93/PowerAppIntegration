﻿using Migration.Domain.Domain.DTOs;
using Migration.Domain.Domain.Enums;
using Migration.Domain.Domain.Helpers;
using System.Data;
using static Migration.Domain.Domain.DTOs.MigrationPackDto;
using static Migration.Domain.Domain.Services.FinalModelService.DocumentModelService;

namespace Migration.Domain.Domain.Services.FinalModelService
{
    public static class MigrationHelperService
    {
        public static List<string> SetRequiredDocumentByUser(DataRow validContact)
        {
            List<string> requiredDocuments;
            if (validContact.GetValueData(Enums.Contact.PersonType.GetDescription()).Contains(GeneralData.ENTITY_PERSON, StringComparison.InvariantCultureIgnoreCase))
            {
                if (validContact.GetValueData(Enums.Contact.JobTitle.GetDescription()).Contains(GeneralData.PERSON_REPRESENTATIVE_ROL, StringComparison.InvariantCultureIgnoreCase))
                {
                    requiredDocuments = GeneralData.REQUIRED_DOCUMENTS_LEGAL_ENTITY_REPRESENTATIVE;
                }
                else
                {
                    requiredDocuments = GeneralData.REQUIRED_DOCUMENTS_LEGAL_ENTITY_OTHER;
                }
            }
            else
            {
                requiredDocuments = GeneralData.REQUIRED_DOCUMENTS_NATURAL;
            }

            return requiredDocuments;
        }

        public static DataRow SetValidContactToMigrate(IEnumerable<DataRow> validWithEmailAndNIT)
        {
            DataRow? validContact;

            // Valida si existe el usuario como representante legal
            DataRow? contactlegalRepresentative = validWithEmailAndNIT.FirstOrDefault(query => query[Enums.Contact.JobTitle.GetDescription()].ToString()! == GeneralData.PERSON_REPRESENTATIVE_ROL);

            if (contactlegalRepresentative is not null)
            {
                validContact = contactlegalRepresentative;
            }
            else
            {
                // valida si existe un usuario completo con usuario en b2c 
                contactlegalRepresentative = validWithEmailAndNIT
                    .FirstOrDefault(query => query[Enums.Contact.IdentityUsername.GetDescription()].ToString()! != string.Empty);

                if (contactlegalRepresentative is not null)
                {
                    validContact = contactlegalRepresentative;
                }
                else
                {
                    validContact = validWithEmailAndNIT.FirstOrDefault()!;
                }
            }

            return validContact;
        }

        public static void SetUserRolByFolder(EmailDocuments document, DataRow validContact)
        {
            if (document.UserRolFolder.Contains(GeneralData.PERSON_REPRESENTATIVE_ROL, StringComparison.InvariantCultureIgnoreCase))
            {
                validContact[Enums.Contact.JobTitle.GetDescription()] = GeneralData.PERSON_REPRESENTATIVE_ROL;
            }
            else if (validContact[Enums.Contact.JobTitle.GetDescription()].ToString()!.Contains(GeneralData.PERSON_REPRESENTATIVE_ROL, StringComparison.InvariantCultureIgnoreCase))
            {
                validContact[Enums.Contact.JobTitle.GetDescription()] = GeneralData.DEFAULT_USER_ROL;
            }
        }

        public static void SetPersonTypeByFolder(EmailDocuments document, DataRow validContact)
        {
            if (document.PersonTypeFolder.Contains(GeneralData.ENTITY_PERSON, StringComparison.InvariantCultureIgnoreCase))
            {
                validContact[Enums.Contact.PersonType.GetDescription()] = GeneralData.ENTITY_PERSON;
            }
            else
            {
                validContact[Enums.Contact.PersonType.GetDescription()] = GeneralData.NATURAL_PERSON;
            }
        }

        public static List<string> ValidateIncompleteDocuments(EmailDocuments documents, List<string> requiredDocuments)
        {
            List<string> documentsPending = new();

            foreach (string required in requiredDocuments)
            {
                IEnumerable<Document> documentsByEmail = (from Document doc in documents.Documents
                                                          where doc.FileType.Contains(required, StringComparison.InvariantCultureIgnoreCase)
                                                          select new Document()
                                                          {
                                                              DocumentPath = doc.DocumentPath,
                                                              Base64Document = doc.Base64Document,
                                                              FileName = doc.FileName,
                                                              FileType = doc.FileType,
                                                          });

                if (!documentsByEmail.Any())
                {
                    documentsPending.Add(required);
                }
            }

            return documentsPending;
        }

        public static void AddItemGeneralList(List<B2CDataUser> b2CCreateUser, MigrationPack migrationPack, List<RegistryMigrationDto> migrateUsers)
        {
            List<RegistryMigrationDto> copyUsers = migrateUsers.Select(user =>
                                    new RegistryMigrationDto
                                    {
                                        AdAdminUserId = user.AdAdminUserId,
                                        ApprovedDate = user.ApprovedDate,
                                        ClarificationRegistry = user.ClarificationRegistry,
                                        CompanyInformation = user.CompanyInformation,
                                        CreatedDateTime = user.CreatedDateTime,
                                        EmployeeInformation = user.EmployeeInformation,
                                        FullName = user.FullName,
                                        IsEdit = user.IsEdit,
                                        Observation = user.Observation,
                                        RegistryId = user.RegistryId,
                                        Status = user.Status
                                    }
                                ).ToList();

            migrationPack.userPack.Add(copyUsers);

            List<B2CDataUser> copyB2cUser = b2CCreateUser.Select(user =>
                new B2CDataUser
                {
                    displayName = user.displayName,
                    mail = user.mail,
                    telephone = user.telephone,
                    username = user.username
                }
             ).ToList();


            migrationPack.userB2C.Add(copyB2cUser);
        }
    }
}
