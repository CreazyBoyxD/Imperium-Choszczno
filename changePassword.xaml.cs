using Google.Protobuf.WellKnownTypes;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace WpfApp1
{
    /// <summary>
    /// Logika interakcji dla klasy changePassword.xaml
    /// </summary>
    public partial class changePassword : Window
    {
        string Pass;
        int theme = 0;
        public changePassword(int Theme = 0)
        {
            InitializeComponent();
            Application.Current.Windows[2].Title = Properties.Resources.changePass;
            ErrorLabel.Visibility = Visibility.Hidden;
            theme = Theme;
            changeTheme();
        }

        private void changeTheme()
        {
            if (theme == 1)
            {
                Main.Background = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFFFFFFF"));
                orderConfirmUserName.Foreground = orderConfirmUserSurname.Foreground  =
                    new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF000000"));
                passConfirm.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF000000"));
                passConfirm.Background = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFDDDDDD"));
                newPass.Background = repeatNewPass.Background 
                    = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFFFFFFF"));
                newPass.Foreground = repeatNewPass.Foreground 
                    = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF000000"));
            }
            else if (theme == 0)
            {
                Main.Background = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF252525"));
                orderConfirmUserName.Foreground = orderConfirmUserSurname.Foreground =
                    new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFE6FF00"));
                passConfirm.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFE6FF00"));
                passConfirm.Background = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF4A4A4A"));
                newPass.Background = repeatNewPass.Background 
                    = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF696969"));
                newPass.Foreground = repeatNewPass.Foreground 
                    = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFFFFFFF"));
            }
        }

        public string Answer
        {
            get { return Pass; }
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
            if ((newPass.Password != null && newPass.Password != null) && (repeatNewPass.Password != null && repeatNewPass.Password != null))
            {
                if(newPass.Password == repeatNewPass.Password)
                {
                    Pass = newPass.Password;
                    this.DialogResult = true;
                }
                else
                {
                    ShowLabel(Properties.Resources.notCompPass);
                }
            }
            else ShowLabel(Properties.Resources.completeFields);
        }
    }
}
