using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Imperium_Choszczno
{
    public partial class Login : Form
    {
        Register rg = new Register();
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
            rg.Show();
            Application.Exit();
        }

        private void loginLogin_Click(object sender, EventArgs e)
        {
            if (isValid())
            {
                using (SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=""D:\Imperium Choszczno\BazaDanych.mdf"";Integrated Security=True"))
                {
                    string query = "SELECT * FROM Login WHERE Username = '" + loginEmail.Text.Trim() +
                         "' AND PASSWORD = '" + loginPassword.Text.Trim() + "'";
                    SqlDataAdapter sda = new SqlDataAdapter(query, conn);
                    DataTable dta = new DataTable();
                    sda.Fill(dta);
                    if (dta.Rows.Count == 1)
                    {
                        /* co ma robić logowanie */
                    }
                }
            }
        }
    }
}
