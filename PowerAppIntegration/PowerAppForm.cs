using Migration.Domain.Domain;
using Migration.Domain.Domain.Enums;
using Migration.Domain.Domain.Helpers;
using Migration.Domain.Domain.Services.FinalModelService;
using System.ComponentModel;
using System.Data;

namespace PowerAppIntegration
{
    public partial class PowerAppForm : Form
    {
        public delegate Task ReportLogAsyncDelegate(string mensaje, Color color);

        ReportLogAsyncDelegate reportLogAsyncDelegate;
        DocumentModelService serviceDocuments;
        public PowerAppForm()
        {
            InitializeComponent();

            GeneralData.DT_CONTACT = new DataTable();

            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.RowHeadersVisible = false;

            reportLogAsyncDelegate = new(ReportLogAsync);

            serviceDocuments = new(reportLogAsyncDelegate);
        }

        #region PEGAR DATOS
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
        #endregion

        #region PROCESAR DATOS
        private void button1_Click(object sender, EventArgs e)
        {

            if (backgroundWorkerProcesarDatos.IsBusy)
            {
                return;
            }

            backgroundWorkerProcesarDatos.RunWorkerAsync();
        }

        private async void backgroundWorkerProcesarDatos_DoWork(object sender, DoWorkEventArgs e)
        {
            await serviceDocuments.GetAllEmailFolders();
        }
        private void backgroundWorkerContact_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ProcesarProgressBar.Value = e.ProgressPercentage;
        }

        #endregion

        #region LIMPIAR CONTACTOS

        private void button2_Click(object sender, EventArgs e)
        {

            if (backgroundWorkerLimpiarDatos.IsBusy)
            {
                return;
            }

            backgroundWorkerLimpiarDatos.RunWorkerAsync();
        }

        private void backgroundWorkerLimpiarDatos_DoWork(object sender, DoWorkEventArgs e)
        {
            GeneralData.CONTACT_LIST = new List<DataRow>();
            foreach (DataRow contact in GeneralData.DT_CONTACT.Rows)
            {
                CleanDataService.CleanDataRowContact(contact);

                int currentIndex = GeneralData.DT_CONTACT.Rows.IndexOf(contact);
                int progressPercentage = (int)(((double)currentIndex / GeneralData.DT_CONTACT.Rows.Count) * 100);

                GeneralData.CONTACT_LIST.Add(contact);

                backgroundWorkerLimpiarDatos.ReportProgress(progressPercentage);
            }

            backgroundWorkerLimpiarDatos.ReportProgress(100);
        }

        private void backgroundWorker_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            LimpiarProgressBar.Value = e.ProgressPercentage;
        }
        #endregion

        #region ARMAR TABLA
        private void button3_Click(object sender, EventArgs e)
        {
            GeneralData.DT_CONTACT = new();

            IEnumerable<string> contactColumns = EnumHelper.GetListOfDescription<Contact>();

            foreach (string contactColumn in contactColumns)
            {
                GeneralData.DT_CONTACT.Columns.Add(contactColumn, typeof(string));
            }

            dataGridView1.DataSource = GeneralData.DT_CONTACT;
        }
        #endregion


        public async Task ReportLogAsync(string mensaje, Color color)
        {
            await Task.Run(() =>
            {
                // Agregar el mensaje al RichTextBox en segundo plano
                this.Invoke(new Action(() =>
                {
                    reporteLogs.SelectionColor = color;
                    reporteLogs.AppendText(mensaje + Environment.NewLine);
                    reporteLogs.ScrollToCaret();
                }));
            });
        }
    }
}
