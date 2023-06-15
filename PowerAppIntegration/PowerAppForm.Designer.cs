namespace PowerAppIntegration
{
    partial class PowerAppForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            groupBox1 = new GroupBox();
            groupBox3 = new GroupBox();
            progressBar_Email_model = new ProgressBar();
            contactProgressBar = new ProgressBar();
            label1 = new Label();
            companyProgressBar = new ProgressBar();
            groupBox2 = new GroupBox();
            button3 = new Button();
            button2 = new Button();
            button1 = new Button();
            saveContactsButton = new Button();
            companyImport = new Button();
            saveCompany = new Button();
            contactImport = new Button();
            backgroundWorkerCompany = new System.ComponentModel.BackgroundWorker();
            backgroundWorkerContact = new System.ComponentModel.BackgroundWorker();
            backgroundWorker_Modelo_Soportes = new System.ComponentModel.BackgroundWorker();
            groupBox4 = new GroupBox();
            report_log = new RichTextBox();
            dataGridView1 = new DataGridView();
            groupBox1.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(groupBox3);
            groupBox1.Controls.Add(groupBox2);
            groupBox1.Dock = DockStyle.Top;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(1360, 202);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(progressBar_Email_model);
            groupBox3.Controls.Add(contactProgressBar);
            groupBox3.Controls.Add(label1);
            groupBox3.Controls.Add(companyProgressBar);
            groupBox3.Dock = DockStyle.Fill;
            groupBox3.Location = new Point(507, 19);
            groupBox3.Margin = new Padding(2);
            groupBox3.Name = "groupBox3";
            groupBox3.Padding = new Padding(2);
            groupBox3.Size = new Size(850, 180);
            groupBox3.TabIndex = 6;
            groupBox3.TabStop = false;
            // 
            // progressBar_Email_model
            // 
            progressBar_Email_model.Dock = DockStyle.Top;
            progressBar_Email_model.Location = new Point(2, 61);
            progressBar_Email_model.Name = "progressBar_Email_model";
            progressBar_Email_model.Size = new Size(846, 23);
            progressBar_Email_model.TabIndex = 9;
            // 
            // contactProgressBar
            // 
            contactProgressBar.Dock = DockStyle.Top;
            contactProgressBar.Location = new Point(2, 38);
            contactProgressBar.Name = "contactProgressBar";
            contactProgressBar.Size = new Size(846, 23);
            contactProgressBar.TabIndex = 8;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Dock = DockStyle.Bottom;
            label1.Location = new Point(2, 163);
            label1.Name = "label1";
            label1.Size = new Size(0, 15);
            label1.TabIndex = 7;
            // 
            // companyProgressBar
            // 
            companyProgressBar.Dock = DockStyle.Top;
            companyProgressBar.Location = new Point(2, 18);
            companyProgressBar.Margin = new Padding(2);
            companyProgressBar.Name = "companyProgressBar";
            companyProgressBar.Size = new Size(846, 20);
            companyProgressBar.TabIndex = 6;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(button3);
            groupBox2.Controls.Add(button2);
            groupBox2.Controls.Add(button1);
            groupBox2.Controls.Add(saveContactsButton);
            groupBox2.Controls.Add(companyImport);
            groupBox2.Controls.Add(saveCompany);
            groupBox2.Controls.Add(contactImport);
            groupBox2.Dock = DockStyle.Left;
            groupBox2.Location = new Point(3, 19);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(504, 180);
            groupBox2.TabIndex = 5;
            groupBox2.TabStop = false;
            // 
            // button3
            // 
            button3.Dock = DockStyle.Bottom;
            button3.Location = new Point(3, 16);
            button3.Name = "button3";
            button3.Size = new Size(498, 23);
            button3.TabIndex = 8;
            button3.Text = "Contactos Limpiar";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button2
            // 
            button2.Dock = DockStyle.Bottom;
            button2.Location = new Point(3, 39);
            button2.Name = "button2";
            button2.Size = new Size(498, 23);
            button2.TabIndex = 7;
            button2.Text = "Test Nuevo Modelo";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button1
            // 
            button1.Dock = DockStyle.Bottom;
            button1.Location = new Point(3, 62);
            button1.Name = "button1";
            button1.Size = new Size(498, 23);
            button1.TabIndex = 6;
            button1.Text = "Generar JSON B2C";
            button1.UseVisualStyleBackColor = true;
            button1.Click += CreateJSONB2C;
            // 
            // saveContactsButton
            // 
            saveContactsButton.Dock = DockStyle.Bottom;
            saveContactsButton.Enabled = false;
            saveContactsButton.Location = new Point(3, 85);
            saveContactsButton.Name = "saveContactsButton";
            saveContactsButton.Size = new Size(498, 23);
            saveContactsButton.TabIndex = 5;
            saveContactsButton.Text = "Guardar Contactos";
            saveContactsButton.UseVisualStyleBackColor = true;
            saveContactsButton.Click += saveContactsButton_Click;
            // 
            // companyImport
            // 
            companyImport.Dock = DockStyle.Bottom;
            companyImport.Location = new Point(3, 108);
            companyImport.Name = "companyImport";
            companyImport.Size = new Size(498, 23);
            companyImport.TabIndex = 0;
            companyImport.Text = "Importar Compañias";
            companyImport.UseVisualStyleBackColor = true;
            companyImport.Click += companyImport_Click;
            // 
            // saveCompany
            // 
            saveCompany.Dock = DockStyle.Bottom;
            saveCompany.Enabled = false;
            saveCompany.Location = new Point(3, 131);
            saveCompany.Name = "saveCompany";
            saveCompany.Size = new Size(498, 23);
            saveCompany.TabIndex = 4;
            saveCompany.Text = "Guardar Compañias";
            saveCompany.UseVisualStyleBackColor = true;
            saveCompany.Click += saveCompany_Click;
            // 
            // contactImport
            // 
            contactImport.Dock = DockStyle.Bottom;
            contactImport.Location = new Point(3, 154);
            contactImport.Name = "contactImport";
            contactImport.Size = new Size(498, 23);
            contactImport.TabIndex = 1;
            contactImport.Text = "Importar Contactos";
            contactImport.UseVisualStyleBackColor = true;
            contactImport.Click += contactImport_Click;
            // 
            // backgroundWorkerCompany
            // 
            backgroundWorkerCompany.WorkerReportsProgress = true;
            backgroundWorkerCompany.WorkerSupportsCancellation = true;
            backgroundWorkerCompany.DoWork += backgroundWorker_DoWork;
            backgroundWorkerCompany.ProgressChanged += backgroundWorker_ProgressChanged;
            backgroundWorkerCompany.RunWorkerCompleted += backgroundWorkerCompany_RunWorkerCompleted;
            // 
            // backgroundWorkerContact
            // 
            backgroundWorkerContact.WorkerReportsProgress = true;
            backgroundWorkerContact.WorkerSupportsCancellation = true;
            backgroundWorkerContact.DoWork += backgroundWorkerContact_DoWork;
            backgroundWorkerContact.ProgressChanged += backgroundWorkerContact_ProgressChanged;
            backgroundWorkerContact.RunWorkerCompleted += backgroundWorkerContact_RunWorkerCompleted;
            // 
            // backgroundWorker_Modelo_Soportes
            // 
            backgroundWorker_Modelo_Soportes.WorkerReportsProgress = true;
            backgroundWorker_Modelo_Soportes.WorkerSupportsCancellation = true;
            backgroundWorker_Modelo_Soportes.DoWork += backgroundWorker_Modelo_Soportes_DoWork;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(report_log);
            groupBox4.Controls.Add(dataGridView1);
            groupBox4.Dock = DockStyle.Fill;
            groupBox4.Location = new Point(0, 202);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(1360, 376);
            groupBox4.TabIndex = 1;
            groupBox4.TabStop = false;
            // 
            // report_log
            // 
            report_log.Location = new Point(515, -93);
            report_log.Name = "report_log";
            report_log.Size = new Size(837, 87);
            report_log.TabIndex = 5;
            report_log.Text = "";
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(3, 19);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 62;
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(1354, 354);
            dataGridView1.TabIndex = 4;
            dataGridView1.KeyDown += dataGridView1_KeyDown;
            // 
            // PowerAppForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1360, 578);
            Controls.Add(groupBox4);
            Controls.Add(groupBox1);
            Name = "PowerAppForm";
            Text = "Power App Integration";
            groupBox1.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Button contactImport;
        private Button companyImport;
        private System.ComponentModel.BackgroundWorker backgroundWorkerCompany;
        private Button saveCompany;
        private GroupBox groupBox2;
        private Button saveContactsButton;
        private GroupBox groupBox3;
        private ProgressBar companyProgressBar;
        private System.ComponentModel.BackgroundWorker backgroundWorkerContact;
        private Label label1;
        private ProgressBar contactProgressBar;
        private Button button1;
        private Button button2;
        private System.ComponentModel.BackgroundWorker backgroundWorker_Modelo_Soportes;
        private ProgressBar progressBar_Email_model;
        private GroupBox groupBox4;
        private RichTextBox report_log;
        private DataGridView dataGridView1;
        private Button button3;
    }
}