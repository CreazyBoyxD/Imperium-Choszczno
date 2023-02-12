using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static MainWindow mainWindow;
        BazySQL bazySQL;
        MySqlDataReader options;
        App()
        {
            bazySQL = new BazySQL();
            options = bazySQL.getOptionTable();
            string language = options.GetValue(3).ToString();
            if(language == "pl_PL")
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("pl_PL");
            }
            else if(language == "en-US")
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");
            }
            //mainWindow = new MainWindow();
            //mainWindow.InitializeComponent();
            //System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");
        }
    }
}
