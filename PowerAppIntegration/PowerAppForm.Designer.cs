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
            groupBox1.Margin = new Padding(4, 5, 4, 5);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4, 5, 4, 5);
            groupBox1.Size = new Size(1924, 337);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(ProcesarProgressBar);
            groupBox3.Controls.Add(label1);
            groupBox3.Controls.Add(LimpiarProgressBar);
            groupBox3.Dock = DockStyle.Fill;
            groupBox3.Location = new Point(724, 29);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(1196, 303);
            groupBox3.TabIndex = 6;
            groupBox3.TabStop = false;
            // 
            // ProcesarProgressBar
            // 
            ProcesarProgressBar.Dock = DockStyle.Top;
            ProcesarProgressBar.Location = new Point(3, 60);
            ProcesarProgressBar.Margin = new Padding(4, 5, 4, 5);
            ProcesarProgressBar.Name = "ProcesarProgressBar";
            ProcesarProgressBar.Size = new Size(1190, 38);
            ProcesarProgressBar.TabIndex = 8;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Dock = DockStyle.Bottom;
            label1.Location = new Point(3, 275);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(0, 25);
            label1.TabIndex = 7;
            // 
            // LimpiarProgressBar
            // 
            LimpiarProgressBar.Dock = DockStyle.Top;
            LimpiarProgressBar.Location = new Point(3, 27);
            LimpiarProgressBar.Name = "LimpiarProgressBar";
            LimpiarProgressBar.Size = new Size(1190, 33);
            LimpiarProgressBar.TabIndex = 6;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(button3);
            groupBox2.Controls.Add(button2);
            groupBox2.Controls.Add(button1);
            groupBox2.Dock = DockStyle.Left;
            groupBox2.Location = new Point(4, 29);
            groupBox2.Margin = new Padding(4, 5, 4, 5);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(4, 5, 4, 5);
            groupBox2.Size = new Size(720, 303);
            groupBox2.TabIndex = 5;
            groupBox2.TabStop = false;
            // 
            // button3
            // 
            button3.Dock = DockStyle.Bottom;
            button3.Location = new Point(4, 184);
            button3.Margin = new Padding(4, 5, 4, 5);
            button3.Name = "button3";
            button3.Size = new Size(712, 38);
            button3.TabIndex = 8;
            button3.Text = "Importar Datos";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button2
            // 
            button2.Dock = DockStyle.Bottom;
            button2.Location = new Point(4, 222);
            button2.Margin = new Padding(4, 5, 4, 5);
            button2.Name = "button2";
            button2.Size = new Size(712, 38);
            button2.TabIndex = 7;
            button2.Text = "Limpiar Datos";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button1
            // 
            button1.Dock = DockStyle.Bottom;
            button1.Location = new Point(4, 260);
            button1.Margin = new Padding(4, 5, 4, 5);
            button1.Name = "button1";
            button1.Size = new Size(712, 38);
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
            groupBox4.Location = new Point(0, 337);
            groupBox4.Margin = new Padding(4, 5, 4, 5);
            groupBox4.Name = "groupBox4";
            groupBox4.Padding = new Padding(4, 5, 4, 5);
            groupBox4.Size = new Size(1924, 626);
            groupBox4.TabIndex = 1;
            groupBox4.TabStop = false;
            // 
            // report_log
            // 
            report_log.Location = new Point(736, -155);
            report_log.Margin = new Padding(4, 5, 4, 5);
            report_log.Name = "report_log";
            report_log.Size = new Size(1194, 142);
            report_log.TabIndex = 5;
            report_log.Text = "";
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(4, 29);
            dataGridView1.Margin = new Padding(4, 5, 4, 5);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 62;
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(1916, 592);
            dataGridView1.TabIndex = 4;
            dataGridView1.KeyDown += dataGridView1_KeyDown;
            // 
            // PowerAppForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1924, 963);
            Controls.Add(groupBox4);
            Controls.Add(groupBox1);
            Margin = new Padding(4, 5, 4, 5);
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
    }
}