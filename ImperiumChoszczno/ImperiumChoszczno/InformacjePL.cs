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
    public partial class Informacje : Form
    {
        public Informacje()
        {
            InitializeComponent();
        }

        private void buttonBackInf_Click(object sender, EventArgs e)
        {
            Studio Studio = new Studio();
            this.Hide();
            Studio.Show();
        }
    }
}
