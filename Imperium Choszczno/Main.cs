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
    public partial class Main : Form
    {
       
        public Main()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Login ln = new Login();
            ln.Show();
        }

        private void mainWallet_Click(object sender, EventArgs e)
        {

        }

        private void mainProfilepic_Click(object sender, EventArgs e)
        {

        }

        private void mainlibrary_Click(object sender, EventArgs e)
        {
         /*   this.mainMiddlepanel.Controls.Clear();
            menu mn = new menu() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            this.mainMiddlepanel.Controls.Add(mn);
            mn.Show();*/
        }

        private void mainRegister_Click(object sender, EventArgs e)
        {
            Register rg = new Register();
            rg.Show();
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void mainLibrary_Click_1(object sender, EventArgs e)
        {

        }

        private void mainHistory_Click(object sender, EventArgs e)
        {

        }
    }
}
