namespace SecHex_GUI
{
    partial class logs
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(logs));
            siticoneControlBox2 = new Siticone.Desktop.UI.WinForms.SiticoneControlBox();
            siticoneControlBox1 = new Siticone.Desktop.UI.WinForms.SiticoneControlBox();
            sechex = new Label();
            richTextBoxLogs = new RichTextBox();
            SuspendLayout();
            // 
            // siticoneControlBox2
            // 
            siticoneControlBox2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            siticoneControlBox2.Animated = true;
            siticoneControlBox2.BackColor = Color.Transparent;
            siticoneControlBox2.BorderColor = Color.White;
            siticoneControlBox2.BorderRadius = 12;
            siticoneControlBox2.ControlBoxType = Siticone.Desktop.UI.WinForms.Enums.ControlBoxType.MinimizeBox;
            siticoneControlBox2.FillColor = Color.Transparent;
            siticoneControlBox2.IconColor = Color.White;
            siticoneControlBox2.Location = new Point(416, 12);
            siticoneControlBox2.Name = "siticoneControlBox2";
            siticoneControlBox2.Size = new Size(48, 29);
            siticoneControlBox2.TabIndex = 2;
            // 
            // siticoneControlBox1
            // 
            siticoneControlBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            siticoneControlBox1.Animated = true;
            siticoneControlBox1.BackColor = Color.Transparent;
            siticoneControlBox1.BorderColor = Color.White;
            siticoneControlBox1.BorderRadius = 12;
            siticoneControlBox1.FillColor = Color.Transparent;
            siticoneControlBox1.IconColor = Color.White;
            siticoneControlBox1.Location = new Point(470, 12);
            siticoneControlBox1.Name = "siticoneControlBox1";
            siticoneControlBox1.Size = new Size(48, 29);
            siticoneControlBox1.TabIndex = 3;
            // 
            // sechex
            // 
            sechex.AutoSize = true;
            sechex.BackColor = Color.Transparent;
            sechex.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            sechex.ForeColor = Color.White;
            sechex.Location = new Point(12, 9);
            sechex.Name = "sechex";
            sechex.Size = new Size(108, 21);
            sechex.TabIndex = 4;
            sechex.Text = "SecHex - Logs";
            sechex.Click += sechex_Click;
            // 
            // richTextBoxLogs
            // 
            richTextBoxLogs.BackColor = Color.FromArgb(17, 17, 17);
            richTextBoxLogs.BorderStyle = BorderStyle.None;
            richTextBoxLogs.Location = new Point(12, 47);
            richTextBoxLogs.Name = "richTextBoxLogs";
            richTextBoxLogs.ReadOnly = true;
            richTextBoxLogs.Size = new Size(506, 328);
            richTextBoxLogs.TabIndex = 5;
            richTextBoxLogs.Text = "";
            richTextBoxLogs.TextChanged += richTextBoxLogs_TextChanged;
            // 
            // logs
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(17, 17, 17);
            ClientSize = new Size(530, 387);
            Controls.Add(richTextBoxLogs);
            Controls.Add(sechex);
            Controls.Add(siticoneControlBox1);
            Controls.Add(siticoneControlBox2);
            ForeColor = Color.FromArgb(17, 17, 17);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "logs";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "hexhex";
            Load += hexhex_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Siticone.Desktop.UI.WinForms.SiticoneControlBox siticoneControlBox2;
        private Siticone.Desktop.UI.WinForms.SiticoneControlBox siticoneControlBox1;
        private Label sechex;
        private RichTextBox richTextBoxLogs;
    }
}