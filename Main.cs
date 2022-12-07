using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySqlConnector;

namespace Imperium_Choszczno
{
    public partial class Main : Form
    {

        public Main()
        {
            InitializeComponent();

            Start start = new Start() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            start.FormBorderStyle = FormBorderStyle.None;
            this.mainMiddlepanel.Controls.Add(start);
            start.Show();

            
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Login ln = new Login();
            ln.ShowDialog();
        }

        private void mainWallet_Click(object sender, EventArgs e)
        {

        }

        private void mainProfilepic_Click(object sender, EventArgs e)
        {

        }

        private void mainlibrary_Click(object sender, EventArgs e)
        {

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

        public void mainLibrary_Click_1(object sender, EventArgs e)
        {
            this.mainMiddlepanel.Controls.Clear();
            Library library = new Library() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            library.FormBorderStyle= FormBorderStyle.None;
            this.mainMiddlepanel.Controls.Add(library);
            library.Show();
        }

        private void mainHistory_Click(object sender, EventArgs e)
        {
            this.mainMiddlepanel.Controls.Clear();
            History history = new History() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            history.FormBorderStyle = FormBorderStyle.None;
            this.mainMiddlepanel.Controls.Add(history);
            history.Show();
        }

        private void mainSettings_Click(object sender, EventArgs e)
        {
            Settings Settings = new Settings();
            Settings.Show();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void mainDiscover_Click(object sender, EventArgs e)
        {
            this.mainMiddlepanel.Controls.Clear();
            Discover discover = new Discover() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            discover.FormBorderStyle = FormBorderStyle.None;
            this.mainMiddlepanel.Controls.Add(discover);
            discover.Show();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void mainMiddlepanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

            
        }

        private void label3_Click(object sender, EventArgs e)
        {
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void mainShop_Click(object sender, EventArgs e)
        {
            this.mainMiddlepanel.Controls.Clear();
            Shop shop = new Shop() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            shop.FormBorderStyle = FormBorderStyle.None;
            this.mainMiddlepanel.Controls.Add(shop);
            shop.Show();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
