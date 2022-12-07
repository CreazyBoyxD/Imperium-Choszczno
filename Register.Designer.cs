namespace Imperium_Choszczno
{
    partial class Register
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
            this.registerEmail = new System.Windows.Forms.TextBox();
            this.registerPassword1 = new System.Windows.Forms.TextBox();
            this.registerPassword2 = new System.Windows.Forms.TextBox();
            this.registerl1 = new System.Windows.Forms.Label();
            this.registerl2 = new System.Windows.Forms.Label();
            this.registerl3 = new System.Windows.Forms.Label();
            this.registerOldUser = new System.Windows.Forms.LinkLabel();
            this.registerRegister = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // registerEmail
            // 
            this.registerEmail.Location = new System.Drawing.Point(76, 89);
            this.registerEmail.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.registerEmail.Name = "registerEmail";
            this.registerEmail.Size = new System.Drawing.Size(172, 22);
            this.registerEmail.TabIndex = 1;
            // 
            // registerPassword1
            // 
            this.registerPassword1.Location = new System.Drawing.Point(76, 158);
            this.registerPassword1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.registerPassword1.Name = "registerPassword1";
            this.registerPassword1.PasswordChar = '*';
            this.registerPassword1.Size = new System.Drawing.Size(172, 22);
            this.registerPassword1.TabIndex = 2;
            // 
            // registerPassword2
            // 
            this.registerPassword2.Location = new System.Drawing.Point(76, 230);
            this.registerPassword2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.registerPassword2.Name = "registerPassword2";
            this.registerPassword2.PasswordChar = '*';
            this.registerPassword2.Size = new System.Drawing.Size(172, 22);
            this.registerPassword2.TabIndex = 3;
            // 
            // registerl1
            // 
            this.registerl1.AutoSize = true;
            this.registerl1.Location = new System.Drawing.Point(73, 55);
            this.registerl1.Name = "registerl1";
            this.registerl1.Size = new System.Drawing.Size(79, 16);
            this.registerl1.TabIndex = 3;
            this.registerl1.Text = "Twój e-mail:";
            // 
            // registerl2
            // 
            this.registerl2.AutoSize = true;
            this.registerl2.Location = new System.Drawing.Point(73, 123);
            this.registerl2.Name = "registerl2";
            this.registerl2.Size = new System.Drawing.Size(86, 16);
            this.registerl2.TabIndex = 4;
            this.registerl2.Text = "Twoje hasło:";
            // 
            // registerl3
            // 
            this.registerl3.AutoSize = true;
            this.registerl3.Location = new System.Drawing.Point(73, 196);
            this.registerl3.Name = "registerl3";
            this.registerl3.Size = new System.Drawing.Size(96, 16);
            this.registerl3.TabIndex = 4;
            this.registerl3.Text = "Powtórz hasło:";
            // 
            // registerOldUser
            // 
            this.registerOldUser.AutoSize = true;
            this.registerOldUser.Location = new System.Drawing.Point(65, 315);
            this.registerOldUser.Name = "registerOldUser";
            this.registerOldUser.Size = new System.Drawing.Size(173, 16);
            this.registerOldUser.TabIndex = 5;
            this.registerOldUser.TabStop = true;
            this.registerOldUser.Text = "Masz już konto? Zaloguj się!";
            this.registerOldUser.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.registerOldUser_LinkClicked);
            // 
            // registerRegister
            // 
            this.registerRegister.Location = new System.Drawing.Point(101, 271);
            this.registerRegister.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.registerRegister.Name = "registerRegister";
            this.registerRegister.Size = new System.Drawing.Size(120, 28);
            this.registerRegister.TabIndex = 4;
            this.registerRegister.Text = "Zarejestruj się";
            this.registerRegister.UseVisualStyleBackColor = true;
            this.registerRegister.Click += new System.EventHandler(this.registerRegister_Click);
            // 
            // Register
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(365, 404);
            this.Controls.Add(this.registerRegister);
            this.Controls.Add(this.registerOldUser);
            this.Controls.Add(this.registerl3);
            this.Controls.Add(this.registerl2);
            this.Controls.Add(this.registerl1);
            this.Controls.Add(this.registerPassword2);
            this.Controls.Add(this.registerPassword1);
            this.Controls.Add(this.registerEmail);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Register";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Zarejestruj się";
            this.Load += new System.EventHandler(this.Register_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox registerEmail;
        private System.Windows.Forms.TextBox registerPassword1;
        private System.Windows.Forms.TextBox registerPassword2;
        private System.Windows.Forms.Label registerl1;
        private System.Windows.Forms.Label registerl2;
        private System.Windows.Forms.Label registerl3;
        private System.Windows.Forms.LinkLabel registerOldUser;
        private System.Windows.Forms.Button registerRegister;
    }
}