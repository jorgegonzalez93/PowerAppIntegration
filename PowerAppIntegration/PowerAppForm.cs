using Migration.Domain.Domain;
using Migration.Domain.Domain.DTOs;
using Migration.Domain.Domain.Enums;
using Migration.Domain.Domain.Helpers;
using Migration.Domain.Domain.Services;
using Migration.Domain.Domain.Services.FinalModelService;
using Migration.Domain.Infrastructure.Logs;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Data;
using static Migration.Domain.Domain.DTOs.MigrationPackDto;
using static Migration.Domain.Domain.Services.FinalModelService.DocumentModelService;

namespace PowerAppIntegration
{
    public partial class PowerAppForm : Form
    {
        private MigrationEmployeeService MigrationEmployeeService { get; set; }
        public delegate Task ReportLogAsyncDelegate(string mensaje, Color color);

        ReportLogAsyncDelegate reportLogAsyncDelegate;
        DocumentModelService serviceDocuments;
        public PowerAppForm()
        {
            InitializeComponent();

            MigrationEmployeeService = new();

            GeneralData.DT_CONTACT = new DataTable();
            GeneralData.DT_COMPANY = new DataTable();

            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.RowHeadersVisible = false;


            reportLogAsyncDelegate = new(ReportLogAsync);

            serviceDocuments = new(reportLogAsyncDelegate);
        }

        private void contactImport_Click(object sender, EventArgs e)
        {
            ContactsTableCreate();

            saveContactsButton.Enabled = true;
            saveCompany.Enabled = false;
        }

        private void ContactsTableCreate()
        {
            GeneralData.DT_CONTACT = new();

            IEnumerable<string> contactColumns = EnumHelper.GetListOfDescription<Migration.Domain.Domain.Enums.Contact>();

            foreach (string contactColumn in contactColumns)
            {
                GeneralData.DT_CONTACT.Columns.Add(contactColumn, typeof(string));
            }

            dataGridView1.DataSource = GeneralData.DT_CONTACT;
        }

        private void companyImport_Click(object sender, EventArgs e)
        {
            IEnumerable<string> companyColumns = EnumHelper.GetListOfDescription<Company>();

            GeneralData.DT_COMPANY = new();
            foreach (string contactColumn in companyColumns)
            {
                GeneralData.DT_COMPANY.Columns.Add(contactColumn, typeof(string));
            }

            dataGridView1.DataSource = GeneralData.DT_COMPANY;

            saveCompany.Enabled = true;
            saveContactsButton.Enabled = false;
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V)
            {
                string clipboardText = Clipboard.GetText(TextDataFormat.Text);

                if (!string.IsNullOrEmpty(clipboardText))
                {
                    string[] lines = clipboardText.Split('\n');
                    int columnIndex = dataGridView1.CurrentCell.ColumnIndex;

                    var cleanLines = lines.Where(line => !string.IsNullOrEmpty(line));

                    foreach (string line in cleanLines)
                    {
                        DataRow newRow = ((DataTable)dataGridView1.DataSource).NewRow();

                        bool saveRow = true;

                        int columnsCount = newRow.Table.Columns.Count;

                        if (!string.IsNullOrEmpty(line))
                        {
                            string[] values = line.Split('\t');
                            int maxColumns = dataGridView1.Columns.Count - columnIndex;

                            for (int i = 0; i < values.Length && i < maxColumns; i++)
                            {

                                newRow[i] = ReplaceCharacterHelper.CleanDataChangeCharacters(values[i]);


                                if (string.IsNullOrEmpty(newRow[i].ToString()))
                                {
                                    columnsCount--;
                                }

                                if (columnsCount == 0)
                                {
                                    saveRow = false;
                                }

                            }
                        }

                        if (saveRow)
                        {
                            ((DataTable)dataGridView1.DataSource).Rows.Add(newRow);
                        }
                    }
                }

                IEnumerable<DataRow> query = from DataRow dr in ((DataTable)dataGridView1.DataSource).Rows
                                             where dr.ItemArray.All(item => string.IsNullOrEmpty(item.ToString()))
                                             select dr;


                foreach (DataRow row in query)
                {
                    ((DataTable)dataGridView1.DataSource).Rows.Remove(row);
                }
            }
        }

