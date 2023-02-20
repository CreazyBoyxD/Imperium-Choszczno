using MySql.Data.MySqlClient;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public BazySQL bazySQL;
        public Logi logi;
        MySqlDataReader options;

        public MainWindow()
        {
            logi = new Logi(); // stworzenie obiektu logów
            logi.checkOrCreateFile();
            InitializeComponent();
            bazySQL = new BazySQL(); //stowrzenie obiektu bazy danych
            options = bazySQL.getOptionTable(); // baza danych pobiera dane o programie, a konkretnie język
            string language = options.GetValue(3).ToString();
            if (language == "pl_PL")
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("pl_PL");
            }
            else if (language == "en-US")
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");
            }
            bazySQL.CheckAndCreateDB();
            Start(); // wystartowanie okna logowania
        }
        public void Start()
        {
            Logowanie logow = new Logowanie(bazySQL);
            Main.NavigationService.Navigate(logow);
        }

    }
}
