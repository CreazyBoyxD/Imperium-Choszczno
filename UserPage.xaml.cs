using Microsoft.Win32;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
using System.Resources;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Data;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Xml;
using Xceed.Wpf.AvalonDock.Themes;

namespace WpfApp1
{
    /// <summary>
    /// Logika interakcji dla klasy UserPage.xaml
    /// </summary>
    public partial class UserPage : Page
    {
        //Bury - dodanie wszystkich funkcjonalności do UserPage
        private DataTable userDataTable, userDataTableAuthors, userDataTableSongs, userDataTableAlbums, userDataTableSongsOfAlbum, userDataTableOrders;
        MySqlDataReader daneUser;
        public BazySQL bazySQL;
        private DataRowView userDataRowSong, dataRowSongOfAlbum, userDataRowAlbum, userDataRowOrder;
        private List<object> userList;
        private int selectedUserSongID = -1;
        private int selectedUserAlbumID = -1;
        private int selectedUserOrderID = -1;
        private int selectedSongOfAlbumID = -1;
        private MySqlDataReader options = null;

        int theme = 0;

        public UserPage(BazySQL obj, MySqlDataReader user, MySqlDataReader optionsFromLogin, int motyw)
        {
            InitializeComponent();
            updateUserList(user);
            bazySQL = obj;
            options = optionsFromLogin;
            addDataOptions();
            getTables();

            Main.Width = 1300;

            RefreshOrderTable((int)userList[0]);
            theme = motyw;
            changeTheme();
        }
        /// <summary>
        /// Funkcje wspólne do obsługi całego layoutu dla widoku usera
        /// </summary>

