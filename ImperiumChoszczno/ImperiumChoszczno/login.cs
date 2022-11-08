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

namespace ImperiumChoszczno
{
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void textLogin_TextChanged(object sender, EventArgs e)
        {

        }

        private void wyjdźToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private bool isValid()
        {
            if (textLogin.Text.TrimStart() == String.Empty)
            {
                MessageBox.Show("Wprowadź prawidłowy login!", "Błąd!");
                return false;
            } else if (textHaslo.Text.TrimStart() == String.Empty)
            {
                MessageBox.Show("Wprowadź prawidłowy hasło!", "Błąd!");
                return false;
            }
            return true;
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            if (isValid())
            {
                using (SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\hubi_\source\repos\ImperiumChoszczno\ImperiumChoszczno\Database1.mdf;Integrated Security=True"))
                {
                    string query = "SELECT * FROM Login WHERE Username = '" + textLogin.Text.Trim() +
                         "' AND PASSWORD = '" + textHaslo.Text.Trim() + "'";
                    SqlDataAdapter sda = new SqlDataAdapter(query, conn);
                    DataTable dta = new DataTable();
                    sda.Fill(dta);
                    if (dta.Rows.Count == 1)
                    {
                        Studio Studio = new Studio();
                        this.Hide();
                        Studio.Show();
                    }
                }
            }
        }

        private void oNasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InformacjeL InformacjeL = new InformacjeL();
            this.Hide();
            InformacjeL.Show();
        }

        private void zarejestrujSięToolStripMenuItem_Click(object sender, EventArgs e)
        {
            register register = new register();
            this.Hide();
            register.Show();
        }
    }
}
