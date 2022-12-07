namespace Imperium_Choszczno
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.panel1 = new System.Windows.Forms.Panel();
            this.mainHistory = new System.Windows.Forms.Button();
            this.mainSettings = new System.Windows.Forms.Button();
            this.mainShop = new System.Windows.Forms.Button();
            this.mainDiscover = new System.Windows.Forms.Button();
            this.mainLibrary = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.mainRegister = new System.Windows.Forms.Label();
            this.mainLogin = new System.Windows.Forms.Label();
            this.mainProfilepic = new System.Windows.Forms.PictureBox();
            this.mainMiddlepanel = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainProfilepic)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(221)))), ((int)(((byte)(226)))));
            this.panel1.Controls.Add(this.mainHistory);
            this.panel1.Controls.Add(this.mainSettings);
            this.panel1.Controls.Add(this.mainShop);
            this.panel1.Controls.Add(this.mainDiscover);
            this.panel1.Controls.Add(this.mainLibrary);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Name = "panel1";
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // mainHistory
            // 
            resources.ApplyResources(this.mainHistory, "mainHistory");
            this.mainHistory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(49)))), ((int)(((byte)(99)))));
            this.mainHistory.Name = "mainHistory";
            this.mainHistory.UseVisualStyleBackColor = true;
            this.mainHistory.Click += new System.EventHandler(this.mainHistory_Click);
            // 
            // mainSettings
            // 
            resources.ApplyResources(this.mainSettings, "mainSettings");
            this.mainSettings.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(49)))), ((int)(((byte)(99)))));
            this.mainSettings.Name = "mainSettings";
            this.mainSettings.UseVisualStyleBackColor = true;
            this.mainSettings.Click += new System.EventHandler(this.mainSettings_Click);
            // 
            // mainShop
            // 
            resources.ApplyResources(this.mainShop, "mainShop");
            this.mainShop.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(49)))), ((int)(((byte)(99)))));
            this.mainShop.Name = "mainShop";
            this.mainShop.UseVisualStyleBackColor = true;
            this.mainShop.Click += new System.EventHandler(this.mainShop_Click);
            // 
            // mainDiscover
            // 
            resources.ApplyResources(this.mainDiscover, "mainDiscover");
            this.mainDiscover.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(49)))), ((int)(((byte)(99)))));
            this.mainDiscover.Name = "mainDiscover";
            this.mainDiscover.UseVisualStyleBackColor = true;
            this.mainDiscover.Click += new System.EventHandler(this.mainDiscover_Click);
            // 
            // mainLibrary
            // 
            resources.ApplyResources(this.mainLibrary, "mainLibrary");
            this.mainLibrary.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(49)))), ((int)(((byte)(99)))));
            this.mainLibrary.Name = "mainLibrary";
            this.mainLibrary.UseVisualStyleBackColor = true;
            this.mainLibrary.Click += new System.EventHandler(this.mainLibrary_Click_1);
            // 
            // panel2
            // 
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.panel2.Controls.Add(this.mainRegister);
            this.panel2.Controls.Add(this.mainLogin);
            this.panel2.Controls.Add(this.mainProfilepic);
            this.panel2.Name = "panel2";
            // 
            // mainRegister
            // 
            resources.ApplyResources(this.mainRegister, "mainRegister");
            this.mainRegister.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(49)))), ((int)(((byte)(99)))));
            this.mainRegister.Name = "mainRegister";
            this.mainRegister.Click += new System.EventHandler(this.mainRegister_Click);
            // 
            // mainLogin
            // 
            resources.ApplyResources(this.mainLogin, "mainLogin");
            this.mainLogin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(49)))), ((int)(((byte)(99)))));
            this.mainLogin.Name = "mainLogin";
            this.mainLogin.Click += new System.EventHandler(this.label1_Click);
            // 
            // mainProfilepic
            // 
            resources.ApplyResources(this.mainProfilepic, "mainProfilepic");
            this.mainProfilepic.Name = "mainProfilepic";
            this.mainProfilepic.TabStop = false;
            this.mainProfilepic.Click += new System.EventHandler(this.mainProfilepic_Click);
            // 
            // mainMiddlepanel
            // 
            resources.ApplyResources(this.mainMiddlepanel, "mainMiddlepanel");
            this.mainMiddlepanel.Name = "mainMiddlepanel";
            this.mainMiddlepanel.Paint += new System.Windows.Forms.PaintEventHandler(this.mainMiddlepanel_Paint);
            // 
            // Main
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mainMiddlepanel);
            this.Controls.Add(this.panel1);
            this.Name = "Main";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainProfilepic)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox mainProfilepic;
        private System.Windows.Forms.Button mainSettings;
        private System.Windows.Forms.Button mainShop;
        private System.Windows.Forms.Button mainDiscover;
        private System.Windows.Forms.Button mainLibrary;
        private System.Windows.Forms.Label mainLogin;
        private System.Windows.Forms.Label mainRegister;
        private System.Windows.Forms.Button mainHistory;
        public System.Windows.Forms.Panel mainMiddlepanel;
    }
}

