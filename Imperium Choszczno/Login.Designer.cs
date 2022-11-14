using System.Windows.Forms;

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
            this.loginEmail.Location = new System.Drawing.Point(58, 104);
            this.loginEmail.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.loginEmail.Name = "loginEmail";
            this.loginEmail.Size = new System.Drawing.Size(130, 20);
            this.loginEmail.TabIndex = 1;
            this.loginEmail.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // loginPassword
            // 
            this.loginPassword.Location = new System.Drawing.Point(58, 155);
            this.loginPassword.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.loginPassword.Name = "loginPassword";
            this.loginPassword.Size = new System.Drawing.Size(130, 20);
            this.loginPassword.TabIndex = 1;
            // 
            // loginl1
            // 
            this.loginl1.AutoSize = true;
            this.loginl1.Location = new System.Drawing.Point(56, 75);
            this.loginl1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.loginl1.Name = "loginl1";
            this.loginl1.Size = new System.Drawing.Size(102, 13);
            this.loginl1.TabIndex = 2;
            this.loginl1.Text = "Your e-mail address:";
            this.loginl1.Click += new System.EventHandler(this.label1_Click);
            // 
            // loginl2
            // 
            this.loginl2.AutoSize = true;
            this.loginl2.Location = new System.Drawing.Point(56, 132);
            this.loginl2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.loginl2.Name = "loginl2";
            this.loginl2.Size = new System.Drawing.Size(80, 13);
            this.loginl2.TabIndex = 2;
            this.loginl2.Text = "Your password:";
            this.loginl2.Click += new System.EventHandler(this.label1_Click);
            // 
            // ForgotPass
            // 
            this.ForgotPass.AutoSize = true;
            this.ForgotPass.Location = new System.Drawing.Point(56, 251);
            this.ForgotPass.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.ForgotPass.Name = "ForgotPass";
            this.ForgotPass.Size = new System.Drawing.Size(92, 13);
            this.ForgotPass.TabIndex = 3;
            this.ForgotPass.TabStop = true;
            this.ForgotPass.Text = "Forgot Password?";
            this.ForgotPass.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ForgotPass_LinkClicked);
            // 
            // loginLogin
            // 
            this.loginLogin.Location = new System.Drawing.Point(98, 186);
            this.loginLogin.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.loginLogin.Name = "loginLogin";
            this.loginLogin.Size = new System.Drawing.Size(56, 23);
            this.loginLogin.TabIndex = 4;
            this.loginLogin.Text = "Login";
            this.loginLogin.UseVisualStyleBackColor = true;
            this.loginLogin.Click += new System.EventHandler(this.loginLogin_Click);
            // 
            // loginNewUser
            // 
            this.loginNewUser.AutoSize = true;
            this.loginNewUser.Location = new System.Drawing.Point(56, 223);
            this.loginNewUser.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.loginNewUser.Name = "loginNewUser";
            this.loginNewUser.Size = new System.Drawing.Size(125, 13);
            this.loginNewUser.TabIndex = 5;
            this.loginNewUser.TabStop = true;
            this.loginNewUser.Text = "First time? Register Here!";
            this.loginNewUser.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.loginNewUser_LinkClicked);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.ClientSize = new System.Drawing.Size(274, 328);
            this.Controls.Add(this.loginNewUser);
            this.Controls.Add(this.loginLogin);
            this.Controls.Add(this.ForgotPass);
            this.Controls.Add(this.loginl2);
            this.Controls.Add(this.loginl1);
            this.Controls.Add(this.loginPassword);
            this.Controls.Add(this.loginEmail);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
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

        private bool isValid()
        {
            if (loginEmail.Text.TrimStart() == string.Empty)
            {
                MessageBox.Show("Enter valid e-mail please!", "Error!");
                return false;
            } else if (loginPassword.Text.TrimStart() == string.Empty)
            {
                MessageBox.Show("Enter valid password please!", "Error!");
                return false;
            }
            return true;
        }
    }
}