namespace SMTPCodeGenerator
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.RTBDBPart = new System.Windows.Forms.RichTextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.BTNSearchDataBase = new System.Windows.Forms.Button();
            this.BTNGenerate = new System.Windows.Forms.Button();
            this.SSTMainStatusBar = new System.Windows.Forms.StatusStrip();
            this.TSLStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.TSPBMainProgresBar = new System.Windows.Forms.ToolStripProgressBar();
            this.label3 = new System.Windows.Forms.Label();
            this.tbDBType = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SSTMainStatusBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(116, 48);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(382, 21);
            this.comboBox1.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(13, 126);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(668, 418);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(660, 392);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "StoresProcedures";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.Controls.Add(this.RTBDBPart);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(654, 386);
            this.panel1.TabIndex = 0;
            // 
            // RTBDBPart
            // 
            this.RTBDBPart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RTBDBPart.Location = new System.Drawing.Point(0, 0);
            this.RTBDBPart.Name = "RTBDBPart";
            this.RTBDBPart.Size = new System.Drawing.Size(654, 386);
            this.RTBDBPart.TabIndex = 0;
            this.RTBDBPart.Text = "";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.panel2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(660, 392);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "CC Codes";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(654, 386);
            this.panel2.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Select Table :";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Select  Connection: ";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(116, 22);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(382, 20);
            this.textBox1.TabIndex = 4;
            // 
            // BTNSearchDataBase
            // 
            this.BTNSearchDataBase.Location = new System.Drawing.Point(504, 20);
            this.BTNSearchDataBase.Name = "BTNSearchDataBase";
            this.BTNSearchDataBase.Size = new System.Drawing.Size(170, 23);
            this.BTNSearchDataBase.TabIndex = 5;
            this.BTNSearchDataBase.Text = "Search DBFile";
            this.BTNSearchDataBase.UseVisualStyleBackColor = true;
            this.BTNSearchDataBase.Click += new System.EventHandler(this.button1_Click);
            // 
            // BTNGenerate
            // 
            this.BTNGenerate.Location = new System.Drawing.Point(504, 91);
            this.BTNGenerate.Name = "BTNGenerate";
            this.BTNGenerate.Size = new System.Drawing.Size(170, 23);
            this.BTNGenerate.TabIndex = 6;
            this.BTNGenerate.Text = "Generate";
            this.BTNGenerate.UseVisualStyleBackColor = true;
            this.BTNGenerate.Click += new System.EventHandler(this.button2_Click);
            // 
            // SSTMainStatusBar
            // 
            this.SSTMainStatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSLStatusLabel,
            this.TSPBMainProgresBar});
            this.SSTMainStatusBar.Location = new System.Drawing.Point(0, 552);
            this.SSTMainStatusBar.Name = "SSTMainStatusBar";
            this.SSTMainStatusBar.Size = new System.Drawing.Size(693, 22);
            this.SSTMainStatusBar.TabIndex = 7;
            // 
            // TSLStatusLabel
            // 
            this.TSLStatusLabel.Name = "TSLStatusLabel";
            this.TSLStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // TSPBMainProgresBar
            // 
            this.TSPBMainProgresBar.Name = "TSPBMainProgresBar";
            this.TSPBMainProgresBar.Size = new System.Drawing.Size(100, 16);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(517, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Selected DBType :";
            // 
            // tbDBType
            // 
            this.tbDBType.Location = new System.Drawing.Point(612, 48);
            this.tbDBType.Name = "tbDBType";
            this.tbDBType.ReadOnly = true;
            this.tbDBType.Size = new System.Drawing.Size(62, 20);
            this.tbDBType.TabIndex = 9;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(693, 574);
            this.Controls.Add(this.tbDBType);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.SSTMainStatusBar);
            this.Controls.Add(this.BTNGenerate);
            this.Controls.Add(this.BTNSearchDataBase);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.comboBox1);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SOFT-MOVIL Codes Generator";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.SSTMainStatusBar.ResumeLayout(false);
            this.SSTMainStatusBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button BTNSearchDataBase;
        private System.Windows.Forms.Button BTNGenerate;
        private System.Windows.Forms.StatusStrip SSTMainStatusBar;
        private System.Windows.Forms.ToolStripProgressBar TSPBMainProgresBar;
        private System.Windows.Forms.RichTextBox RTBDBPart;
        private System.Windows.Forms.ToolStripStatusLabel TSLStatusLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbDBType;
    }
}