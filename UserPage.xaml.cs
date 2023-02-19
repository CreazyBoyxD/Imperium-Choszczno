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

                /* //DataGridy
                 //Uzytkownicy
                 usersTable.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF000000"));
                 usersTable.Background = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFFFFFFF"));
                 //Albumy
                 albumList.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF000000"));
                 albumList.Background= new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFFFFFFF"));
                 albumListOfSongs.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF000000"));
                 albumListOfSongs.Background = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFFFFFFF"));
                 listOfSongsAlbum.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF000000"));
                 listOfSongsAlbum.Background = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFFFFFFF"));
                 //Tworcy
                 authorTable.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF000000"));
                 authorTable.Background = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFFFFFFF"));
                 //Utwory
                 SongsTable.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF000000"));
                 SongsTable.Background = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFFFFFFF"));
                 //Zamowienie
                 orderList.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF000000"));
                 orderList.Background = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFFFFFFF"));*/

                theme = 1;
            }
            else if (theme == 1)
            {
                foreach (MenuItem mi in MenuTop.Items)
                {
                    mi.Background = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF696969"));
                    //mi.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFFFFFFF"));
                }
                foreach (TabItem ti in Tabs.Items)
                {
                    ti.Background = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF696969"));
                    //ti.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFFFFFFF"));
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

                /*//DataGridy
                //Uzytkownicy
                usersTable.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFFFFFFF"));
                usersTable.Background = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF000000"));
                //Albumy
                albumList.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFFFFFFF"));
                albumList.Background = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF000000"));
                albumListOfSongs.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFFFFFFF"));
                albumListOfSongs.Background = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF000000"));
                listOfSongsAlbum.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFFFFFFF"));
                listOfSongsAlbum.Background = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF000000"));
                //Tworcy
                authorTable.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFFFFFFF"));
                authorTable.Background = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF000000"));
                //Utwory
                SongsTable.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFFFFFFF"));
                SongsTable.Background = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF000000"));
                //Zamowienie
                orderList.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFFFFFFF"));
                orderList.Background = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF000000"));*/

                theme = 0;
            }
        }
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
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            userList = null;
            options = null;
            userDataRowSong = userDataRowAlbum = userDataRowOrder = null;
            userDataTable = userDataTableAuthors = userDataTableSongs = userDataTableAlbums = userDataTableSongsOfAlbum = userDataTableOrders = null;
            daneUser = null;
            NavigationService.Navigate(new Logowanie(bazySQL));
        }
        private void albumUserList_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "ID" || e.PropertyName == "FileName" || e.PropertyName == "Image" ||
                e.PropertyName == "Songs" || e.PropertyName == "Price" || e.PropertyName == "Discount")
            {
                e.Cancel = true;
            }
        }
        private void albumListOfSongs_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "ID" || e.PropertyName == "FileName" || e.PropertyName == "Image" ||
                e.PropertyName == "Authors" || e.PropertyName == "Price" || e.PropertyName == "Discount")
            {
                e.Cancel = true;
            }
        }
        private void orderUserList_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "ID" || e.PropertyName == "UserID" || e.PropertyName == "ProductID" || e.PropertyName == "ProductType" || e.PropertyName == "Price")
            {
                e.Cancel = true;
            }
        }
        private void updateUserList(MySqlDataReader user)
        {
            userList = new List<object>();
            for (int i = 0; i < user.FieldCount; i++)
            {
                userList.Add(user.GetValue(i));
            }
            selectedUserName.Text = userList[1].ToString();
            selectedUserUserName.Text = userList[4].ToString();
            selectedUserSurname.Text = userList[5].ToString();
            selectedUserAddress.Text = userList[7].ToString();
            selectedUserCity.Text = userList[8].ToString();
            CashUserLabel.Content = userList[6].ToString() + "zł";
        }
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
        private void youCantBuyIt()
        {
            string messageBoxText = Properties.Resources.youcantbuymoney;
            string caption = Properties.Resources.notenoughcash;
            MessageBox.Show(messageBoxText, caption, MessageBoxButton.OK);
        }

        private void deleteOrder_Click(object sender, RoutedEventArgs e)
        {
            if (selectedUserOrderID != -1)
            {
                bazySQL.deleteUserAuthorSongAlbum("Orders", selectedUserOrderID);
            }
            RefreshOrderTable((int)userList[0]);
        }

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

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            changeTheme();
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            Regulamin regulamin = new Regulamin(theme);
            regulamin.Show();
        }

        ///
        /// Funkcje obsługi zamówień
        /// 
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
        private void SongsTableUser_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "ID" || e.PropertyName == "FileName" || e.PropertyName == "Image"
                || e.PropertyName == "Authors" || e.PropertyName == "Price" || e.PropertyName == "Discount")
            {
                e.Cancel = true;
            }
        }
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
        private void changePass_Click(object sender, RoutedEventArgs e) //zmiana hasla do konta
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