        //Hubert - Oprogramowanie zmiany motywu dla wszystkich elementów dla UserPage 
        private void changeTheme()
        {
            if (theme == 0)
            {
                foreach (MenuItem mi in MenuTop.Items)
                {
                    mi.Background = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFFFFFFF"));
                }
                foreach (TabItem ti in Tabs.Items)
                {
                    ti.Background = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFFFFFFF"));
                }
                NameOfStudioMainprogram.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF000000"));

                //Tła
                Main.Background = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFFFFFFF"));
                //Grids
                AlbumsGrid.Background = MyData.Background = SongsGrid.Background = OrdersGrid.Background =
                    new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFFFFFFF"));
                //Tekst
                //Zakładka Moje Dane
                UserNameLabel1.Foreground = NameLabel.Foreground = SurnameLabel.Foreground = AddressLabel.Foreground = CityLabel.Foreground = CashLabel.Foreground = CashUserLabel.Foreground =
                    new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF000000"));

                selectedUserUserName.Background = selectedUserName.Background = selectedUserSurname.Background = selectedUserAddress.Background = selectedUserCity.Background =
                    new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFFFFFFF"));
                selectedUserUserName.Foreground = selectedUserName.Foreground = selectedUserSurname.Foreground = selectedUserAddress.Foreground = selectedUserCity.Foreground =
                    new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF000000"));

                //Zakładka albumy
                AlbumListLabel.Foreground = AlbumSongListLabel.Foreground = nameOfAlbumLabel.Foreground = nameOfAlbumLabelShow.Foreground = priceOfSong1.Foreground =
                    priceOfAlbumLabelShow.Foreground = discountOfAlbum_Copy.Foreground = discountOfAlbumLabelShow.Foreground = AlbumImageLabel.Foreground = nameOfSong_Copy.Foreground =
                    nameOfSongAlbumLabelShow.Foreground = nameOfAuthorSong_Copy2.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF000000"));

                //Zakładka utwory
                nameOfSong.Foreground = nameOfAuthorSong_Copy1.Foreground = nameOfSongLabelShow.Foreground = nameOfSong.Foreground = priceOfSongLabelShow.Foreground = discountOfSongLabelShow.Foreground =
                    priceOfSong.Foreground = discountOfSong.Foreground = nameOfAuthorSong_Copy1.Foreground = nameOfAuthorSong_Copy.Foreground = listOfSongs.Foreground =
                    new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF000000"));

                //Zakładka zamowienia
                orderUserNameSurnameLabel.Foreground = orderUserAddressLabel.Foreground = orderProductLabel.Foreground = orderCostLabel.Foreground = orderUserNameSurnameLabel.Foreground =
                    orderUserNameSurnameLabel_Copy1.Foreground = orderUserNameSurnameLabelShow.Foreground = orderUserAddressLabelShow.Foreground = orderProductLabelShow.Foreground = orderCostLabelShow.Foreground =
                   orderUserNameSurnameLabel_Copy.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF000000"));

                //Przyciski

                //Uzytkownicy
                saveUser.Foreground = changePass.Foreground =
                    new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF000000"));
                saveUser.Background = changePass.Background =
                    new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFDDDDDD"));
                //Albumy
                buyThisAlbum.Foreground =
                    new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF000000"));
                buyThisAlbum.Background =
                    new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFDDDDDD"));
                //Utwory
                buyThisSong.Foreground =
                    new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF000000"));
                buyThisSong.Background =
                    new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFDDDDDD"));
                //Zamowienie
                deleteOrder.Foreground =
                    new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF000000"));
                deleteOrder.Background =
                    new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFDDDDDD"));

                theme = 1;
            }
            else if (theme == 1)
            {
                foreach (MenuItem mi in MenuTop.Items)
                {
                    mi.Background = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF696969"));
                }
                foreach (TabItem ti in Tabs.Items)
                {
                    ti.Background = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF696969"));
                }
                NameOfStudioMainprogram.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFE6FF00"));
                //infos

                //Tła
                Main.Background = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF252525"));
                AlbumsGrid.Background = MyData.Background = SongsGrid.Background = OrdersGrid.Background =
                    new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF252525"));
                //Tekst

                //Zakładka Moje Dane
                UserNameLabel1.Foreground = NameLabel.Foreground = SurnameLabel.Foreground = AddressLabel.Foreground = CityLabel.Foreground = CashLabel.Foreground = CashUserLabel.Foreground =
                    new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFE6FF00"));

                selectedUserUserName.Background = selectedUserName.Background = selectedUserSurname.Background = selectedUserAddress.Background = selectedUserCity.Background =
                    new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF696969"));
                selectedUserUserName.Foreground = selectedUserName.Foreground = selectedUserSurname.Foreground = selectedUserAddress.Foreground = selectedUserCity.Foreground =
                    new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFFFFFFF"));

                //Zakładka albumy
                AlbumListLabel.Foreground = AlbumSongListLabel.Foreground = nameOfAlbumLabel.Foreground = nameOfAlbumLabelShow.Foreground = priceOfSong1.Foreground =
                    priceOfAlbumLabelShow.Foreground = discountOfAlbum_Copy.Foreground = discountOfAlbumLabelShow.Foreground = AlbumImageLabel.Foreground = nameOfSong_Copy.Foreground =
                    nameOfSongAlbumLabelShow.Foreground = nameOfAuthorSong_Copy2.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFE6FF00"));

                //Zakładka utwory
                nameOfSong.Foreground = nameOfAuthorSong_Copy1.Foreground = nameOfSongLabelShow.Foreground = nameOfSong.Foreground = priceOfSongLabelShow.Foreground = discountOfSongLabelShow.Foreground =
                    priceOfSong.Foreground = discountOfSong.Foreground = nameOfAuthorSong_Copy1.Foreground = nameOfAuthorSong_Copy.Foreground = listOfSongs.Foreground =
                    new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFE6FF00"));

                //Zakładka zamowienia
                orderUserNameSurnameLabel.Foreground = orderUserAddressLabel.Foreground = orderProductLabel.Foreground = orderCostLabel.Foreground = orderUserNameSurnameLabel.Foreground =
                    orderUserNameSurnameLabel_Copy1.Foreground = orderUserNameSurnameLabelShow.Foreground = orderUserAddressLabelShow.Foreground = orderProductLabelShow.Foreground = orderCostLabelShow.Foreground =
                   orderUserNameSurnameLabel_Copy.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFE6FF00"));


                //Przyciski

                //Uzytkownicy
                saveUser.Foreground = changePass.Foreground =
                    new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFE6FF00"));
                saveUser.Background = changePass.Background =
                    new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF4A4A4A"));
                //Albumy
                buyThisAlbum.Foreground =
                    new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFE6FF00"));
                buyThisAlbum.Background =
                    new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF4A4A4A"));
                //Utwory
                buyThisSong.Foreground =
                    new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFE6FF00"));
                buyThisSong.Background =
                    new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF4A4A4A"));
                //Zamowienie
                deleteOrder.Foreground =
                    new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFE6FF00"));
                deleteOrder.Background =
                    new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF4A4A4A"));

                theme = 0;
            }
        }
        //Hubert - fukcja konwertująca cene na decimal
        private decimal convertPrice(string price)
        {
            decimal defaultValue = 0;
            decimal result;
            //Try parsing in the current culture
            if (!System.Decimal.TryParse(price, System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out result) &&
                //Then try in US english
                !System.Decimal.TryParse(price, System.Globalization.NumberStyles.Any, CultureInfo.GetCultureInfo("en-US"), out result) &&
                //Then in neutral language
                !System.Decimal.TryParse(price, System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out result))
            {
                result = defaultValue;
            }
            return result;
        }
        //Hubert - konwertowanie rabatu z wartości na liczbe, najpierw usuwa procent potem to formatuje do danych
        //Funckja ta i powyższa jest wykorzystywana do obliczania rabatu przy kupowaniu utworu ub albumu
        private int convertDiscount(string discount)
        {
            int rabat = 0;
            string temp = "";
            try
            {
                temp = discount.TrimEnd('%');
            }
            catch (Exception ex)
            {
                temp = "0";
            }
            try
            {
                int.TryParse(temp, out rabat);
            }
            catch (Exception ex)
            {
                rabat = 0;
            }
            return rabat;
        }
        //Bury - Wczytywanie danych z bazy danych do aplikacji
        private void getTables()
        {
            userDataTableSongs = bazySQL.getTables("songs");
            SongsTableUser.ItemsSource = userDataTableSongs.DefaultView;
            userDataTableAlbums = bazySQL.getTables("albums");
            albumUserList.ItemsSource = userDataTableAlbums.DefaultView;
        }
        private void RefreshOrderTable(int UserID)
        {
            userDataTableOrders = bazySQL.getUserOrder(UserID);
            orderUserList.ItemsSource = userDataTableOrders.DefaultView;
        }
        //Bury - Funkcja pomocnicza do pobierania danych po wciśnięciu przycisku wybierz
        //zdjęcie oraz konwertuje pobrane zdjęcie na tablice bitową potrzebną do zapisu danych do bazy danych
        private BitmapImage photoService(byte[] imageBytes)
        {
            BitmapImage imageFromBytes = null;
            if (imageBytes != null)
            {
                imageFromBytes = new BitmapImage();
                using (var mem = new MemoryStream(imageBytes))
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
            return imageFromBytes;
        }

        // Bury - oprogramowanie przycisku wyloguj; po wciśnięciu następuje wylogowanie użytkownika/administratora oraz przeniesienie do okna logowania
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            userList = null;
            options = null;
            userDataRowSong = userDataRowAlbum = userDataRowOrder = null;
            userDataTable = userDataTableAuthors = userDataTableSongs = userDataTableAlbums = userDataTableSongsOfAlbum = userDataTableOrders = null;
            daneUser = null;
            NavigationService.Navigate(new Logowanie(bazySQL));
        }
        //Bury - wczytanie wybranych danych do okna z albumami
        private void albumUserList_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "ID" || e.PropertyName == "FileName" || e.PropertyName == "Image" ||
                e.PropertyName == "Songs" || e.PropertyName == "Price" || e.PropertyName == "Discount")
            {
                e.Cancel = true;
            }
        }
        //Bury - wczytanie wybranych danych do okna z wybramymi utworami w albumie
        private void albumListOfSongs_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "ID" || e.PropertyName == "FileName" || e.PropertyName == "Image" ||
                e.PropertyName == "Authors" || e.PropertyName == "Price" || e.PropertyName == "Discount")
            {
                e.Cancel = true;
            }
        }
        //Bury - wyświetlanie zamówień danego użytwkonika w oknie zamówień
        private void orderUserList_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "ID" || e.PropertyName == "UserID" || e.PropertyName == "ProductID" || e.PropertyName == "ProductType" || e.PropertyName == "Price")
            {
                e.Cancel = true;
            }
        }
        //Bury - Funkcja służąca do wyświetlania informacji takich jak imie nazwisko adres ilość pieniędzy
        private void updateUserList(MySqlDataReader user)
        {
            userList = new List<object>();
            for (int i = 0; i < user.FieldCount; i++)
            {
                userList.Add(user.GetValue(i));
            }
            selectedUserName.Text = userList[4].ToString();
            selectedUserUserName.Text = userList[1].ToString();
            selectedUserSurname.Text = userList[5].ToString();
            selectedUserAddress.Text = userList[7].ToString();
            selectedUserCity.Text = userList[8].ToString();
            CashUserLabel.Content = userList[6].ToString() + "zł";
        }
        //Bury - schowanie okna opcji
        private void addDataOptions()
        {
            NameOfStudioMainprogram.Content = options.GetValue(1).ToString();

            if (!options.IsDBNull("Image"))
            {
                if ((byte[])options.GetValue(4) != null && options.GetValue(4) != DBNull.Value)
                {
                    MainAppLogoImage.Source = (photoService((byte[])options.GetValue(4)));
                }
                else MainAppLogoImage.Source = null;
            }
        }
        //Bury - Funkcja wyświetlająca błąd gdy nie masz wystarczającej liczby pieniędzy
        private void youCantBuyIt()
        {
            string messageBoxText = Properties.Resources.youcantbuymoney;
            string caption = Properties.Resources.notenoughcash;
            MessageBox.Show(messageBoxText, caption, MessageBoxButton.OK);
        }
        //Bury - funkcja pozwalająca na usunięcie swojego zamówienia
        //po wybraniu szukanego zamówienia i wciśnięciu przycisku Anuluj Zamówienie wybrane zamówienie usuwane jest z listy
        //tym samym użytkownik traci do niego dostęp; pieniądze zapłacone podczas kupna danego albumu/zamówienia nie są zwracane
        private void deleteOrder_Click(object sender, RoutedEventArgs e)
        {
            if (selectedUserOrderID != -1)
            {
                bazySQL.deleteUserAuthorSongAlbum("Orders", selectedUserOrderID);
            }
            RefreshOrderTable((int)userList[0]);
        }
        //Bury - funkcja pozwalająca na edytowanie swoich danych osobowych
        //dane podawane przy rejestracji (z wyjatkiem hasla) wyswietlane sa w oknie Moje Konto
        //użytkownik może zmienić swoje dane a wszystkie zmiany zatwierdzic naciskajac przycisk Zapisz
        private void saveUser_Click(object sender, RoutedEventArgs e)
        {
            if ((selectedUserName.Text != null && selectedUserName.Text != "") && (selectedUserUserName.Text != null && selectedUserUserName.Text != "") &&
                (selectedUserSurname.Text != null && selectedUserSurname.Text != "") && (selectedUserAddress.Text != null && selectedUserAddress.Text != "")
                && (selectedUserCity.Text != null && selectedUserCity.Text != ""))
            {
                bazySQL.saveUserFromUserPage((int)userList[0], selectedUserName.Text, selectedUserUserName.Text, selectedUserSurname.Text, selectedUserAddress.Text,
                selectedUserCity.Text);
            }
            MySqlDataReader userTemp = bazySQL.infoAboutUserByID((int)userList[0]);
            updateUserList(userTemp);
        }
        //Hubert - Przycisk umożliwiający zamknięcie aplikacji
        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
        //Hubert - Przycisk umożliwiający zmiane motywu apliakcji
        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            changeTheme();
        }
        //Bury - Przycisk pozwalający na wyświetlenie regulaminu
        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            Regulamin regulamin = new Regulamin(theme);
            regulamin.Show();
        }

        ///
        /// Funkcje obsługi zamówień
        /// 

        //Bury - funckja która wyślietla dane aktualnie wybranego zamówienia w oknie zamówienia
        private void orderUserList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            userDataRowOrder = (DataRowView)orderUserList.SelectedItem;

            if (userDataRowOrder != null)
            {
                MySqlDataReader elementMain, authorSong = null;
                selectedUserOrderID = (int)userDataRowOrder[0];
                orderUserNameSurnameLabelShow.Content = userDataRowOrder[4];
                orderUserAddressLabelShow.Content = userDataRowOrder[6];
                elementMain = bazySQL.getOrderedProductinfo(userDataRowOrder[3].ToString(), int.Parse(userDataRowOrder[2].ToString()));
                if (elementMain != null)
                {
                    if (elementMain.GetValue(3) != DBNull.Value && (byte[])elementMain.GetValue(3) != null)
                    {
                        orderedProductCover.Source = photoService((byte[])elementMain.GetValue(3));
                    }
                    if (userDataRowOrder[3].ToString() == "Songs")
                    {
                        authorSong = bazySQL.getInfoAboutAuthor(int.Parse(elementMain.GetValue(4).ToString()));
                        if (authorSong != null)
                        {
                            orderProductLabelShow.Content = authorSong[1].ToString() + " - " + elementMain[1].ToString();
                        }
                        else
                        {
                            orderProductLabelShow.Content = elementMain[1].ToString();
                        }
                    }
                }
                orderCostLabelShow.Content = userDataRowOrder[5];
            }
        }
        /// <summary>
        /// Funkcje obsługi utworów
        /// </summary>

        //Bury - wyświetlenie wybranych danych w tabeli utworów
        private void SongsTableUser_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "ID" || e.PropertyName == "FileName" || e.PropertyName == "Image"
                || e.PropertyName == "Authors" || e.PropertyName == "Price" || e.PropertyName == "Discount")
            {
                e.Cancel = true;
            }
        }
        //Bury - wyświetlenie aktualnych danych wybranego utworu w oknie utwory po jego kliknięciu
        private void SongsTableUser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            userDataRowSong = (DataRowView)SongsTableUser.SelectedItem;
            AuthorImage.Source = null;
            if (userDataRowSong != null)
            {
                MySqlDataReader author = null;
                selectedUserSongID = (int)userDataRowSong.Row[0];
                int AuthorsID = int.Parse(userDataRowSong.Row[4].ToString());
                bool checkIfAuthorExists = bazySQL.checkIfUserAuthorSongAlbumExist("author", AuthorsID, "");
                if (checkIfAuthorExists)
                {
                    author = bazySQL.getInfoAboutAuthor(AuthorsID);
                    if (author != null)
                    {
                        nameOfSongLabelShow.Content = author.GetValue(1).ToString() + " - " + userDataRowSong.Row[1].ToString();
                        if (author.GetValue(3) != DBNull.Value)
                        {
                            AuthorImage.Source = photoService((byte[])author.GetValue(3));
                        }
                        else AuthorImage.Source = null;
                    }
                    else
                    {
                        nameOfSongLabelShow.Content = userDataRowSong.Row[1].ToString();
                    }
                }
                if (userDataRowSong.Row[3] != DBNull.Value)
                {
                    SongImage.Source = photoService((byte[])userDataRowSong.Row[3]);
                }
                else SongImage.Source = null;
                decimal priceOfSong = convertPrice(userDataRowSong.Row[5].ToString());
                int discountOfSong = convertDiscount(userDataRowSong.Row[6].ToString());
                decimal finishPrice = priceOfSong;
                bool ifCanBuy;
                if (discountOfSong > 0)
                {
                    finishPrice = priceOfSong * (decimal)(1 - ((decimal)discountOfSong / 100));
                }
                priceOfSongLabelShow.Content = finishPrice.ToString() + " zł";
                discountOfSongLabelShow.Content = userDataRowSong.Row[6].ToString();
            }
        }
        ///
        /// Funkcje obsługi albumów
        /// 

        //Bury - pokazanie informacji o albumie w oknie albumy po wciśnięciu na wybrany album
        private void albumUserList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            userDataRowAlbum = (DataRowView)albumUserList.SelectedItem;
            if (userDataRowAlbum != null)
            {
                byte[] byteToImage = null;
                selectedUserAlbumID = Convert.ToInt32(userDataRowAlbum.Row[0]);
                nameOfAlbumLabelShow.Content = userDataRowAlbum.Row[1].ToString();
                if (userDataRowAlbum.Row[3] != DBNull.Value)
                {
                    byteToImage = (byte[])userDataRowAlbum.Row[3];
                    AlbumImage.Source = photoService(byteToImage);
                }

                userDataTableSongsOfAlbum = bazySQL.getSongsOfAlbum(userDataRowAlbum.Row[4].ToString());

                string[] songList = userDataRowAlbum.Row[4].ToString().Split(',');
                albumListOfSongs.ItemsSource = null;
                albumListOfSongs.ItemsSource = userDataTableSongsOfAlbum.DefaultView;

                decimal priceOfAlbum = convertPrice(userDataRowAlbum.Row[5].ToString());
                int discountOfAlbum = convertDiscount(userDataRowAlbum.Row[6].ToString());
                decimal finishPrice = priceOfAlbum;
                if (discountOfAlbum > 0)
                {
                    finishPrice = priceOfAlbum * (decimal)(1 - ((decimal)discountOfAlbum / 100));
                }
                priceOfAlbumLabelShow.Content = finishPrice.ToString() + " zł";
                discountOfAlbumLabelShow.Content = userDataRowAlbum.Row[6].ToString();
            }
        }
        //Bury - Wyświetlenie listy utwrów wybranego albumu
        private void albumListOfSongs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dataRowSongOfAlbum = (DataRowView)albumListOfSongs.SelectedItem;
            AuthorImage.Source = null;
            if (dataRowSongOfAlbum != null)
            {
                MySqlDataReader authorOfSongAlbum = null;
                selectedSongOfAlbumID = (int)dataRowSongOfAlbum.Row[0];
                int AuthorsID = int.Parse(dataRowSongOfAlbum.Row[4].ToString());
                bool checkIfAuthorExists = bazySQL.checkIfUserAuthorSongAlbumExist("author", AuthorsID, "");
                if (checkIfAuthorExists)
                {
                    authorOfSongAlbum = bazySQL.getInfoAboutAuthor(AuthorsID);
                    if (authorOfSongAlbum != null)
                    {
                        nameOfSongAlbumLabelShow.Content = authorOfSongAlbum.GetValue(1).ToString() + " - " + dataRowSongOfAlbum.Row[1].ToString();
                    }
                    else
                    {
                        nameOfSongAlbumLabelShow.Content = dataRowSongOfAlbum.Row[1].ToString();
                    }
                }
                if (dataRowSongOfAlbum.Row[3] != DBNull.Value)
                {
                    SongInAlbumImage.Source = photoService((byte[])dataRowSongOfAlbum.Row[3]);
                }
            }
        }
        ///
        /// Funkcje obsługi kupowania
        ///

        //Bury - Funkcja pozwalająca na kupienie wybranego utworu, sprawdza czy użytkownik ma wystarczająco pieniędzy
        //Po naciśnięciu otwierane jest okno zawierające dane dot. zamówienia oraz zawierające przycisk potwierdzający zamówienie
        //po potwierdzeniu zamówienia odpowiednia ilość pieniędzy zabierana jest z konta użytkownika a zakupiony utwór pokazuje się w zakładce zamówienia
        private void buyThisSong_Click(object sender, RoutedEventArgs e)
        {
            string[] infosFormOrder = new string[4];
            OrderConfirmation window1 = new OrderConfirmation(userList, theme);
            if (window1.ShowDialog() == true)
            {
                infosFormOrder = window1.Answer;
            }
            if (userDataRowSong != null)
            {
                decimal priceOfSong = convertPrice(userDataRowSong.Row[5].ToString());
                int discountOfSong = convertDiscount(userDataRowSong.Row[6].ToString());
                decimal finishPrice = priceOfSong;
                decimal difference = 0;
                bool ifCanBuy;
                if (discountOfSong > 0)
                {
                    finishPrice = priceOfSong * (decimal)(1 - ((decimal)discountOfSong / 100));
                }

                decimal wallet = convertPrice(userList[6].ToString());
                if (wallet >= finishPrice)
                {
                    difference = wallet - finishPrice;
                    int SongID = (int)userDataRowSong.Row[0];
                    if (infosFormOrder != null)
                    {
                        if ((infosFormOrder[0] != null && infosFormOrder[0] != "") && (infosFormOrder[1] != null && infosFormOrder[1] != "") &&
                            (infosFormOrder[2] != null && infosFormOrder[2] != "") && (infosFormOrder[3] != null && infosFormOrder[3] != ""))
                        {
                            bazySQL.addSaveOrder(1, (int)userList[0], SongID, "Songs", String.Format(infosFormOrder[0] + " " + infosFormOrder[1]), finishPrice.ToString().Replace(',', '.'), String.Format(infosFormOrder[2] + " " + infosFormOrder[3]), -1);
                        }
                        else
                        {
                            bazySQL.addSaveOrder(1, (int)userList[0], SongID, "Songs", String.Format(userList[4] + " " + userList[5]), finishPrice.ToString().Replace(',', '.'), String.Format(userList[7] + " " + userList[8]), -1);
                        }
                    }
                    else
                    {
                        bazySQL.addSaveOrder(1, (int)userList[0], SongID, "Songs", String.Format(userList[4] + " " + userList[5]), finishPrice.ToString().Replace(',', '.'), String.Format(userList[7] + " " + userList[8]), -1);
                    }
                    bazySQL.updateUserCash((int)userList[0], userList[1].ToString(), difference.ToString());
                    MySqlDataReader userTemp = bazySQL.infoAboutUserByID((int)userList[0]);
                    updateUserList(userTemp);
                    RefreshOrderTable((int)userList[0]);
                }
                else
                {
                    youCantBuyIt();
                }
            }
        }
        //Bury - Funkcja pozwalająca na kupienie wybranego albumu, sprawdza czy użytkownik ma wystarczająco pieniędzy
        //Po naciśnięciu otwierane jest okno zawierające dane dot. zamówienia oraz zawierające przycisk potwierdzający zamówienie
        //po potwierdzeniu zamówienia odpowiednia ilość pieniędzy zabierana jest z konta użytkownika a zakupiony album pokazuje się w zakładce zamówienia
        private void buyThisAlbum_Click(object sender, RoutedEventArgs e)
        {
            string[] infosFormOrder = new string[4];
            OrderConfirmation window1 = new OrderConfirmation(userList, theme);
            if (window1.ShowDialog() == true)
            {
                infosFormOrder = window1.Answer;
            }
            if (userDataRowAlbum != null)
            {
                decimal priceOfAlbum = convertPrice(userDataRowAlbum.Row[5].ToString());
                int discountOfAlbum = convertDiscount(userDataRowAlbum.Row[6].ToString());
                decimal finishPrice = priceOfAlbum;
                decimal difference = 0;
                bool ifCanBuy;
                if (discountOfAlbum > 0)
                {
                    finishPrice = priceOfAlbum * (decimal)(1 - ((decimal)discountOfAlbum / 100));
                }

                decimal wallet = convertPrice(userList[6].ToString());
                if (wallet >= finishPrice)
                {
                    difference = wallet - finishPrice;
                    int AlbumID = (int)userDataRowAlbum.Row[0];
                    if (infosFormOrder != null)
                    {
                        if ((infosFormOrder[0] != null && infosFormOrder[0] != "") && (infosFormOrder[1] != null && infosFormOrder[1] != "") &&
                            (infosFormOrder[2] != null && infosFormOrder[2] != "") && (infosFormOrder[3] != null && infosFormOrder[3] != ""))
                        {
                            bazySQL.addSaveOrder(1, (int)userList[0], AlbumID, "Albums", String.Format(infosFormOrder[0] + " " + infosFormOrder[1]), finishPrice.ToString().Replace(',', '.'), String.Format(infosFormOrder[2] + " " + infosFormOrder[3]), -1);
                        }
                        else
                        {
                            bazySQL.addSaveOrder(1, (int)userList[0], AlbumID, "Albums", String.Format(userList[4] + " " + userList[5]), finishPrice.ToString().Replace(',', '.'), String.Format(userList[7] + " " + userList[8]), -1);
                        }
                    }
                    else
                    {
                        bazySQL.addSaveOrder(1, (int)userList[0], AlbumID, "Albums", String.Format(userList[4] + " " + userList[5]), finishPrice.ToString().Replace(',', '.'), String.Format(userList[7] + " " + userList[8]), -1);
                    }
                    bazySQL.addSaveOrder(1, (int)userList[0], selectedUserAlbumID, "Albums", "Something", finishPrice.ToString().Replace(',', '.'), String.Format(userList[7] + " " + userList[8]), -1);
                    bazySQL.updateUserCash((int)userList[0], userList[1].ToString(), difference.ToString());
                    MySqlDataReader userTemp = bazySQL.infoAboutUserByID((int)userList[0]);
                    updateUserList(userTemp);
                    RefreshOrderTable((int)userList[0]);
                    //OrderTable();
                }
                else
                {
                    youCantBuyIt();
                }
            }
        }
        ///
        /// Funkcje obsługi usera
        ///

        // Bury - zmiana hasła dostępu do konta
        //po wciśnięciu przycisku Zmień Hasło otwiera się nowe okno z tekstboksami do wypełnienia w których wpisujemy nowe hasło
        //jeżeli hasło i powtórz hasło zostaną poprawnie wypełnione, hasło dostępu do konta zostaje zmienione
        private void changePass_Click(object sender, RoutedEventArgs e)
        {
            string NewPass = "";
            changePassword windowChPass = new changePassword(theme);
            if (windowChPass.ShowDialog() == true)
            {
                NewPass = windowChPass.Answer;
            }
            bazySQL.changeUserPassword((int)userList[0], userList[1].ToString(), NewPass);
        }
    }
}