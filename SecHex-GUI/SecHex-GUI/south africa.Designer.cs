namespace SecHex_GUI
{
    partial class south_africa
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(south_africa));
            siticoneControlBox2 = new Siticone.Desktop.UI.WinForms.SiticoneControlBox();
            siticoneControlBox1 = new Siticone.Desktop.UI.WinForms.SiticoneControlBox();
            sechex = new Label();
            dns = new Siticone.Desktop.UI.WinForms.SiticoneCheckBox();
            spoofall = new Siticone.Desktop.UI.WinForms.SiticoneButton();
            winlogs = new Siticone.Desktop.UI.WinForms.SiticoneCheckBox();
            tempclear = new Siticone.Desktop.UI.WinForms.SiticoneCheckBox();
            wintemp = new Siticone.Desktop.UI.WinForms.SiticoneCheckBox();
            tcp = new Siticone.Desktop.UI.WinForms.SiticoneCheckBox();
            cookie = new Siticone.Desktop.UI.WinForms.SiticoneCheckBox();
            docs = new Siticone.Desktop.UI.WinForms.SiticoneCheckBox();
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
            siticoneControlBox2.Location = new Point(289, 12);
            siticoneControlBox2.Name = "siticoneControlBox2";
            siticoneControlBox2.Size = new Size(48, 29);
            siticoneControlBox2.TabIndex = 3;
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
            siticoneControlBox1.Location = new Point(343, 12);
            siticoneControlBox1.Name = "siticoneControlBox1";
            siticoneControlBox1.Size = new Size(48, 29);
            siticoneControlBox1.TabIndex = 4;
            // 
            // sechex
            // 
            sechex.AutoSize = true;
            sechex.BackColor = Color.Transparent;
            sechex.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            sechex.ForeColor = Color.White;
            sechex.Location = new Point(12, 9);
            sechex.Name = "sechex";
            sechex.Size = new Size(130, 21);
            sechex.TabIndex = 5;
            sechex.Text = "SecHex - Cleaner";
            // 
            // dns
            // 
            dns.AutoSize = true;
            dns.BackColor = Color.Transparent;
            dns.CheckedState.BorderColor = Color.Gray;
            dns.CheckedState.BorderRadius = 3;
            dns.CheckedState.BorderThickness = 0;
            dns.CheckedState.FillColor = Color.Gray;
            dns.Font = new Font("Segoe UI", 9F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            dns.ForeColor = Color.White;
            dns.Location = new Point(12, 49);
            dns.Name = "dns";
            dns.Size = new Size(80, 19);
            dns.TabIndex = 6;
            dns.Text = "FlushDNS";
            dns.UncheckedState.BorderColor = Color.FromArgb(125, 137, 149);
            dns.UncheckedState.BorderRadius = 3;
            dns.UncheckedState.BorderThickness = 0;
            dns.UncheckedState.FillColor = Color.Gray;
            dns.UseVisualStyleBackColor = false;
            dns.CheckedChanged += dns_CheckedChanged;
            // 
            // spoofall
            // 
            spoofall.Animated = true;
            spoofall.AutoRoundedCorners = true;
            spoofall.BackColor = Color.Transparent;
            spoofall.BorderColor = Color.White;
            spoofall.BorderRadius = 15;
            spoofall.BorderThickness = 1;
            spoofall.CustomBorderColor = Color.Transparent;
            spoofall.DisabledState.BorderColor = Color.DarkGray;
            spoofall.DisabledState.CustomBorderColor = Color.DarkGray;
            spoofall.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            spoofall.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            spoofall.FillColor = Color.Transparent;
            spoofall.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            spoofall.ForeColor = Color.White;
            spoofall.Location = new Point(12, 195);
            spoofall.Name = "spoofall";
            spoofall.PressedDepth = 60;
            spoofall.Size = new Size(375, 32);
            spoofall.TabIndex = 18;
            spoofall.Text = "Start Cleaning";
            spoofall.Click += spoofall_Click;
            // 
            // winlogs
            // 
            winlogs.AutoSize = true;
            winlogs.BackColor = Color.Transparent;
            winlogs.CheckedState.BorderColor = Color.Gray;
            winlogs.CheckedState.BorderRadius = 3;
            winlogs.CheckedState.BorderThickness = 0;
            winlogs.CheckedState.FillColor = Color.Gray;
            winlogs.Font = new Font("Segoe UI", 9F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            winlogs.ForeColor = Color.White;
            winlogs.Location = new Point(12, 74);
            winlogs.Name = "winlogs";
            winlogs.Size = new Size(106, 19);
            winlogs.TabIndex = 19;
            winlogs.Text = "Windows Logs";
            winlogs.UncheckedState.BorderColor = Color.FromArgb(125, 137, 149);
            winlogs.UncheckedState.BorderRadius = 3;
            winlogs.UncheckedState.BorderThickness = 0;
            winlogs.UncheckedState.FillColor = Color.Gray;
            winlogs.UseVisualStyleBackColor = false;
            winlogs.CheckedChanged += winlogs_CheckedChanged;
            // 
            // tempclear
            // 
            tempclear.AutoSize = true;
            tempclear.BackColor = Color.Transparent;
            tempclear.CheckedState.BorderColor = Color.Gray;
            tempclear.CheckedState.BorderRadius = 3;
            tempclear.CheckedState.BorderThickness = 0;
            tempclear.CheckedState.FillColor = Color.Gray;
            tempclear.Font = new Font("Segoe UI", 9F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            tempclear.ForeColor = Color.White;
            tempclear.Location = new Point(12, 99);
            tempclear.Name = "tempclear";
            tempclear.Size = new Size(90, 19);
            tempclear.TabIndex = 20;
            tempclear.Text = "Temp Clear";
            tempclear.UncheckedState.BorderColor = Color.FromArgb(125, 137, 149);
            tempclear.UncheckedState.BorderRadius = 3;
            tempclear.UncheckedState.BorderThickness = 0;
            tempclear.UncheckedState.FillColor = Color.Gray;
            tempclear.UseVisualStyleBackColor = false;
            tempclear.CheckedChanged += tempclear_CheckedChanged;
            // 
            // wintemp
            // 
            wintemp.AutoSize = true;
            wintemp.BackColor = Color.Transparent;
            wintemp.CheckedState.BorderColor = Color.Gray;
            wintemp.CheckedState.BorderRadius = 3;
            wintemp.CheckedState.BorderThickness = 0;
            wintemp.CheckedState.FillColor = Color.Gray;
            wintemp.Font = new Font("Segoe UI", 9F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            wintemp.ForeColor = Color.White;
            wintemp.Location = new Point(12, 124);
            wintemp.Name = "wintemp";
            wintemp.Size = new Size(150, 19);
            wintemp.TabIndex = 21;
            wintemp.Text = "Windows Temp Folder";
            wintemp.UncheckedState.BorderColor = Color.FromArgb(125, 137, 149);
            wintemp.UncheckedState.BorderRadius = 3;
            wintemp.UncheckedState.BorderThickness = 0;
            wintemp.UncheckedState.FillColor = Color.Gray;
            wintemp.UseVisualStyleBackColor = false;
            wintemp.CheckedChanged += wintemp_CheckedChanged;
            // 
            // tcp
            // 
            tcp.AutoSize = true;
            tcp.BackColor = Color.Transparent;
            tcp.CheckedState.BorderColor = Color.Gray;
            tcp.CheckedState.BorderRadius = 3;
            tcp.CheckedState.BorderThickness = 0;
            tcp.CheckedState.FillColor = Color.Gray;
            tcp.Font = new Font("Segoe UI", 9F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            tcp.ForeColor = Color.White;
            tcp.Location = new Point(12, 149);
            tcp.Name = "tcp";
            tcp.Size = new Size(82, 19);
            tcp.TabIndex = 22;
            tcp.Text = "TCP Reset";
            tcp.UncheckedState.BorderColor = Color.FromArgb(125, 137, 149);
            tcp.UncheckedState.BorderRadius = 3;
            tcp.UncheckedState.BorderThickness = 0;
            tcp.UncheckedState.FillColor = Color.Gray;
            tcp.UseVisualStyleBackColor = false;
            tcp.CheckedChanged += tcp_CheckedChanged;
            // 
            // cookie
            // 
            cookie.AutoSize = true;
            cookie.BackColor = Color.Transparent;
            cookie.CheckedState.BorderColor = Color.Gray;
            cookie.CheckedState.BorderRadius = 3;
            cookie.CheckedState.BorderThickness = 0;
            cookie.CheckedState.FillColor = Color.Gray;
            cookie.Font = new Font("Segoe UI", 9F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            cookie.ForeColor = Color.White;
            cookie.Location = new Point(235, 49);
            cookie.Name = "cookie";
            cookie.Size = new Size(156, 19);
            cookie.TabIndex = 23;
            cookie.Text = "Delete Chrome Cookies";
            cookie.UncheckedState.BorderColor = Color.FromArgb(125, 137, 149);
            cookie.UncheckedState.BorderRadius = 3;
            cookie.UncheckedState.BorderThickness = 0;
            cookie.UncheckedState.FillColor = Color.Gray;
            cookie.UseVisualStyleBackColor = false;
            cookie.CheckedChanged += cookie_CheckedChanged;
            // 
            // docs
            // 
            docs.AutoSize = true;
            docs.BackColor = Color.Transparent;
            docs.CheckedState.BorderColor = Color.Gray;
            docs.CheckedState.BorderRadius = 3;
            docs.CheckedState.BorderThickness = 0;
            docs.CheckedState.FillColor = Color.Gray;
            docs.Font = new Font("Segoe UI", 9F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point);
            docs.ForeColor = Color.White;
            docs.Location = new Point(235, 74);
            docs.Name = "docs";
            docs.Size = new Size(165, 19);
            docs.TabIndex = 24;
            docs.Text = "Delete Latest Documents";
            docs.UncheckedState.BorderColor = Color.FromArgb(125, 137, 149);
            docs.UncheckedState.BorderRadius = 3;
            docs.UncheckedState.BorderThickness = 0;
            docs.UncheckedState.FillColor = Color.Gray;
            docs.UseVisualStyleBackColor = false;
            docs.CheckedChanged += docs_CheckedChanged;
            // 
            // south_africa
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.AppWorkspace;
            ClientSize = new Size(403, 245);
            Controls.Add(docs);
            Controls.Add(cookie);
            Controls.Add(tcp);
            Controls.Add(wintemp);
            Controls.Add(tempclear);
            Controls.Add(winlogs);
            Controls.Add(spoofall);
            Controls.Add(dns);
            Controls.Add(sechex);
            Controls.Add(siticoneControlBox1);
            Controls.Add(siticoneControlBox2);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "south_africa";
            Text = "yes";
            Load += south_africa_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Siticone.Desktop.UI.WinForms.SiticoneControlBox siticoneControlBox2;
        private Siticone.Desktop.UI.WinForms.SiticoneControlBox siticoneControlBox1;
        private Label sechex;
        private Siticone.Desktop.UI.WinForms.SiticoneCheckBox dns;
        private Siticone.Desktop.UI.WinForms.SiticoneButton spoofall;
        private Siticone.Desktop.UI.WinForms.SiticoneCheckBox winlogs;
        private Siticone.Desktop.UI.WinForms.SiticoneCheckBox tempclear;
        private Siticone.Desktop.UI.WinForms.SiticoneCheckBox wintemp;
        private Siticone.Desktop.UI.WinForms.SiticoneCheckBox tcp;
        private Siticone.Desktop.UI.WinForms.SiticoneCheckBox cookie;
        private Siticone.Desktop.UI.WinForms.SiticoneCheckBox docs;
    }
}