        private async void saveContactsButton_Click(object sender, EventArgs e)
        {
            if (GeneralData.DT_CONTACT is null || GeneralData.DT_CONTACT.Rows.Count <= 0)
            {
                return;
            }

            if (backgroundWorkerContact.IsBusy)
            {
                return;
            }

            backgroundWorkerContact.RunWorkerAsync();

        }

        private async void saveCompany_Click(object sender, EventArgs e)
        {
            if (GeneralData.DT_COMPANY is null || GeneralData.DT_COMPANY.Rows.Count <= 0)
            {
                return;
            }

            if (backgroundWorkerCompany.IsBusy)
            {
                return;
            }

            backgroundWorkerCompany.RunWorkerAsync();
        }

        private async void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            GeneralData.REGISTRY_COMPANYS = new List<RegistryCompanyDto?>();

            foreach (DataRow company in GeneralData.DT_COMPANY.Rows)
            {
                CleanDataService.CleanDataRowCompany(company);

                GeneralData.REGISTRY_COMPANYS.Add(MigrationEmployeeService.MigrateCompanyAsync(company));

                int currentIndex = GeneralData.DT_COMPANY.Rows.IndexOf(company);
                int progressPercentage = (int)(((double)currentIndex / GeneralData.DT_COMPANY.Rows.Count) * 100);

                backgroundWorkerCompany.ReportProgress(progressPercentage);
            }

            var companylist = new List<CompanyDataDto>();
            foreach (DataRow item in GeneralData.DT_COMPANY.Rows)
            {
                companylist.Add(new()
                {
                    CompanyIdentification = item.GetValueData(Company.CompanyIdentification.GetDescription()),
                    CreateDate = item.GetValueData(Company.DateCreate.GetDescription())
                }) ;
            }
            var json = JsonConvert.SerializeObject(companylist);

