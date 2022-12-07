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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using ComboBox = System.Windows.Forms.ComboBox;

namespace Imperium_Choszczno
{
    public partial class Settings : Form
    {

        public Settings()
        {
            InitializeComponent();

        }

        private void Settings_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxEnglish_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxPolish_CheckedChanged(object sender, EventArgs e)
        {

        }

        public void comboBoxLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBoxLanguage.SelectedIndex)
            {
                case 0:
                    CultureInfo pl = new CultureInfo("pl");
                    Thread.CurrentThread.CurrentCulture = pl;
                    Thread.CurrentThread.CurrentUICulture = pl;

                    this.Controls.Clear();
                    InitializeComponent();
                    break;
                case 1:
                    CultureInfo en = new CultureInfo("en");
                    Thread.CurrentThread.CurrentCulture = en;
                    Thread.CurrentThread.CurrentUICulture = en;
                    this.Controls.Clear();
                    InitializeComponent();
                    break;

                    //How do i make a button click in one form change an option in another form? C#
            }
  
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
    {


        switch (comboBox2.SelectedIndex)
        {
            case 0:
                    this.BackColor = Color.White;

                break;
            case 1:
                    this.BackColor = Color.Gray;

                    break;
        }
    }
}
}
