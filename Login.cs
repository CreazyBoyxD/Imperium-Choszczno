using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;

namespace Imperium_Choszczno
{
    public partial class Login : Form
    {

        public Login()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void ForgotPass_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void loginNewUser_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            Register rg = new Register();
            rg.Show();
        }

        public void loginLogin_Click(object sender, EventArgs e)
        {

            if (isValid())
            {
                using (MySqlConnection cn = new MySqlConnection("SERVER=192.166.219.220;DATABASE=imperium;UID=imperium;Password=imperium1212"))

                {
                    string query = "SELECT * FROM Users WHERE email = '" + loginEmail.Text.Trim() +
                         "' AND password = '" + loginPassword.Text.Trim() + "'";
                    MySqlDataAdapter sda = new MySqlDataAdapter(query, cn);
                    DataTable dta = new DataTable();
                    sda.Fill(dta);
                    if (dta.Rows.Count == 1)
                    {
                        this.Hide();
                        MessageBox.Show("Logowanie przebiegło pomyślnie!");
                    }
                }
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void loginPassword_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
