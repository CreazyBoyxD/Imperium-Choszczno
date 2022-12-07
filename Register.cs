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
using MySqlConnector;

namespace Imperium_Choszczno
{
    public partial class Register : Form
    {

        private string connectionString = "SERVER=192.166.219.220;DATABASE=imperium;UID=imperium;Password=imperium1212";

        public Register()
        {
            InitializeComponent();
        }

        private void Register_Load(object sender, EventArgs e)
        {

        }

        private void registerOldUser_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            Login ln = new Login();
            ln.Show();
        }

        private void registerRegister_Click(object sender, EventArgs e)
        {
            // Check if the password and repeat password match.
            if (registerPassword1.Text == registerPassword2.Text)
            {
                // Check if the password has at least 8 characters.
                if (registerPassword1.Text.Length >= 8)
                {
                    // Create a connection to the database.
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {

                        // Check if the Users table is empty.
                        bool usersTableEmpty = false;
                        using (MySqlCommand command = new MySqlCommand("SELECT COUNT(*) FROM Users", connection))
                        {
                            connection.Open();
                            usersTableEmpty = Convert.ToInt32(command.ExecuteScalar()) == 0;
                            connection.Close();
                        }

                        // Create a command to insert the data into the database.
                        using (MySqlCommand command = new MySqlCommand("INSERT INTO Users (ID, Email, Password, Role) VALUES (@id, @email, @password, @role)", connection))
                        {
                            // Generate a new ID for the user.
                            Guid id = Guid.NewGuid();

                            // Add the parameters for the values.
                            command.Parameters.Add("@id", MySqlDbType.VarChar).Value = id;
                            command.Parameters.Add("@email", MySqlDbType.VarChar).Value = registerEmail.Text;
                            command.Parameters.Add("@password", MySqlDbType.VarChar).Value = registerPassword1.Text;

                            // Set the user's role to admin if the Users table is empty, otherwise set it to regular user.
                            command.Parameters.Add("@role", MySqlDbType.VarChar).Value = usersTableEmpty ? "admin" : "regular user";

                            // Open the connection and execute the insert command.
                            connection.Open();
                            command.ExecuteNonQuery();
                        }
                    }

                    // Clear the input fields.
                    registerEmail.Clear();
                    registerPassword1.Clear();
                    registerPassword2.Clear();

                    // Going to Login window.
                    this.Hide();
                    Login ln = new Login();
                    ln.Show();

                    // Show that you reggistered succesfully.
                    MessageBox.Show("Rejestracja przebiegła pomyślnie. Można sie zalogować!");
                }
                else
                {
                    // Show an error message if the password is too short.
                    MessageBox.Show("Hasło musi mieć co najmniej 8 znaków. Proszę spróbuj ponownie.");

                    // Clear the password fields.
                    registerPassword1.Clear();
                    registerPassword2.Clear();
                }
            }
            else
            {
                // Show an error message if the passwords do not match.
                MessageBox.Show("Hasło i powtórzone hasło nie pasują do siebie. Proszę spróbuj ponownie.");

                // Clear the password fields.
                registerPassword1.Clear();
                registerPassword2.Clear();
            }
        }
    }
}