            backgroundWorkerCompany.ReportProgress(100);
        }

        private void backgroundWorker_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            companyProgressBar.Value = e.ProgressPercentage;
        }

        private void backgroundWorkerCompany_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            saveCompany.Enabled = true;
        }

        private async void backgroundWorkerContact_DoWork(object sender, DoWorkEventArgs e)
        {
            List<RegistryDto> registryList = new();
            foreach (DataRow contact in GeneralData.DT_CONTACT.Rows)
            {
                RegistryDto? registry = await MigrationEmployeeService.MigrateContactAsync(contact);

                int currentIndex = GeneralData.DT_CONTACT.Rows.IndexOf(contact);
                int progressPercentage = (int)(((double)currentIndex / GeneralData.DT_CONTACT.Rows.Count) * 100);

                if (registry is not null && !registryList.Any(reg => reg.EmployeeInformation.Email == registry.EmployeeInformation.Email))
                {
                    registryList.Add(registry);
                }

                backgroundWorkerContact.ReportProgress(progressPercentage);
            }

            CreateJsonResponse<RegistryDto>.CrateJSONResponse(registryList, "JsonContacto", "Employee");

            backgroundWorkerContact.ReportProgress(100);
        }

        private void backgroundWorkerContact_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            contactProgressBar.Value = e.ProgressPercentage;
        }

        private void backgroundWorkerContact_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            saveContactsButton.Enabled = true;
        }

        private void CreateJSONB2C(object sender, EventArgs e)
        {
            List<B2CDataUser> b2CCreateUser = new();
            foreach (DataRow contact in GeneralData.DT_CONTACT.Rows)
            {
                B2CDataUser? _B2CCreateUser = CreateB2CCreateService.CreateB2CObject(contact);

                int currentIndex = GeneralData.DT_CONTACT.Rows.IndexOf(contact);
                int progressPercentage = (int)(((double)currentIndex / GeneralData.DT_CONTACT.Rows.Count) * 100);

                if (_B2CCreateUser is not null && !b2CCreateUser.Any(b2c => b2c.mail == _B2CCreateUser.mail))
                {
                    b2CCreateUser.Add(_B2CCreateUser);
                }

                backgroundWorkerContact.ReportProgress(progressPercentage);
            }

            CreateJsonResponse<B2CCreateUser>.CrateJSONResponse(b2CCreateUser, "JsonB2C", "b2c");

            //backgroundWorkerContact.ReportProgress(100);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            backgroundWorker_Modelo_Soportes.RunWorkerAsync();
        }

        private async void backgroundWorker_Modelo_Soportes_DoWork(object sender, DoWorkEventArgs e)
        {
            IEnumerable<EmailDocuments> documents = await serviceDocuments.GetAllEmailFolders();

            if (GeneralData.CONTACT_LIST is null || GeneralData.CONTACT_LIST.Count <= 0)
            {
                string messageLog = "Sin contactos cargados";

                await ReportLogAsync(messageLog, Color.Red);
                ApplicationLogService.GenerateLogByMessage("Error general", messageLog, "email");
                return;
            }

            MigrationPack migrationPack = new()
            {
                userPack = new List<List<RegistryMigrationDto>>(),
                userB2C = new List<List<B2CDataUser>>(),
            };

            List<RegistryMigrationDto> migrateUsers = new();
            List<B2CDataUser> b2CCreateUser = new();

            documents = documents.OrderBy(order => order.Email);

            foreach (EmailDocuments document in documents)
            {
                IEnumerable<DataRow>? userQueryByEmail = GeneralData.CONTACT_LIST
                    .Where(contact => contact[Migration.Domain.Domain.Enums.Contact.Email.GetDescription()].ToString()!.Contains(document.Email, StringComparison.InvariantCultureIgnoreCase));

                // Valida si fue posible encontrar el usuario en la lista
                if (userQueryByEmail is null || !userQueryByEmail.Any())
                {
                    string messageLog = $"Usuario con correo: {document.Email} sin datos encontrados";

                    await ReportLogAsync(messageLog, Color.Red);
                    ApplicationLogService.GenerateLogByMessage(document.Email, messageLog, "email");
                    continue;
                }
                
                // Valida si el usuario tiene algun registro con nit               
                IEnumerable < DataRow > prospectiveContactEmailAndNit = userQueryByEmail
                    .Where(query => query[Migration.Domain.Domain.Enums.Contact.CompanyIdentification.GetDescription()].ToString()! != string.Empty);

                if (prospectiveContactEmailAndNit is null || !prospectiveContactEmailAndNit.Any())
                {
                    string messageLog = $"Usuario con correo: {document.Email} no tiene relacion de empresa";

                    await ReportLogAsync(messageLog, Color.Red);
                    ApplicationLogService.GenerateLogByMessage(document.Email, messageLog, "email");
                    continue;
                }

                // Valida si el usuario existe para mas de una empresa
                IEnumerable<string> queryMultipleCompanies = prospectiveContactEmailAndNit
                    .Select(query => query[Migration.Domain.Domain.Enums.Contact.CompanyIdentification.GetDescription()].ToString()!);

                List<string> queryDistinctCompany = queryMultipleCompanies.Distinct().ToList();

                if (queryDistinctCompany.Count > 1)
                {
                    string messageLog = $"Usuario con correo: {document.Email} tiene varias empresas {string.Join("-", queryDistinctCompany)}";

                    await ReportLogAsync(messageLog, Color.Red);
                    ApplicationLogService.GenerateLogByMessage(document.Email, messageLog, "email");
                    continue;
                }

                DataRow? validContact = MigrationHelperService.SetValidContactToMigrate(prospectiveContactEmailAndNit);

                string[] queryContact = validContact[Migration.Domain.Domain.Enums.Contact.Email.GetDescription()].ToString()!.Split("@");

                if (queryContact.Count() > 2)
                {
                    string messageLog = $"Usuario con correo: {document.Email} tiene varias correos en el mismo campo {validContact[Migration.Domain.Domain.Enums.Contact.Email.GetDescription()].ToString()!}";

                    await ReportLogAsync(messageLog, Color.Red);
                    ApplicationLogService.GenerateLogByMessage(document.Email, messageLog, "email");
                    continue;
                }

                // Define el rol segun la carpeta
                MigrationHelperService.SetPersonTypeByFolder(document, validContact);

                // Define el tipo de persona segun la carpeta
                MigrationHelperService.SetUserRolByFolder(document, validContact);

                List<string> requiredDocuments = MigrationHelperService.SetRequiredDocumentByUser(validContact);

                List<string> documentsPending = MigrationHelperService.ValidateIncompleteDocuments(document, requiredDocuments);

                if (documentsPending.Any())
                {
                    string messageLog = $"Usuario con correo: {document.Email} no se puede crear no tiene los documentos completos falta: {string.Join("-", documentsPending)}";

                    await ReportLogAsync(messageLog, Color.Red);
                    ApplicationLogService.GenerateLogByMessage(document.Email, messageLog, "email");
                    continue;
                }

                // Usuario posible

                RegistryMigrationDto? migrateObject = RegistryMigrationCreateService.MigrateContacPlanBAsync(validContact, document);

                B2CDataUser? _B2CCreateUser = CreateB2CCreateService.CreateB2CObject(validContact);

                if (_B2CCreateUser is null)
                {
                    string messageLog = $"Usuario con correo: {document.Email} no se puede generar script para B2C";

                    await ReportLogAsync(messageLog, Color.Red);
                    ApplicationLogService.GenerateLogByMessage(document.Email, messageLog, "email");
                    continue;
                }

                string messageFinalLog = $"Usuario generado de manera correcta";

                await ReportLogAsync(messageFinalLog, Color.Purple);
                ApplicationLogService.GenerateLogByMessage(document.Email, messageFinalLog, "email");

                b2CCreateUser.Add(_B2CCreateUser);
                migrateUsers.Add(migrateObject!);

                if (migrateUsers.Count == GeneralData.MAX_USER_BY_PACKAGE)
                {
                    MigrationHelperService.AddItemGeneralList(b2CCreateUser, migrationPack, migrateUsers);

                    migrateUsers = new();
                    b2CCreateUser = new();
                }

            }

            if (migrateUsers.Any())
            {
                MigrationHelperService.AddItemGeneralList(b2CCreateUser, migrationPack, migrateUsers);
            }

            foreach (List<B2CDataUser> b2cUsers in migrationPack.userB2C)
            {
                int index = migrationPack.userB2C.IndexOf(b2cUsers);
                CreateJsonResponse<B2CCreateUser>.CrateJSONResponse(b2cUsers, $"Paquete {index}", $"b2cPlanB");
            }

            foreach (List<RegistryMigrationDto> userPack in migrationPack.userPack)
            {
                int index = migrationPack.userPack.IndexOf(userPack);
                CreateJsonResponse<RegistryMigrationDto>.CrateJSONResponse(userPack, $"Paquete {index}", $"UserRegistryPlanB");
            }
        }

        public async Task ReportLogAsync(string mensaje, Color color)
        {
            await Task.Run(() =>
            {
                // Agregar el mensaje al RichTextBox en segundo plano
                this.Invoke(new Action(() =>
                {
                    report_log.SelectionColor = color;
                    report_log.AppendText(mensaje + Environment.NewLine);
                    report_log.ScrollToCaret();
                }));
            });
        }

        private void button3_Click(object sender, EventArgs e)
        {
            GeneralData.CONTACT_LIST = new List<DataRow>();
            foreach (DataRow contact in GeneralData.DT_CONTACT.Rows)
            {
                CleanDataService.CleanDataRowContact(contact);

                int currentIndex = GeneralData.DT_CONTACT.Rows.IndexOf(contact);
                int progressPercentage = (int)(((double)currentIndex / GeneralData.DT_CONTACT.Rows.Count) * 100);

                GeneralData.CONTACT_LIST.Add(contact);

                backgroundWorkerContact.ReportProgress(progressPercentage);
            }

            backgroundWorkerContact.ReportProgress(100);
        }

    }
}
