using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
    }
}
