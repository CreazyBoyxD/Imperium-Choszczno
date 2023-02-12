using System;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using MySql.Data.MySqlClient;

namespace WpfApp1
{
    /// <summary>
    /// Logika interakcji dla klasy Logowanie.xaml
    /// </summary>
    public partial class Logowanie : Page
    {
        BazySQL bazySQL;
        int ThemeColor = 0;
        //Logi logs;
        public int UserExists;
        MySqlDataReader options;
        public Logowanie(BazySQL obj)
        {

            InitializeComponent();
            Main.Height = 450;
            Main.Width = 800;
            bazySQL = obj;
            options = bazySQL.getOptionTable();
            if(options.HasRows)
            {
                addDataOptions();
            }
            ErrorLabelLogin.Content = "";
        }
        private void addDataOptions()
        {
            NameOfStudio.Content = options.GetValue(1).ToString();
            Application.Current.Windows[0].Title = options.GetValue(1).ToString();
            if (!options.IsDBNull("Image"))
            {
                var imageFromBytes = new BitmapImage();
                if ((byte[])options.GetValue(4) != null && options.GetValue(4) != DBNull.Value)
                {
                    using (var mem = new MemoryStream((byte[])options.GetValue(4)))
                    {
                        mem.Position = 0;
                        imageFromBytes.BeginInit();
                        imageFromBytes.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                        imageFromBytes.CacheOption = BitmapCacheOption.OnLoad;
                        imageFromBytes.UriSource = null;
                        imageFromBytes.StreamSource = mem;
                        imageFromBytes.EndInit();
                    }
                    imageFromBytes.Freeze();
                }
                LogoImage.Source = (imageFromBytes);
            }
        }
        private async Task ShowLabel(string tekst, int Green = 0)
        {
            if (Green == 1)
            {
                ErrorLabelLogin.Foreground = new SolidColorBrush(Colors.Green);
            }
            else
            {
                ErrorLabelLogin.Foreground = new SolidColorBrush(Colors.Red);
            }
            ErrorLabelLogin.Visibility = Visibility.Visible;
            ErrorLabelLogin.Content = tekst;
            await Task.Delay(3000);
            ErrorLabelLogin.Visibility = Visibility.Hidden;

        }
        private void openAdminPage(BazySQL obj, MySqlDataReader user, MySqlDataReader optionsFromLogin)
        {
            NavigationService.Navigate(new AdminPage(obj, user, optionsFromLogin, ThemeColor));
        }
        private void openUserPage(BazySQL obj, MySqlDataReader user, MySqlDataReader optionsFromLogin)
        {
            NavigationService.Navigate(new UserPage(obj, user, optionsFromLogin, ThemeColor));
        }

        public void Button_Click_signin(object sender, RoutedEventArgs e)
        {
            if ((txtBox1.Text != "" || txtBox1.Text == null) && (PassBoxLogin.Password != "" || PassBoxLogin.Password == null))
            {
                UserExists = bazySQL.checkIfDataExistLogIn(txtBox1.Text, PassBoxLogin.Password);
                if (UserExists == 1)
                {
                    //MessageBox.Show("Zalogowano");
                    ShowLabel(Properties.Resources.loggedIn);
                    Logi.addTextToFile("Logged in", txtBox1.Text.ToString());
                    MySqlDataReader user = bazySQL.InfoAboutUser(txtBox1.Text, PassBoxLogin.Password);
                    string Admin = user.GetValue(9).ToString();
                    //MainApp main = new MainApp(bazySQL,user,options);
                    //NavigationService.Navigate(new AdminPage(bazySQL, user, options));
                    //main.Show();
                    //Application.Current.Windows[0].WindowState = WindowState.Minimized;
                    if(Admin == "1")
                    {
                        openAdminPage(bazySQL, user, options);
                    }
                    else
                    {
                        openUserPage(bazySQL, user, options);
                    }
                }
                else if (UserExists == 5)
                {
                    ShowLabel(Properties.Resources.wrongPass);
                }
                else if (UserExists == 99)
                {
                    ShowLabel(Properties.Resources.userDoesntExist);
                }
            }
            else
            {
                ShowLabel(Properties.Resources.completeLoginPass);
            }
        }

        private void Button_Click_signup(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Rejestracja(bazySQL, ThemeColor, Main));
            //Rejestracja rej = new Rejestracja(bazySQL);
            //Main.Content = rej;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(ThemeColor == 0) 
            {
                Main.Background = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF252525"));
                LoginLabel.Foreground = Passlabel.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFE6FF00"));
                button1_login.Background = button2_signup.Background = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF4A4A4A"));
                button1_login.Foreground = button2_signup.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFE6FF00"));
                txtBox1.Background = PassBoxLogin.Background = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF696969"));
                txtBox1.Foreground = PassBoxLogin.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFFFFFFF"));
                ThemeColor = 1;
            }
            else if(ThemeColor == 1) 
            {
                Main.Background = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFFFFFFF"));
                LoginLabel.Foreground = Passlabel.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF000000"));
                button1_login.Background = button2_signup.Background = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFDDDDDD"));
                button1_login.Foreground = button2_signup.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF000000"));
                txtBox1.Background = PassBoxLogin.Background = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFFFFFFF"));
                txtBox1.Foreground = PassBoxLogin.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF000000"));
                ThemeColor = 0;
            }
        }
    }
}
