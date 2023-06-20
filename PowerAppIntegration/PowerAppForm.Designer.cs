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
            ProcesarProgressBar = new ProgressBar();
            label1 = new Label();
            LimpiarProgressBar = new ProgressBar();
            groupBox2 = new GroupBox();
            button3 = new Button();
            button2 = new Button();
            button1 = new Button();
            backgroundWorkerLimpiarDatos = new System.ComponentModel.BackgroundWorker();
            backgroundWorkerProcesarDatos = new System.ComponentModel.BackgroundWorker();
            groupBox4 = new GroupBox();
            report_log = new RichTextBox();
            dataGridView1 = new DataGridView();
            reporteLogs = new RichTextBox();
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
            groupBox1.Size = new Size(1123, 202);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(reporteLogs);
            groupBox3.Controls.Add(ProcesarProgressBar);
            groupBox3.Controls.Add(label1);
            groupBox3.Controls.Add(LimpiarProgressBar);
            groupBox3.Dock = DockStyle.Fill;
            groupBox3.Location = new Point(507, 19);
            groupBox3.Margin = new Padding(2, 2, 2, 2);
            groupBox3.Name = "groupBox3";
            groupBox3.Padding = new Padding(2, 2, 2, 2);
            groupBox3.Size = new Size(613, 180);
            groupBox3.TabIndex = 6;
            groupBox3.TabStop = false;
            // 
            // ProcesarProgressBar
            // 
            ProcesarProgressBar.Dock = DockStyle.Top;
            ProcesarProgressBar.Location = new Point(2, 38);
            ProcesarProgressBar.Name = "ProcesarProgressBar";
            ProcesarProgressBar.Size = new Size(609, 23);
            ProcesarProgressBar.TabIndex = 8;
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
            // LimpiarProgressBar
            // 
            LimpiarProgressBar.Dock = DockStyle.Top;
            LimpiarProgressBar.Location = new Point(2, 18);
            LimpiarProgressBar.Margin = new Padding(2, 2, 2, 2);
            LimpiarProgressBar.Name = "LimpiarProgressBar";
            LimpiarProgressBar.Size = new Size(609, 20);
            LimpiarProgressBar.TabIndex = 6;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(button3);
            groupBox2.Controls.Add(button2);
            groupBox2.Controls.Add(button1);
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
            button3.Location = new Point(3, 108);
            button3.Name = "button3";
            button3.Size = new Size(498, 23);
            button3.TabIndex = 8;
            button3.Text = "Importar Datos";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button2
            // 
            button2.Dock = DockStyle.Bottom;
            button2.Location = new Point(3, 131);
            button2.Name = "button2";
            button2.Size = new Size(498, 23);
            button2.TabIndex = 7;
            button2.Text = "Limpiar Datos";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button1
            // 
            button1.Dock = DockStyle.Bottom;
            button1.Location = new Point(3, 154);
            button1.Name = "button1";
            button1.Size = new Size(498, 23);
            button1.TabIndex = 6;
            button1.Text = "Procesar Datos";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // backgroundWorkerLimpiarDatos
            // 
            backgroundWorkerLimpiarDatos.WorkerReportsProgress = true;
            backgroundWorkerLimpiarDatos.WorkerSupportsCancellation = true;
            backgroundWorkerLimpiarDatos.DoWork += backgroundWorkerLimpiarDatos_DoWork;
            backgroundWorkerLimpiarDatos.ProgressChanged += backgroundWorker_ProgressChanged;
            // 
            // backgroundWorkerProcesarDatos
            // 
            backgroundWorkerProcesarDatos.WorkerReportsProgress = true;
            backgroundWorkerProcesarDatos.WorkerSupportsCancellation = true;
            backgroundWorkerProcesarDatos.DoWork += backgroundWorkerProcesarDatos_DoWork;
            backgroundWorkerProcesarDatos.ProgressChanged += backgroundWorkerContact_ProgressChanged;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(report_log);
            groupBox4.Controls.Add(dataGridView1);
            groupBox4.Dock = DockStyle.Fill;
            groupBox4.Location = new Point(0, 202);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(1123, 327);
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
            dataGridView1.Size = new Size(1117, 305);
            dataGridView1.TabIndex = 4;
            dataGridView1.KeyDown += dataGridView1_KeyDown;
            // 
            // reporteLogs
            // 
            reporteLogs.BackColor = SystemColors.MenuText;
            reporteLogs.Dock = DockStyle.Bottom;
            reporteLogs.Location = new Point(2, 67);
            reporteLogs.Name = "reporteLogs";
            reporteLogs.Size = new Size(609, 96);
            reporteLogs.TabIndex = 9;
            reporteLogs.Text = "";
            // 
            // PowerAppForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1123, 529);
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
        private System.ComponentModel.BackgroundWorker backgroundWorkerLimpiarDatos;
        private GroupBox groupBox3;
        private ProgressBar LimpiarProgressBar;
        private System.ComponentModel.BackgroundWorker backgroundWorkerProcesarDatos;
        private Label label1;
        private ProgressBar ProcesarProgressBar;
        private GroupBox groupBox4;
        private RichTextBox report_log;
        private DataGridView dataGridView1;
        private GroupBox groupBox2;
        private Button button3;
        private Button button2;
        private Button button1;
        private RichTextBox reporteLogs;
    }
}