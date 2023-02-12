using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;

namespace WpfApp1
{
    /// <summary>
    /// Logika interakcji dla klasy Rejestracja.xaml
    /// </summary>
    
    public partial class Rejestracja : Page
    {
        BazySQL bazyRej;
        int theme;
        Frame frame1;
        public Rejestracja(BazySQL bazyObj, int motyw, Frame frame)
        {
            //System.Windows.Application.Current.MainWindow.Height = 500;
            frame1 = frame;
            InitializeComponent();
            Main.Height = 600;
            Main.Width = 900;
            //frame1.Height = 550;
            //frame1.Width = 800;
            ErrorLabel.Visibility = Visibility.Hidden;
            bazyRej = bazyObj;
            theme = motyw;
            changeTheme();
        }
        private void changeTheme() 
        {
            if (theme == 0)
            {
                Main.Background = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFFFFFFF"));
                UserName.Foreground =  UserSurname.Foreground = UserUserName.Foreground = PassEnt.Foreground = 
                PassEntAgain.Foreground = AddressEnt.Foreground = CityEnt.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF000000"));
                BtRejestracja.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF000000"));
                BtRejestracja.Background = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFDDDDDD"));
                txtImie.Background = txtNazwisko.Background = txtNazwaUzytkownika.Background = Haslo.Background = HasloPowt.Background = Adres.Background = City.Background =
                    new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFFFFFFF"));
                txtImie.Foreground = txtNazwisko.Foreground = txtNazwaUzytkownika.Foreground = Haslo.Foreground = HasloPowt.Foreground = Adres.Foreground = City.Foreground
                    = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF000000"));
            }
            else if (theme == 1)
            {
                Main.Background = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF252525"));
                UserName.Foreground = UserSurname.Foreground = UserUserName.Foreground = PassEnt.Foreground = 
                PassEntAgain.Foreground = AddressEnt.Foreground = CityEnt.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFE6FF00"));
                BtRejestracja.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFE6FF00"));
                BtRejestracja.Background = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF4A4A4A"));
                txtImie.Background = txtNazwisko.Background = txtNazwaUzytkownika.Background = Haslo.Background = HasloPowt.Background = Adres.Background = City.Background
                    = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF696969"));
                txtImie.Foreground = txtNazwisko.Foreground = txtNazwaUzytkownika.Foreground = Haslo.Foreground = HasloPowt.Foreground = Adres.Foreground = City.Foreground
                    = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFFFFFFF"));
            }
        }
        private async Task ShowLabel(string tekst, int Green = 0)
        {
            if (Green == 1)
            {
                ErrorLabel.Foreground = new SolidColorBrush(Colors.Green);
            }
            else
            {
                ErrorLabel.Foreground = new SolidColorBrush(Colors.Red);
            }
            ErrorLabel.Visibility = Visibility.Visible;
            ErrorLabel.Content = tekst;
            await Task.Delay(3000);
            ErrorLabel.Visibility = Visibility.Hidden;
        }
        private async void BtRejestracja_Click(object sender, RoutedEventArgs e)
        {
            if(((txtImie.Text != "" || txtImie.Text == null) && (txtNazwisko.Text != "" || txtNazwisko.Text == null) &&
                (txtNazwaUzytkownika.Text != "" || txtNazwaUzytkownika.Text == null) && (Haslo.Password != "" || Haslo.Password == null) && 
                (HasloPowt.Password != "" || HasloPowt.Password == null) && (Adres.Text != "" || Adres.Text == null) &&
                (City.Text != "" || City.Text == null)))
            {
                if (Haslo.Password == HasloPowt.Password)
                {
                    int UserExists = bazyRej.checkIfDataExistLogIn(txtNazwaUzytkownika.Text.ToString(), Haslo.Password.ToString());
                    if(UserExists == 1)
                    {
                        ShowLabel(Properties.Resources.userexists);
                        Logi.addTextToFile("Failed register new user with a name (user exists): ",txtNazwaUzytkownika.Text);
                    }
                    else if(UserExists == 5)
                    {
                        ShowLabel(Properties.Resources.userNameExists);
                        Logi.addTextToFile("Failed register new user with a name (user name exists): ", txtNazwaUzytkownika.Text);
                    }
                    else if(UserExists == 99)
                    {
                        bazyRej.registerUser(txtNazwaUzytkownika.Text.ToString(), Haslo.Password.ToString(), txtImie.Text.ToString(),txtNazwisko.Text.ToString(), Adres.Text.ToString(), City.Text.ToString());
                        ShowLabel(Properties.Resources.userRegistered, 1);
                        Logi.addTextToFile(String.Format("Registered new user with a user name: {0}, name: {1} and surname: {2} ", txtNazwaUzytkownika.Text, txtImie.Text, txtNazwisko.Text), txtNazwaUzytkownika.Text);
                        await Task.Delay(1000);
                        NavigationService.Navigate(new Logowanie(bazyRej));
                    }
                }
                else
                {
                    ShowLabel(Properties.Resources.notCompPass);
                    Logi.addTextToFile("Failed register new user with a name (user's passwords aren't the same): ", txtNazwaUzytkownika.Text);
                }
            }
            else
            {
                ShowLabel(Properties.Resources.completeFields);
                if(txtNazwaUzytkownika != null)
                {
                    Logi.addTextToFile("Failed register new user with a name (fields were not filled in): ", txtNazwaUzytkownika.Text);
                }
                Logi.addTextToFile("Failed register new user with a name (fields were not filled in): ");
            }
        }
    }
}
