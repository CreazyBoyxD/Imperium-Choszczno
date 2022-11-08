using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImperiumChoszczno
{
    public partial class Studio : Form
    {
        public Studio()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Start_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void zalogujSięToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void zalogujSięToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void oNasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Informacje Informacje = new Informacje();
            this.Hide();
            Informacje.Show();
        }

        private void zakończToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void wylogujSięToolStripMenuItem_Click(object sender, EventArgs e)
        {
            login login = new login();
            this.Hide();
            login.Show();
        }
    }
}
