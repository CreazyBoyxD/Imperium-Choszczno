namespace Imperium_Choszczno
{
    partial class Login
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
            this.components = new System.ComponentModel.Container();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.loginEmail = new System.Windows.Forms.TextBox();
            this.loginPassword = new System.Windows.Forms.TextBox();
            this.loginl1 = new System.Windows.Forms.Label();
            this.loginl2 = new System.Windows.Forms.Label();
            this.ForgotPass = new System.Windows.Forms.LinkLabel();
            this.loginLogin = new System.Windows.Forms.Button();
            this.loginNewUser = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // loginEmail
            // 
            this.loginEmail.Location = new System.Drawing.Point(78, 128);
            this.loginEmail.Name = "loginEmail";
            this.loginEmail.Size = new System.Drawing.Size(172, 22);
            this.loginEmail.TabIndex = 1;
            this.loginEmail.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // loginPassword
            // 
            this.loginPassword.Location = new System.Drawing.Point(78, 191);
            this.loginPassword.Name = "loginPassword";
            this.loginPassword.Size = new System.Drawing.Size(172, 22);
            this.loginPassword.TabIndex = 1;
            // 
            // loginl1
            // 
            this.loginl1.AutoSize = true;
            this.loginl1.Location = new System.Drawing.Point(75, 92);
            this.loginl1.Name = "loginl1";
            this.loginl1.Size = new System.Drawing.Size(131, 16);
            this.loginl1.TabIndex = 2;
            this.loginl1.Text = "Your e-mail address:";
            this.loginl1.Click += new System.EventHandler(this.label1_Click);
            // 
            // loginl2
            // 
            this.loginl2.AutoSize = true;
            this.loginl2.Location = new System.Drawing.Point(75, 163);
            this.loginl2.Name = "loginl2";
            this.loginl2.Size = new System.Drawing.Size(100, 16);
            this.loginl2.TabIndex = 2;
            this.loginl2.Text = "Your password:";
            this.loginl2.Click += new System.EventHandler(this.label1_Click);
            // 
            // ForgotPass
            // 
            this.ForgotPass.AutoSize = true;
            this.ForgotPass.Location = new System.Drawing.Point(75, 309);
            this.ForgotPass.Name = "ForgotPass";
            this.ForgotPass.Size = new System.Drawing.Size(116, 16);
            this.ForgotPass.TabIndex = 3;
            this.ForgotPass.TabStop = true;
            this.ForgotPass.Text = "Forgot Password?";
            this.ForgotPass.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ForgotPass_LinkClicked);
            // 
            // loginLogin
            // 
            this.loginLogin.Location = new System.Drawing.Point(131, 229);
            this.loginLogin.Name = "loginLogin";
            this.loginLogin.Size = new System.Drawing.Size(75, 28);
            this.loginLogin.TabIndex = 4;
            this.loginLogin.Text = "Login";
            this.loginLogin.UseVisualStyleBackColor = true;
            // 
            // loginNewUser
            // 
            this.loginNewUser.AutoSize = true;
            this.loginNewUser.Location = new System.Drawing.Point(75, 274);
            this.loginNewUser.Name = "loginNewUser";
            this.loginNewUser.Size = new System.Drawing.Size(157, 16);
            this.loginNewUser.TabIndex = 5;
            this.loginNewUser.TabStop = true;
            this.loginNewUser.Text = "First time? Register Here!";
            this.loginNewUser.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.loginNewUser_LinkClicked);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.ClientSize = new System.Drawing.Size(365, 404);
            this.Controls.Add(this.loginNewUser);
            this.Controls.Add(this.loginLogin);
            this.Controls.Add(this.ForgotPass);
            this.Controls.Add(this.loginl2);
            this.Controls.Add(this.loginl1);
            this.Controls.Add(this.loginPassword);
            this.Controls.Add(this.loginEmail);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.TextBox loginEmail;
        private System.Windows.Forms.TextBox loginPassword;
        private System.Windows.Forms.Label loginl1;
        private System.Windows.Forms.Label loginl2;
        private System.Windows.Forms.LinkLabel ForgotPass;
        private System.Windows.Forms.Button loginLogin;
        private System.Windows.Forms.LinkLabel loginNewUser;
    }
}