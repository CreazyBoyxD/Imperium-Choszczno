using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows;
using System.Security.Cryptography;
using System.Globalization;
using System.Data.SqlTypes;
using MySql.Data.MySqlClient;
using System.Windows.Media.Imaging;


namespace WpfApp1
{
    public class BazySQL
    {
        static string workingDirectory = Environment.CurrentDirectory;
        static string path = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
        static string PathTest = System.AppDomain.CurrentDomain.BaseDirectory;
        static string database = "TestDB2";
        static string databaseName = "TestDB";
        static string Filename = "uzytkownicy.txt";
        static string FileNameAuthors = "autorzy.txt";
        static string FileNameSongs = "utwory.txt";
        static string nameOfMachineAndUser = Environment.MachineName + "\\" + Environment.UserName;
        static int ConnectTimeoutToBase = 30;
        static Logi logs = new Logi();

        private string makeConnString(int timeout, string nameOfMachAndUser)
        {
            return string.Format(@"Server=(localdb)\MSSQLLocalDB;User id={0};Integrated Security=True;Connect Timeout={1};Encrypt=False", 
                nameOfMachAndUser, timeout.ToString());
        }
        private string normalConnStringAllFunctions(string databaseName, string nameOfMachineAndUser, string path)
        {
            return string.Format(@"Data source = (localdb)\MSSQLLocalDB; AttachDbFilename={2}\{0}_Data.mdf;
                User Id={1};Integrated Security=True;Connect Timeout=1;Encrypt=False", databaseName, nameOfMachineAndUser, path);
        }
        public string makeMySQLConnString()
        {
            return "SERVER=192.166.219.220;DATABASE=imperium;UID=imperium;Password=imperium1212";
        }
        public bool CheckConnectToDataBase(string connectionStr)
        {
            bool result = false;

            using (MySqlConnection connection = new MySqlConnection(connectionStr))
            {
                try
                {
                    connection.Open();
                    connection.Close();
                    return true;
                    
                }
                catch (MySqlException)
                {
                    connection.Close();
                    return false;
                }
            }
        }
        public void CheckAndCreateDB() //funkcja tworzaca lub sprawdzajaca istnienie bazy o zadanej nazwie
        {
            //string workingDirectory = Environment.CurrentDirectory;
            //string path = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            string sqlCreateDBQuery, sqlCheckDBQuery;
            //string databaseName = "TestDB2";

            SqlConnection tmpConn = new SqlConnection();

            string connectionStr = makeConnString(ConnectTimeoutToBase,nameOfMachineAndUser);
            string connectionStr2 = "SERVER=192.166.219.220;DATABASE=imperium;UID=imperium;Password=imperium1212";
            bool ConnOk = CheckConnectToDataBase(connectionStr2);
            if(ConnOk) 
            {
                CheckOrCreateTablesDB();
            }
            //tmpConn.ConnectionString = connectionStr;

            /*string sql = string.Format(@"CREATE DATABASE {0} ON PRIMARY" +
                @"(NAME = {1}, FILENAME = '{2}\{1}_Data.mdf', SIZE = 2MB, FILEGROWTH = 10%) " +
                @"LOG ON (NAME = {1}_Log, FILENAME = '{2}\{1}_Data_Log.ldf', SIZE = 1MB, FILEGROWTH = 10%)",@database, @databaseName, @path);

            sqlCreateDBQuery = sql;

            //sqlCheckDBQuery = string.Format("SELECT database_id FROM sys.databases WHERE Name = '{0}'", @database);
            sqlCheckDBQuery = string.Format("SELECT COUNT(*) FROM sys.databases WHERE Name = '{0}'", @database);
            bool result;
            using (tmpConn)
            {
                using (SqlCommand sqlCmd = new SqlCommand(sqlCheckDBQuery, tmpConn))
                {
                    tmpConn.Open();

                    var resultObj = sqlCmd.ExecuteScalar();

                    int databaseID = 0;

                    if ((int)resultObj != 0)
                    {
                        int.TryParse(resultObj.ToString(), out databaseID);
                    }

                    tmpConn.Close();

                    result = (databaseID > 0);
                    if (!result)
                    {
                        tmpConn = new SqlConnection();
                        tmpConn.ConnectionString = connectionStr;

                        SqlCommand myCommand = new SqlCommand(sqlCreateDBQuery, tmpConn);
                        try
                        {
                            tmpConn.Open();
                            myCommand.ExecuteNonQuery();
                            Logi.addTextToFile("Database created");
                            //App.mainWindow.checkBox1.IsEnabled = true;
                            //App.mainWindow.checkBox1.IsChecked = true;
                            //App.mainWindow.checkBox1.IsEnabled = false;
                            //MessageBox.Show("Database has been created successfully!", "Create Database");
                        }
                        catch (SqlException ex)
                        {
                            string plik = string.Format(@"{0}\{1}_Data.mdf", path, database);
                            string plik2 = string.Format(@"{0}\{1}_Data_Log.ldf", path, database);
                            if (File.Exists(plik) == true)
                            {
                                tmpConn.Close();
                                Logi.addTextToFile("File of database exists but found a problem and deleted and created new one");
                                tmpConn = new SqlConnection();
                                tmpConn.ConnectionString = connectionStr;
                                string sqlCommandText = string.Format(@"DROP DATABASE {0}", database);
                                SqlCommand myCommandDelete = new SqlCommand(sqlCommandText, tmpConn);
                                tmpConn.Open();
                                myCommand.ExecuteNonQuery();
                                //tmpConn.Close();
                                File.Delete(plik);
                                File.Delete(plik2);
                                CheckAndCreateDB();
                            }
                            MessageBox.Show(ex.ToString(), "Created new Database");
                        }
                        finally
                        {
                            tmpConn.Close();
                        }
                        return;
                    }
                    else
                    {
                        Logi.addTextToFile("Database exists");
                        //App.mainWindow.checkBox1.IsChecked = true;
                        //MessageBox.Show("Baza istnieje w zadanej lokalizacji");
                    }
                }
            }*/
        }

        private void CheckOrCreateTablesDB()
        {
            MySqlConnection conn = new MySqlConnection(makeMySQLConnString());

            using (MySqlCommand command = new MySqlCommand("DROP TABLE IF EXISTS Options;" +
                " CREATE TABLE Options (ID INTEGER PRIMARY KEY AUTO_INCREMENT," +
                " StudioName varchar(255) NOT NULL," +
                " KeyProduct varchar(255) NOT NULL," +
                " Country varchar(10) NOT NULL," +
                " Image longblob);", conn))
            {
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
                Logi.addTextToFile("Added new Authors table to database");
                addToOptionTable();
            }

            using (MySqlCommand command = new MySqlCommand("DROP TABLE IF EXISTS Users;" +
                " CREATE TABLE Users (ID INTEGER PRIMARY KEY AUTO_INCREMENT," +
                " Login varchar(255) NOT NULL," +
                " Password varchar(255) NOT NULL," +
                " Hashpass varchar(1024) NOT NULL," +
                " Name varchar(255) NOT NULL," +
                " Surname varchar(255) NOT NULL, " +
                " Wallet double(10,2) NOT NULL," +
                " Address varchar(255) NOT NULL," +
                " City varchar(255) NOT NULL," +
                " Admin int NOT NULL," +
                " IsFirstAdmin int NOT NULL," +
                " UNIQUE (Login));", conn))
            {
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
                Logi.addTextToFile("Added new Users table to database");
                addUsersToTable();
            }

            using (MySqlCommand command = new MySqlCommand("DROP TABLE IF EXISTS Authors;" +
                " CREATE TABLE Authors (ID INTEGER PRIMARY KEY AUTO_INCREMENT," +
                " AuthorName varchar(255) NOT NULL," +
                " FileName varchar(255)," +
                " Image longblob);", conn))
            {
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
                Logi.addTextToFile("Added new Authors table to database");
                addAuthorsToTable();
            }

            using (MySqlCommand command = new MySqlCommand("DROP TABLE IF EXISTS Songs;" +
                " CREATE TABLE Songs (ID INTEGER PRIMARY KEY AUTO_INCREMENT," +
                " SongName varchar(255) NOT NULL," +
                " FileName varchar(255)," +
                " Image longblob," +
                " Authors varchar(255) NOT NULL," +
                " Price varchar(255) NOT NULL," +
                " Discount varchar(255));", conn))
            {
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
                Logi.addTextToFile("Added new Songs table to database");
                addSongsToTable();
            }

            using (MySqlCommand command = new MySqlCommand("DROP TABLE IF EXISTS Albums;" +
                " CREATE TABLE Albums (ID INTEGER PRIMARY KEY AUTO_INCREMENT," +
                " AlbumName varchar(255) NOT NULL," +
                " FileName varchar(255)," +
                " Image longblob," +
                " Songs varchar(255) NOT NULL," +
                " Price varchar(255) NOT NULL," +
                " Discount varchar(255));", conn))
            {
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
                Logi.addTextToFile("Added new Albums table to database");
            }

            using (MySqlCommand command = new MySqlCommand("DROP TABLE IF EXISTS Orders;" +
                " CREATE TABLE Orders(ID INTEGER PRIMARY KEY AUTO_INCREMENT," +
                " UserID varchar(255) NOT NULL," +
                " ProductID varchar(255) NOT NULL," +
                " ProductType varchar(255) NOT NULL," +
                " OrderData varchar(255) NOT NULL," +
                " Price varchar(255) NOT NULL," +
                " DeliveryAdress varchar(255));", conn))
            {
                conn.Open();
                command.ExecuteNonQuery();
                conn.Close();
                Logi.addTextToFile("Added new Orders table to database");
            }
        }
        /// <summary>
        /// Funkcje obsługi ładowania opcji do programu, ich edycji oraz dodawania
        /// </summary>
        public void addToOptionTable()
        {
            string sql = "";
            string studioName = "Imperium Choszczno";
            string Keyproduct = "2137-J2PGD-MHH88";
            string country = "pl_PL";
            byte[] studioImage = null;
            
            if (File.Exists(String.Format(path+"\\logo.png")))
            {
                MemoryStream memStream = new MemoryStream();
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(new Uri(String.Format(path + "\\logo.png"))));
                encoder.Save(memStream);
                studioImage= memStream.ToArray();
            }
            addSaveOptions(1, studioName, Keyproduct, country, studioImage);
        }
        public void addSaveOptions(int type, string StudioName, string prodKey, string localization, byte[] image)
        {
            MySqlConnection conn = new MySqlConnection(makeMySQLConnString());
            string sql = "";
            if (type == 1) 
            {
                sql = String.Format(@"INSERT INTO Options (StudioName, KeyProduct, Country, Image) VALUES ('{0}', '{1}', '{2}', @image)",
                    StudioName, prodKey, localization);
            }
            else if (type == 2) 
            {
                sql = String.Format(@"UPDATE Options SET StudioName = '{0}', KeyProduct = '{1}', Country = '{2}', Image = @image WHERE ID = 1",
                    StudioName, prodKey, localization);
            }

            conn.Open();
            
            MySqlCommand cmd = new MySqlCommand(sql, conn);

            if (image != null)
            {
                cmd.Parameters.Add("@image", MySqlDbType.VarBinary).Value = image;
            }
            else
            {
                cmd.Parameters.Add("@image", MySqlDbType.VarBinary).Value = DBNull.Value;
            }
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public MySqlDataReader getOptionTable()
        {
            MySqlConnection conn = new MySqlConnection(makeMySQLConnString());
            MySqlCommand getOptions = new MySqlCommand("SELECT * FROM Options WHERE ID = 1", conn);
            conn.Open();
            MySqlDataReader reader = null;
            reader = getOptions.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                return reader;
            }
            else
            {
                return reader;
            }
            
        }
        /// <summary>
        /// Funkcje pozostałe
        /// </summary>
        private string HashPassword(string pass)
        {
            string HashPass;
            using (var sha256 = SHA256.Create())
            {  
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(pass));
                //podwójne hashowanie hasła poprzez 256
                //var hashedBytes = sha256.ComputeHash((sha256.ComputeHash(Encoding.UTF8.GetBytes(pass))));
                HashPass = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
            return HashPass;
        }
        private decimal ConvertToDecimal(string cash)
        {
            decimal defaultValue = 0;
            decimal result;
            //Try parsing in the current culture
            if (!System.Decimal.TryParse(cash, System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out result) &&
                //Then try in US english
                !System.Decimal.TryParse(cash, System.Globalization.NumberStyles.Any, CultureInfo.GetCultureInfo("en-US"), out result) &&
                //Then in neutral language
                !System.Decimal.TryParse(cash, System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out result))
            {
                result = defaultValue;
            }
            return result;
        }
        /// <summary>
        /// Funkcje obsługi użytkowników
        /// </summary>
        public void registerUser(string UserName, string Password, string Name, string Surname, string Address, string City, string Cash = "1000.00")
        {
            int Admin = 0;
            MySqlConnection conn = new MySqlConnection(makeMySQLConnString());
            bool checkAdmins = checkIfAdminExists();
            if (!checkAdmins)
            {
                Admin = 1;
            }
            string hashpass = HashPassword(Password);
            decimal cash = ConvertToDecimal(Cash);
            conn.Open();
            string sql = String.Format(@"INSERT INTO Users(Login, Password, Hashpass, Name, Surname, Wallet, Address, City, Admin, IsFirstAdmin) 
            VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', {8}, {9})", UserName, Password, hashpass, Name, Surname, cash.ToString().Replace(',', '.'), Address, City, Admin, Admin);
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            Logi.addTextToFile(String.Format("Added new user to Users table - UserName: {0}, Name: {1}, Surname: {2}, is Admin {3}", UserName, Name, Surname, Admin), UserName);
        }
        public void saveUser(int ID, string UserName, string Name, string Surname, string Cash, string Address, string City, int Admin, string WHO)
        {
            MySqlConnection conn = new MySqlConnection(makeMySQLConnString());
            decimal cash = ConvertToDecimal(Cash);
            conn.Open();
            string sql = String.Format(@"UPDATE Users SET Login = '{0}', Name = '{1}', Surname = '{2}', Wallet = '{7}', Address = '{3}', City='{4}', Admin='{5}' WHERE ID={6}",
                        UserName, Name, Surname, Address, City, Admin, ID, cash.ToString().Replace(',', '.'));
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            Logi.addTextToFile(String.Format("Saved existing user in Users table - ID in table: {0}, UserName: {1}, Name: {2}, Surname: {3}, is Admin {4}", ID, UserName, Name, Surname, Admin), WHO);
        }
        public void deleteUser(int ID, string WHO)
        {
            MySqlConnection conn = new MySqlConnection(makeMySQLConnString());
            conn.Open();
            string sql = String.Format(@"DELETE FROM Users WHERE ID={0}", ID);
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            Logi.addTextToFile(String.Format("Saved existing user in Users table - ID in table: {0}", ID), WHO);
        }
        public MySqlDataReader InfoAboutUser(string login, string password)
        {
            MySqlConnection conn = new MySqlConnection(makeMySQLConnString());
            MySqlCommand checkLoginPass = new MySqlCommand("SELECT * FROM Users WHERE Login = @log AND Password = @pas", conn);
            checkLoginPass.Parameters.Add(new MySqlParameter("@log", MySqlDbType.VarChar));
            checkLoginPass.Parameters["@log"].Value = login;
            checkLoginPass.Parameters.Add(new MySqlParameter("@pas", MySqlDbType.VarChar));
            checkLoginPass.Parameters["@pas"].Value = password;
            conn.Open();
            MySqlDataReader reader = checkLoginPass.ExecuteReader();

            reader.Read();
            return reader;
        }
        public MySqlDataReader infoAboutUserByID(int ID) 
        {
            MySqlConnection conn = new MySqlConnection(makeMySQLConnString());
            MySqlCommand cmd = new MySqlCommand(String.Format(@"SELECT * FROM Users WHERE ID = {0}",ID), conn);
            conn.Open();
            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
            }
            else
            {
                reader = null;
            }
            
            return reader;
        }
        public void updateUserCash(int ID, string userName, string cash)
        {
            MySqlConnection conn = new MySqlConnection(makeMySQLConnString());
            conn.Open();
            string sql = String.Format(@"UPDATE Users SET Wallet = '{0}' WHERE ID={1} AND Login='{2}'",
                        cash.ToString().Replace(',', '.'), ID, userName);
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public DataTable getUserOrder(int ID)
        {
            MySqlConnection conn = new MySqlConnection(makeMySQLConnString());
            MySqlDataAdapter sda = null;
            DataTable dt = null;
            string query = "SELECT * FROM Orders WHERE UserID=@ID";
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@ID", ID);
            sda = new MySqlDataAdapter(cmd);

            dt = new DataTable("Orders");
            sda.Fill(dt);
            return dt;
        }
        public void changeUserPassword(int ID, string UserName, string Pass)
        {
            string hashpass = HashPassword(Pass);
            MySqlConnection conn = new MySqlConnection(makeMySQLConnString());
            conn.Open();
            string sql = "UPDATE Users SET Password = @Pass, Hashpass = @hashpass WHERE ID=@ID AND Login = @UserName";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@ID", ID);
            cmd.Parameters.AddWithValue("@Pass", Pass);
            cmd.Parameters.AddWithValue("@hashpass", hashpass);
            cmd.Parameters.AddWithValue("@UserName", UserName);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public void saveUserFromUserPage(int ID, string login, string name, string surname, string address, string city)
        {
            MySqlConnection conn = new MySqlConnection(makeMySQLConnString());
            conn.Open();
            string sql = "UPDATE Users SET Login = @login, Name = @name, Surname = @surname, Address = @address, City = @city WHERE ID = @ID ";

            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@ID", ID);
            cmd.Parameters.AddWithValue("@login", login);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@surname", surname);
            cmd.Parameters.AddWithValue("@address", address);
            cmd.Parameters.AddWithValue("@city", city);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        /// <summary>
        /// Funkcje obsługi sprawdzania istnienia danych w konkretnych tabelach
        /// </summary>
        private bool checkIfAdminExists()
        {
            MySqlConnection conn = new MySqlConnection(makeMySQLConnString());
            MySqlCommand checkLoginPass = new MySqlCommand("SELECT * FROM Users WHERE Admin = 1", conn);
            conn.Open();
            var reader = checkLoginPass.ExecuteReader();
            if (reader.HasRows)
            {
                return true;
            }
            else
            {
                return false;
            }
            conn.Close();
        }
        public int checkIfDataExistLogIn(string login, string password = "default")
        {
            MySqlConnection conn = new MySqlConnection(makeMySQLConnString());
            MySqlCommand checkLoginPass = new MySqlCommand();
            checkLoginPass.CommandText = "SELECT * FROM Users WHERE Login = @log";
            checkLoginPass.Connection = conn;
            checkLoginPass.Parameters.Add(new MySqlParameter("@log", MySqlDbType.VarChar));
            checkLoginPass.Parameters["@log"].Value = login;
            conn.Open();
            MySqlDataReader reader = checkLoginPass.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                string passw = reader.GetValue(2).ToString();
                if (passw == password)
                {
                    return 1;
                }
                else
                {
                    return 5;
                }
            }
            else
            {
                return 99;
            }
            conn.Close();
        }
        public bool checkIfUserAuthorSongAlbumExist(string typeOfQuestion, int ID, string Name = "")
        {
            MySqlConnection conn = new MySqlConnection(makeMySQLConnString());
            string check = "";
            if (Name != "" && Name != null && Name != string.Empty)
            {
                if (typeOfQuestion == "user")
                {

                }
                else if (typeOfQuestion == "author")
                {
                    check = String.Format("SELECT COUNT(*) FROM Authors WHERE ID = @ID AND AuthorName = @Name");
                }
                else if (typeOfQuestion == "song")
                {
                    check = String.Format("SELECT COUNT(*) FROM Songs WHERE ID = @ID AND SongName = @Name");
                }
                else if (typeOfQuestion == "album")
                {
                    check = String.Format("SELECT COUNT(*) FROM Albums WHERE ID = @ID AND AlbumName = @Name");
                }
            }
            else
            {
                if (typeOfQuestion == "user")
                {

                }
                else if (typeOfQuestion == "author")
                {
                    check = String.Format("SELECT COUNT(*) FROM Authors WHERE ID = @ID");
                }
                else if (typeOfQuestion == "song")
                {
                    check = String.Format("SELECT COUNT(*) FROM Songs WHERE ID = @ID");
                }
                else if (typeOfQuestion == "album")
                {
                    check = String.Format("SELECT COUNT(*) FROM Albums WHERE ID = @ID");
                }
            }

            conn.Open();
            MySqlCommand cmd = new MySqlCommand(check, conn);
            cmd.Parameters.AddWithValue("@ID", ID);
            if (Name != "" || Name != null || Name != string.Empty)
            {
                cmd.Parameters.AddWithValue("@Name", Name);
            }
            var resultObj = cmd.ExecuteScalar();

            int count = 0;

            if ((Int64)resultObj != 0)
            {
                int.TryParse(resultObj.ToString(), out count);
            }
            if (count > 0)
            {
                return true;
            }
            else
                return false;
        }
        /// <summary>
        /// Funkcje obsługi tabel (wspólne)
        /// </summary>
        public void deleteUserAuthorSongAlbum(string type, int ID, string WHO = "")
        {
            MySqlConnection conn = new MySqlConnection(makeMySQLConnString());
            string sql = "";
            if (type == "Users")
            {
                sql = String.Format(@"DELETE FROM Users WHERE ID={0}", ID);
            }
            else if (type == "Songs")
            {
                sql = String.Format(@"DELETE FROM Songs WHERE ID={0}", ID);
            }
            else if (type == "Albums")
            {
                sql = String.Format(@"DELETE FROM Albums WHERE ID={0}", ID);
            }
            else if (type == "Authors")
            {
                sql = String.Format(@"DELETE FROM Authors WHERE ID={0}", ID);
            }
            else if (type == "Orders")
            {
                sql = String.Format(@"DELETE FROM Orders WHERE ID={0}", ID);
            }
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public DataTable getTables(string Table)
        {
            MySqlConnection conn = new MySqlConnection(makeMySQLConnString());
            string CmdString = "";
            MySqlDataAdapter sda = null;
            DataTable dt = null;
            if(Table == "users")
            {
                CmdString = "SELECT * FROM Users";
            }
            else if(Table == "authors")
            {
                CmdString = "SELECT * FROM Authors";
            }
            else if(Table == "songs")
            {
                CmdString = "SELECT * FROM Songs";
            }
            else if(Table == "albums")
            {
                CmdString = "SELECT * FROM Albums";
            }
            else if(Table == "orders")
            {
                CmdString = "SELECT * FROM Orders";
            }
            MySqlCommand cmd = new MySqlCommand(CmdString, conn);
            sda = new MySqlDataAdapter(cmd);
            if (Table == "users")
            {
                dt = new DataTable("Users");
            }
            else if (Table == "authors")
            {
                dt = new DataTable("Authors");
            }
            else if (Table == "songs")
            {
                dt = new DataTable("Songs");
            }
            else if (Table == "albums")
            {
                dt = new DataTable("Albums");
            }
            else if (Table == "orders")
            {
                dt = new DataTable("Orders");
            }
            sda.Fill(dt);
            return dt;
        }
        /// <summary>
        /// Funkcje obsługi dodawania autorów
        /// </summary>
        public void addSaveAuthor(int AddOrSave, string AuthName, string NameFile = "null", byte[] image = null, int Id = -1)
        {
            MySqlConnection conn = new MySqlConnection(makeMySQLConnString());
            string sql = "";

            if (AddOrSave == 1)
            {
                conn.Open();
                sql = String.Format(@"INSERT INTO Authors (AuthorName, FileName, Image) VALUES ('{0}', @NameFile, @image)", AuthName);
            }
            else if (AddOrSave == 2 && Id >= 0)
            {
                conn.Open();
                sql = String.Format(@"UPDATE Authors SET AuthorName = '{0}', FileName = @NameFile, Image = @image WHERE ID={1}",
                            AuthName, Id);
            }
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            if (NameFile != "" && NameFile != null && NameFile != "null")
            {
                cmd.Parameters.AddWithValue("@NameFile", NameFile);
            }
            else
            {
                cmd.Parameters.AddWithValue("@NameFile", DBNull.Value);
            }
            if (image != null)
            {
                cmd.Parameters.AddWithValue("@image", image);
            }
            else
            {
                cmd.Parameters.AddWithValue("@image", DBNull.Value);
            }
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public MySqlDataReader getInfoAboutAuthor(int ID)
        {
            MySqlConnection conn = new MySqlConnection(makeMySQLConnString());
            string query = "SELECT * FROM Authors WHERE ID=@ID";
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@ID", ID);
            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                return reader;
            }
            else
                return null;
        }
        public DataSet getAuthorsToCombobox()
        {
            MySqlConnection conn = new MySqlConnection(makeMySQLConnString());

            string query = "SELECT ID, AuthorName FROM Authors";
            MySqlDataAdapter da = new MySqlDataAdapter(query, conn);
            conn.Open();
            DataSet ds = new DataSet();
            da.Fill(ds, "Authors");
            return ds;

        }
        public byte[] getAuthorImage(int ID)
        {
            MySqlConnection conn = new MySqlConnection(makeMySQLConnString());

            byte[] imageByte = null;
            string query = "SELECT Image FROM Authors WHERE ID=@ID";
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@ID", ID);
            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                if (!reader.IsDBNull("Image"))
                {
                    imageByte = (byte[])reader[0];
                }
                else
                {
                    imageByte = null;
                }
            }
            else
            {
                imageByte = null;
            }
            return imageByte;
        }
        public void deleteAuthor(int ID)
        {
            MySqlConnection conn = new MySqlConnection(makeMySQLConnString());
            conn.Open();
            string sql = String.Format(@"DELETE FROM Authors WHERE ID={0}", ID);
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            //Logi.addTextToFile(String.Format("Saved existing user in Users table - ID in table: {0}", ID), WHO);
        }
        /// <summary>
        /// Funkcje obsługi utworów
        /// </summary>
        public void addSaveSong(int AddOrSave, string SongName, string Authors, string price, string discount, int Id = -1, string NameFile = "null", byte[] image = null)
        {
            MySqlConnection conn = new MySqlConnection(makeMySQLConnString());
            string sql = "";
            conn.Open();
            if (AddOrSave == 1)
            {
                sql = String.Format(@"INSERT INTO Songs (SongName, FileName, Image, Authors, Price, Discount) VALUES ('{0}', @NameFile, @image, 
                @Authors, @price, @discount)", SongName);
            }

            else if (AddOrSave == 2 && Id >= 0)
            {
                sql = String.Format(@"UPDATE Songs SET SongName = '{0}', FileName = @NameFile, Image = @image, Authors = @authors,
                Price = @price, Discount = @discount WHERE ID={1}",
                            SongName, Id);
            }
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            if (NameFile != "" && NameFile != null && NameFile != "null")
            {
                cmd.Parameters.AddWithValue("@NameFile", NameFile);
            }
            else
            {
                cmd.Parameters.AddWithValue("@NameFile", SqlString.Null);
            }
            if (image != null)
            {
                cmd.Parameters.AddWithValue("@image", image);
            }
            else
            {
                cmd.Parameters.AddWithValue("@image", SqlBinary.Null);
            }
            cmd.Parameters.AddWithValue("@authors", Authors);
            cmd.Parameters.AddWithValue("@price", price);
            cmd.Parameters.AddWithValue("@discount", discount);

            cmd.ExecuteNonQuery();
            conn.Close();
        }
        /// <summary>
        /// Funkcje obsługi albumów
        /// </summary>
        public void addSaveAlbum(int AddOrSave, string SongName, string Songs, string price, string discount, int Id = -1, string NameFile = "null", byte[] image = null)
        {
            MySqlConnection conn = new MySqlConnection(makeMySQLConnString());
            string sql = "";
            conn.Open();
            if (AddOrSave == 1)
            {
                sql = String.Format(@"INSERT INTO Albums (AlbumName, FileName, Image, Songs, Price, Discount) VALUES ('{0}', @NameFile, @image, 
                @Songs, @price, @discount)", SongName);
            }

            else if (AddOrSave == 2 && Id >= 0)
            {
                sql = String.Format(@"UPDATE Albums SET AlbumName = '{0}', FileName = @NameFile, Image = @image, Authors = @authors
                Price = @price, Discount = @discount WHERE ID={1}",
                            SongName, Id);
            }
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            if (NameFile != "" && NameFile != null && NameFile != "null")
            {
                cmd.Parameters.AddWithValue("@NameFile", NameFile);
            }
            else
            {
                cmd.Parameters.AddWithValue("@NameFile", SqlString.Null);
            }
            if (image != null)
            {
                cmd.Parameters.AddWithValue("@image", image);
            }
            else
            {
                cmd.Parameters.AddWithValue("@image", DBNull.Value);
            }
            cmd.Parameters.AddWithValue("@Songs", Songs);
            cmd.Parameters.AddWithValue("@price", price);
            cmd.Parameters.AddWithValue("@discount", discount);

            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public DataTable getSongsOfAlbum(string SongsString)
        {
            MySqlConnection conn = new MySqlConnection(makeMySQLConnString());
            string CmdString = "";
            MySqlDataAdapter sda = null;
            DataTable dt = null;

            CmdString = String.Format(@"SELECT * FROM Songs WHERE id IN ({0})", SongsString.Replace('"', '\0'));
            MySqlCommand cmd = new MySqlCommand(CmdString, conn);
            sda = new MySqlDataAdapter(cmd);

            dt = new DataTable("Songs");
            sda.Fill(dt);
            return dt;
        }
        /// <summary>
        /// Funkcje obsługi zamówień
        /// </summary>
        public void addSaveOrder(int AddOrSave, int UserID, int ProductID, string ProductType, string OrderData, string Price, string DeliveryAddress, int Id = -1)
        {
            MySqlConnection conn = new MySqlConnection(makeMySQLConnString());
            string sql = "";

            if (AddOrSave == 1)
            {
                conn.Open();
                sql = String.Format(@"INSERT INTO Orders (UserID, ProductID, ProductType, OrderData, Price, DeliveryAdress) VALUES ({0}, {1}, '{2}', '{3}', '{4}', '{5}')",
                    UserID, ProductID, ProductType, OrderData, Price, DeliveryAddress);
            }
            else if (AddOrSave == 2 && Id >= 0)
            {
               // conn.Open();
                //sql = String.Format(@"UPDATE Orders SET UserID = '{0}', FileName = @NameFile, Image = @image WHERE ID={1}",
                           // AuthName, Id);
            }
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public MySqlDataReader getOrderedProductinfo(string type, int ID, string name = "")
        {
            MySqlConnection conn = new MySqlConnection(makeMySQLConnString());
            string command = "";
            if (type == "Songs") 
            {
                command = "SELECT * FROM Songs WHERE ID = @ID";
            }
            else if (type == "Albums") 
            {
                command = "SELECT * FROM Albums WHERE ID = @ID";
            }
            MySqlCommand cmd = new MySqlCommand(command, conn);
            cmd.Parameters.AddWithValue("@ID", ID);
            conn.Open();
            MySqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
            }
            else
            {
                reader = null;
            }

            return reader;
        }
        /// <summary>
        /// Funkcje dodawania danych z plików TXT do tabel
        /// </summary>
        private void addUsersToTable()
        {
            //List<string> Users = new List<string>();
            if (File.Exists(string.Format(@"{0}\{1}", path, Filename)))
            {
                string[] lines = File.ReadAllLines(string.Format(@"{0}\{1}", path, Filename));
                foreach (var line in lines)
                {
                    if(line != "" || line != null)
                    {
                        string[] podzial = line.Split(';');

                        registerUser(podzial[0], podzial[1], podzial[2], podzial[3], podzial[5], podzial[6], podzial[4]);
                    }
                }
            }
            else MessageBox.Show("Plik z informacjami do bazy nie istnieje!");
        }
        private void addAuthorsToTable()
        {
            if (File.Exists(string.Format(@"{0}\{1}", path, FileNameAuthors)))
            {
                string[] lines = File.ReadAllLines(string.Format(@"{0}\{1}", path, FileNameAuthors));
                foreach (var line in lines)
                {
                    if (line != "" || line != null)
                    {
                        string[] podzial = line.Split(';');
                        string[] hex = podzial[2].Split('-');
                        byte[] bits = new byte[hex.Length];
                        for(int i=0; i< hex.Length;i++)
                        {
                            bits[i] = Convert.ToByte(hex[i], 16);
                        }
                        addSaveAuthor(1, podzial[0], podzial[1],bits);
                    }
                }
            }
            else MessageBox.Show("Plik z informacjami do bazy nie istnieje!");
        }
        private void addSongsToTable()
        {
            if (File.Exists(string.Format(@"{0}\{1}", path, FileNameSongs)))
            {
                string[] lines = File.ReadAllLines(string.Format(@"{0}\{1}", path, FileNameSongs));
                foreach (var line in lines)
                {
                    if (line != "" || line != null)
                    {
                        string[] podzial = line.Split(';');
                        string[] hex = podzial[2].Split('-');
                        byte[] bits = new byte[hex.Length];
                        for (int i = 0; i < hex.Length; i++)
                        {
                            bits[i] = Convert.ToByte(hex[i], 16);
                        }
                        addSaveSong(1, podzial[0], podzial[3], podzial[4], podzial[5],-1,podzial[1],bits);
                    }
                }
            }
            else MessageBox.Show("Plik z informacjami do bazy nie istnieje!");
        }
    }
}