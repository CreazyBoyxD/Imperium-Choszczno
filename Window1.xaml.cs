using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Xceed.Wpf.AvalonDock.Themes;

namespace WpfApp1
{
    /// <summary>
    /// Logika interakcji dla klasy OrderConfirmation.xaml
    /// </summary>
    public partial class OrderConfirmation : Window
    {
        List<object> userInfo;
        string[] information;
        int theme = 0;
        public OrderConfirmation(List<object> user, int Theme = 0)
        {
            userInfo = new List<object>();
            userInfo = user;
            InitializeComponent();
            ErrorLabel.Visibility = Visibility.Hidden;
            orderConfirmImie.Text = userInfo[4].ToString();
            orderConfirmNazwisko.Text = userInfo[5].ToString();
            orderConfirmAdres.Text= userInfo[7].ToString();
            orderConfirmCity.Text= userInfo[8].ToString();
            theme = Theme;
            changeTheme();
        }
        public string[] Answer
        {
            get { return information; }
        }
        private void changeTheme()
        {
            if (theme == 1)
            {
                Main.Background = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFFFFFFF"));
                orderConfirmUserName.Foreground = orderConfirmUserSurname.Foreground = orderConfirmAddressEnt.Foreground = orderConfirmCityEnt.Foreground =
                    new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF000000"));
                orderConfirm.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF000000"));
                orderConfirm.Background = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFDDDDDD"));
                orderConfirmImie.Background = orderConfirmNazwisko.Background = orderConfirmAdres.Background = orderConfirmCity.Background 
                    = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFFFFFFF"));
                orderConfirmImie.Foreground = orderConfirmNazwisko.Foreground = orderConfirmAdres.Foreground = orderConfirmCity.Foreground 
                    = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF000000"));
            }
            else if (theme == 0)
            {
                Main.Background = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF252525"));
                orderConfirmUserName.Foreground = orderConfirmUserSurname.Foreground = orderConfirmAddressEnt.Foreground = orderConfirmCityEnt.Foreground =
                    new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFE6FF00"));
                orderConfirm.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFE6FF00"));
                orderConfirm.Background = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF4A4A4A"));
                orderConfirmImie.Background = orderConfirmNazwisko.Background = orderConfirmAdres.Background = orderConfirmCity.Background 
                    = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF696969"));
                orderConfirmImie.Foreground = orderConfirmNazwisko.Foreground = orderConfirmAdres.Foreground = orderConfirmCity.Foreground 
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
        private void orderConfirm_Click(object sender, RoutedEventArgs e)
        {
            if((orderConfirmImie.Text != null && orderConfirmImie.Text != "") && (orderConfirmNazwisko.Text != null && orderConfirmNazwisko.Text != "") &&
                (orderConfirmAdres.Text != null && orderConfirmAdres.Text != "") && (orderConfirmCity.Text != null && orderConfirmCity.Text != ""))
            {
                information = new string[4];
                information[0] = orderConfirmImie.Text;
                information[1] = orderConfirmNazwisko.Text;
                information[2] = orderConfirmAdres.Text;
                information[3] = orderConfirmCity.Text;
                this.DialogResult = true;
            }
            else
            {
                ShowLabel(Properties.Resources.notAllFieldsFillUp,1);
            }
        }
    }
}
