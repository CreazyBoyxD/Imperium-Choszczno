using Microsoft.Win32;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Printing;
using System.Security.Policy;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Xceed.Wpf.AvalonDock.Themes;


namespace WpfApp1
{
    /// <summary>
    /// Logika interakcji dla klasy AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    { //Bury - dodanie wszystkich funkcjonalności do AdminPage
        private DataTable dataTable, dataTableAuthors, dataTableSongs, dataTableAlbums, dataTableSongsOfAlbum, dataTableOrders;
        public BazySQL bazySQL;
        private DataRowView dataRowAuthor, dataRowSong, dataRowAlbum, dataRowOrder;
        private List<object> userList;
        private List<string> discounts;
        private IList songList = null;
        private int saveUserID = 0;
        private string AuthorImageFilePath, AuthorImageFileName;
        private int selectedAuthorID = -1;
        private int selectedSongID = -1;
        private int selectedAlbumID = -1;
        private int selectedOrderID = -1;
        private MySqlDataReader options = null;
        private int theme = 0;

        //public Image AuthorImage; 
        public OpenFileDialog ofd;
        byte[] BinaryDataImage, BinaryDataImageSong, BinaryDataImageAlbum, BinaryDataImageStudio;
        Regex regexCeny = new Regex("^\\d{0,5}\\.\\d{1,2}$");
        public AdminPage(BazySQL obj, MySqlDataReader user, MySqlDataReader optionsFromLogin, int motyw)
        {

            InitializeComponent();
            updateUserList(user);
            bazySQL = obj;


            UsersTable();
            options = optionsFromLogin;
            addDiscountsToLists();
            addDataOptions();
            AuthorsTable();
            addAuthorsToCombobox();
            SongsTableRefresh();
            OrderTable();
            theme = (int)userList[11];
            changeTheme();
            songList = null;
        }
        private async Task checkConnect()
        {
            /*Thread.Sleep(1500);
            bool connect = bazySQL.CheckConnectToDataBase(bazySQL.makeMySQLConnString());
            if (connect) 
            {
                databaseConnection.IsChecked = true;
                databaseConnection.Content = "Connected to database";
                databaseConnection.Foreground = new SolidColorBrush(Colors.Green);
            }
            else 
            {
                databaseConnection.IsChecked = true;
                databaseConnection.Content = "Problem with connection";
                databaseConnection.Foreground = new SolidColorBrush(Colors.Red);
            }
            checkConnect();*/
        }
        /// <summary>
        /// Funkcje wspólne
        /// </summary>

        // Hubert - Oprogramowanie zmiany motywu dla wszystkich elementów dla AdminPage
        private void changeTheme()
        {
            if (theme == 0)
            {
                foreach (MenuItem mi in MenuTop.Items)
                {
                    mi.Background = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFFFFFFF"));
                    //mi.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF000000"));
                }
                foreach (TabItem ti in Tabs.Items)
                {
                    ti.Background = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFFFFFFF"));
                    //ti.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF000000"));
                }
                NameOfStudioMainprogram.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF000000"));
                //infos
                UserNameIDLabel.Foreground = UserNameLabel.Foreground = UserWalletLabel.Foreground =
                    new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF000000"));
                //Tła
                Main.Background = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFFFFFFF"));
                //Grids
                OptionsGrid.Background = UsersGrid.Background = AlbumsGrid.Background = AuthorsGrid.Background = SongsGrid.Background = OrdersGrid.Background =
                    new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFFFFFFF"));
                //Tekst
                //Zakładka studio
                coverOfStudioLabel.Foreground = nameOfStudioLabel.Foreground = keyProductLabel.Foreground = cmbLocalizationLabel.Foreground =
                    new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF000000"));
                nameOfStudio.Foreground = KeyProduct.Foreground =
                    new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF000000"));
                nameOfStudio.Background = KeyProduct.Background =
                    new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFFFFFFF"));

                //Zakładka uzytkownicy
                UserNameLabel1.Foreground = NameLabel.Foreground = SurnameLabel.Foreground = AddressLabel.Foreground = CityLabel.Foreground = CashLabel.Foreground = AdminLabel.Foreground =
                    new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF000000"));

                selectedUserUserName.Background = selectedUserName.Background = selectedUserSurname.Background = selectedUserAddress.Background = selectedUserCity.Background = selectedUserCash.Background = selectedUserAdmin.Background =
                    new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFFFFFFF"));
                selectedUserUserName.Foreground = selectedUserName.Foreground = selectedUserSurname.Foreground = selectedUserAddress.Foreground = selectedUserCity.Foreground = selectedUserCash.Foreground = selectedUserAdmin.Foreground =
                    new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF000000"));

                //Zakładka albumy
                AlbumListLabel.Foreground = AlbumSongListLabel.Foreground = ListOfAvailableSongsLabel.Foreground = selectedAlbumIDLabel.Foreground = selectedAlbumSongsIDLabel.Foreground =
                    nameOfAlbumLabel.Foreground = albumSelectedImagePath.Foreground = albumSelectedImageNameFile.Foreground = priceOfAlbum.Foreground = discountOfAlbum_Copy.Foreground =
                    AlbumImageLabel.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF000000"));

                selectedAlbumIDTXT.Background = selectedAlbumSongsIDTXT.Background = nameOfAlbum.Background = selectedImageFilePathAlbum.Background = selectedImageFileNameAlbum.Background =
                    priceOfAlbumTXT.Background = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFFFFFFF"));
                selectedAlbumIDTXT.Foreground = selectedAlbumSongsIDTXT.Foreground = nameOfAlbum.Foreground = selectedImageFilePathAlbum.Foreground = selectedImageFileNameAlbum.Foreground =
                    priceOfAlbumTXT.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF000000"));

                //Zakładka utwory
                listOfSongsLabel.Foreground = nameOfAuthorSong_Copy1.Foreground = selectedSongIDsLabel.Foreground = nameOfSong.Foreground = songSelectedImagePath.Foreground = songSelectedImageNameFile.Foreground =
                    priceOfSong.Foreground = discountOfSong.Foreground = nameOfAuthorSong.Foreground = nameOfAuthorSong_Copy.Foreground =
                    new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF000000"));

                selectedSongIDsTXT.Foreground = nameOfSong1.Foreground = selectedImageFilePathSong.Foreground = selectedImageFileNameSong.Foreground = priceOfSongTXT.Foreground
                    = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF000000"));
                selectedSongIDsTXT.Background = nameOfSong1.Background = selectedImageFilePathSong.Background = selectedImageFileNameSong.Background = priceOfSongTXT.Background
                     = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFFFFFFF"));

                //Zakładka autorzy
                listOfAuthorsLabel.Foreground = nameOfAuthorSong_Copy2.Foreground = selectedIDsAuthorLabel.Foreground = nameOfAuthorLabel.Foreground = nameOfPathAuthorLabel.Foreground =
                    nameOfFileImageAuthorLabel.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF000000"));

                selectedIDsAuthorsTXT.Background = nameOfAuthor.Background = selectedFilePath.Background = selectedFileName.Background
                    = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFFFFFFF"));
                selectedIDsAuthorsTXT.Foreground = nameOfAuthor.Foreground = selectedFilePath.Foreground = selectedFileName.Foreground
                    = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF000000"));

                //Zakładka zamowienia
                listOfOrdersLabel.Foreground = orderProductCoverLabel.Foreground = selectedOrderIDsLabel.Foreground = orderUserIDLabel.Foreground = orderUserNameSurnameLabel.Foreground = orderUserAddressLabel.Foreground =
                    orderProductLabel.Foreground = orderCostLabel.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF000000"));

                selectedOrderIDsTXT.Foreground = orderUserIDLabelTXT.Foreground = orderUserNameSurnameLabelTXT.Foreground = orderUserAddressTXT.Foreground = orderProductTXT.Foreground = orderCostTXT.Foreground =
                    new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF000000"));
                selectedOrderIDsTXT.Background = orderUserIDLabelTXT.Background = orderUserNameSurnameLabelTXT.Background = orderUserAddressTXT.Background = orderProductTXT.Background = orderCostTXT.Background =
                    new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFFFFFFF"));

                //Przyciski
                // Studio
                selectPhotoStudio.Foreground = saveStudioOptions.Foreground =
                    new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF000000"));
                selectPhotoStudio.Background = saveStudioOptions.Background =
                    new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFDDDDDD"));
                //Uzytkownicy
                saveUser.Foreground = deleteUser.Foreground =
                    new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF000000"));
                saveUser.Background = deleteUser.Background =
                    new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFDDDDDD"));
                //Albumy
                selectAlbumPhotoImage.Foreground = buyThisAlbum.Foreground = saveAlbum.Foreground = deleteAlbum.Foreground =
                    new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF000000"));
                selectAlbumPhotoImage.Background = buyThisAlbum.Background = saveAlbum.Background = deleteAlbum.Background =
                    new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFDDDDDD"));
                //Autorzy
                saveAuthor.Foreground = deleteAuthor.Foreground = selectPhoto.Foreground =
                    new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF000000"));
                saveAuthor.Background = deleteAuthor.Background = selectPhoto.Background =
                    new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFDDDDDD"));
                //Utwory
                selectPhotoSong.Foreground = saveSong.Foreground = deleteSong.Foreground = buyThisSong.Foreground =
                    new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF000000"));
                selectPhotoSong.Background = saveSong.Background = deleteSong.Background = buyThisSong.Background =
                    new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFDDDDDD"));
                //Zamowienie
                saveOrder.Foreground = deleteOrder.Foreground =
                    new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF000000"));
                saveOrder.Background = deleteOrder.Background =
                    new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFDDDDDD"));

                bazySQL.updateUserTheme((int)userList[0], theme);
                MySqlDataReader userTemp = bazySQL.infoAboutUserByID((int)userList[0]);
                updateUserList(userTemp);
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
                UserNameIDLabel.Foreground = UserNameLabel.Foreground = UserWalletLabel.Foreground =
                    new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFE6FF00"));
                //Tła
                Main.Background = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF252525"));
                OptionsGrid.Background = UsersGrid.Background = AlbumsGrid.Background = AuthorsGrid.Background = SongsGrid.Background = OrdersGrid.Background =
                    new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF252525"));
                //Tekst
                //Zakładka studio
                coverOfStudioLabel.Foreground = nameOfStudioLabel.Foreground = keyProductLabel.Foreground =
                    new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFE6FF00"));

                nameOfStudio.Foreground = KeyProduct.Foreground =
                    new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFFFFFFF"));
                nameOfStudio.Background = KeyProduct.Background =
                    new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF696969"));

                //Zakładka uzytkownicy
                UserNameLabel1.Foreground = NameLabel.Foreground = SurnameLabel.Foreground = AddressLabel.Foreground = CityLabel.Foreground = CashLabel.Foreground = AdminLabel.Foreground =
                    new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFE6FF00"));

                selectedUserUserName.Background = selectedUserName.Background = selectedUserSurname.Background = selectedUserAddress.Background = selectedUserCity.Background = selectedUserCash.Background = selectedUserAdmin.Background =
                    new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF696969"));
                selectedUserUserName.Foreground = selectedUserName.Foreground = selectedUserSurname.Foreground = selectedUserAddress.Foreground = selectedUserCity.Foreground = selectedUserCash.Foreground = selectedUserAdmin.Foreground =
                    new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFFFFFFF"));

                //Zakładka albumy
                AlbumListLabel.Foreground = AlbumSongListLabel.Foreground = ListOfAvailableSongsLabel.Foreground = selectedAlbumIDLabel.Foreground = selectedAlbumSongsIDLabel.Foreground =
                    nameOfAlbumLabel.Foreground = albumSelectedImagePath.Foreground = albumSelectedImageNameFile.Foreground = priceOfAlbum.Foreground = discountOfAlbum_Copy.Foreground =
                    AlbumImageLabel.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFE6FF00"));

                selectedAlbumIDTXT.Background = selectedAlbumSongsIDTXT.Background = nameOfAlbum.Background = selectedImageFilePathAlbum.Background = selectedImageFileNameAlbum.Background =
                    priceOfAlbumTXT.Background = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF696969"));
                selectedAlbumIDTXT.Foreground = selectedAlbumSongsIDTXT.Foreground = nameOfAlbum.Foreground = selectedImageFilePathAlbum.Foreground = selectedImageFileNameAlbum.Foreground =
                    priceOfAlbumTXT.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFFFFFFF"));

                //Zakładka utwory
                listOfSongsLabel.Foreground = nameOfAuthorSong_Copy1.Foreground = selectedSongIDsLabel.Foreground = nameOfSong.Foreground = songSelectedImagePath.Foreground = songSelectedImageNameFile.Foreground =
                    priceOfSong.Foreground = discountOfSong.Foreground = nameOfAuthorSong.Foreground = nameOfAuthorSong_Copy.Foreground =
                    new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFE6FF00"));

                selectedSongIDsTXT.Foreground = nameOfSong1.Foreground = selectedImageFilePathSong.Foreground = selectedImageFileNameSong.Foreground = priceOfSongTXT.Foreground
                    = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFFFFFFF"));
                selectedSongIDsTXT.Background = nameOfSong1.Background = selectedImageFilePathSong.Background = selectedImageFileNameSong.Background = priceOfSongTXT.Background
                    = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF696969"));

                //Zakładka autorzy
                listOfAuthorsLabel.Foreground = nameOfAuthorSong_Copy2.Foreground = selectedIDsAuthorLabel.Foreground = nameOfAuthorLabel.Foreground = nameOfPathAuthorLabel.Foreground =
                    nameOfFileImageAuthorLabel.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFE6FF00"));

                selectedIDsAuthorsTXT.Background = nameOfAuthor.Background = selectedFilePath.Background = selectedFileName.Background
                    = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF696969"));
                selectedIDsAuthorsTXT.Foreground = nameOfAuthor.Foreground = selectedFilePath.Foreground = selectedFileName.Foreground
                    = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFFFFFFF"));

                //Zakładka zamowienia
                listOfOrdersLabel.Foreground = orderProductCoverLabel.Foreground = selectedOrderIDsLabel.Foreground = orderUserIDLabel.Foreground = orderUserNameSurnameLabel.Foreground = orderUserAddressLabel.Foreground =
                    orderProductLabel.Foreground = orderCostLabel.Foreground = new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFE6FF00"));

                selectedOrderIDsTXT.Foreground = orderUserIDLabelTXT.Foreground = orderUserNameSurnameLabelTXT.Foreground = orderUserAddressTXT.Foreground = orderProductTXT.Foreground = orderCostTXT.Foreground =
                    new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFFFFFFF"));
                selectedOrderIDsTXT.Background = orderUserIDLabelTXT.Background = orderUserNameSurnameLabelTXT.Background = orderUserAddressTXT.Background = orderProductTXT.Background = orderCostTXT.Background =
                    new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF696969"));

                //Przyciski
                // Studio
                selectPhotoStudio.Foreground = saveStudioOptions.Foreground =
                    new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFE6FF00"));
                selectPhotoStudio.Background = saveStudioOptions.Background =
                    new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF4A4A4A"));
                //Uzytkownicy
                saveUser.Foreground = deleteUser.Foreground =
                    new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFE6FF00"));
                saveUser.Background = deleteUser.Background =
                    new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF4A4A4A"));
                //Albumy
                selectAlbumPhotoImage.Foreground = buyThisAlbum.Foreground = saveAlbum.Foreground = deleteAlbum.Foreground =
                    new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFE6FF00"));
                selectAlbumPhotoImage.Background = buyThisAlbum.Background = saveAlbum.Background = deleteAlbum.Background =
                    new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF4A4A4A"));
                //Autorzy
                saveAuthor.Foreground = deleteAuthor.Foreground = selectPhoto.Foreground =
                    new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFE6FF00"));
                saveAuthor.Background = deleteAuthor.Background = selectPhoto.Background =
                    new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF4A4A4A"));
                //Utwory
                selectPhotoSong.Foreground = saveSong.Foreground = deleteSong.Foreground = buyThisSong.Foreground =
                    new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFE6FF00"));
                selectPhotoSong.Background = saveSong.Background = deleteSong.Background = buyThisSong.Background =
                    new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF4A4A4A"));
                //Zamowienie
                saveOrder.Foreground = deleteOrder.Foreground =
                    new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FFE6FF00"));
                saveOrder.Background = deleteOrder.Background =
                    new SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF4A4A4A"));
                bazySQL.updateUserTheme((int)userList[0], theme);
                MySqlDataReader userTemp = bazySQL.infoAboutUserByID((int)userList[0]);
                updateUserList(userTemp);
                theme = 0;
            }

        }
        //Bury - Funkcja wyświetlająca błąd gdy nie masz wystarczającej liczby pieniędzy
        private void youCantBuyIt()
        {
            string messageBoxText = Properties.Resources.youcantbuymoney;
            string caption = Properties.Resources.notenoughcash;
            MessageBox.Show(messageBoxText, caption, MessageBoxButton.OK);
        }
        //Bury - Funkcja pomocnicza do pobierania danych po wciśnięciu przycisku wybierz
        //zdjęcie oraz konwertuje pobrane zdjęcie na tablice bitową potrzebną do zapisu danych do bazy danych
        public byte[] GetImageSelectPhoto(string type)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.Filter = "Image format files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";
            openFileDialog.FilterIndex = 1;
            openFileDialog.Multiselect = false;
            openFileDialog.RestoreDirectory = true;

            //bool? result = openFileDialog.ShowDialog;

            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                if (type == "author")
                {
                    AuthorImage.Source = new BitmapImage(new Uri(filePath));
                    selectedFilePath.Text = filePath;
                    selectedFileName.Text = openFileDialog.SafeFileName;
                }
                else if (type == "song")
                {
                    SongImage.Source = new BitmapImage(new Uri(filePath));
                    selectedImageFilePathSong.Text = filePath;
                    selectedImageFileNameSong.Text = openFileDialog.SafeFileName;
                }
                else if (type == "album")
                {
                    AlbumImage.Source = new BitmapImage(new Uri(filePath));
                    selectedImageFilePathAlbum.Text = filePath;
                    selectedImageFileNameAlbum.Text = openFileDialog.SafeFileName;
                }
                else if (type == "studio")
                {
                    StudioImage.Source = new BitmapImage(new Uri(filePath));
                }
                string ext = System.IO.Path.GetExtension(openFileDialog.SafeFileName);
                MemoryStream memStream = new MemoryStream();
                if (ext == ".jpeg" || ext == ".jpg")
                {
                    JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(new Uri(filePath)));
                    encoder.Save(memStream);
                }
                if (ext == ".png")
                {
                    PngBitmapEncoder encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(new Uri(filePath)));
                    encoder.Save(memStream);
                }
                byte[] ready = memStream.ToArray();
                return ready;
            }
            else return null;
        }
        //Hubert - dodanie rabatu do listy od 0 do 100% 
        public void addDiscountsToLists()
        {
            discounts = new List<string>();
            for (int i = 0; i < 105; i = i + 5)
            {
                discounts.Add(i.ToString() + "%");
            }
            cmbSongDiscount.ItemsSource = discounts;
            cmbSongDiscount.SelectedIndex = 0;
            cmbAlbumDiscount.ItemsSource = discounts;
            cmbAlbumDiscount.SelectedIndex = 0;
        }
        //Bury - funckja dostaje tablice bajtów i konwertuje ją na obraz bitmapowy tak aby można go było wgrać to elemetu typu image
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
        //Bury - Funkcja służąca do wyświetlania w prawym górym rogu informacji takich jak nazwa użytkownika i ilość pieniędzy
        private void updateUserList(MySqlDataReader user)
        {
            userList = new List<object>();
            for (int i = 0; i < user.FieldCount; i++)
            {
                userList.Add(user.GetValue(i));
            }
            UserNameIDLabel.Content = "User ID: " + userList[0].ToString();
            UserNameLabel.Content = "User: " + userList[1].ToString();
            UserWalletLabel.Content = "Wallet: " + userList[6].ToString() + " zł";
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
        /// <summary>
        /// Funkcje obsługi opcji
        /// </summary>

        //Bury - Funkcja wczytywania danych do okna opcji
        //Pobieranie informacji o języku, nazwie studia, kluczu produktu oraz logo aplikacji
        private void addDataOptions()
        {
            cmbLocalization.Items.Add("pl_PL");
            cmbLocalization.Items.Add("en_US");
            NameOfStudioMainprogram.Content = options.GetValue(1).ToString();
            //MainApp1.Title = options.GetValue(1).ToString();
            nameOfStudio.Text = options.GetValue(1).ToString();
            KeyProduct.Text = options.GetValue(2).ToString();
            if (!options.IsDBNull("Image"))
            {
                if ((byte[])options.GetValue(4) != null && options.GetValue(4) != DBNull.Value)
                {
                    MainAppLogoImage.Source = photoService((byte[])options.GetValue(4));
                    StudioImage.Source = photoService((byte[])options.GetValue(4));
                }
            }
            try
            {
                cmbLocalization.SelectedItem = options.GetValue(3).ToString();
            }
            catch (Exception ex)
            {
                cmbLocalization.SelectedIndex = 0;
            }
        }
        //Bury - funkcja do wykorzystania zdjęcia z dysku komputera w aplikacji 
        private void selectPhotoStudio_Click(object sender, RoutedEventArgs e)
        {
            BinaryDataImageStudio = GetImageSelectPhoto("studio");
        }
        //Bury - funckja zapisująca dane które podaliśmy w opcjach oraz sprawdza czy dane w textboxach są puste
        private void saveStudioOptions_Click(object sender, RoutedEventArgs e)
        {
            if ((nameOfStudio.Text != "" && nameOfStudio.Text != string.Empty) && (KeyProduct.Text != "" && KeyProduct.Text != string.Empty)
                && cmbLocalization.Items.Count > 0)
            {
                bazySQL.addSaveOptions(2, nameOfStudio.Text, KeyProduct.Text, cmbLocalization.SelectedItem.ToString(), BinaryDataImageStudio);
            }
            options = bazySQL.getOptionTable();
            addDataOptions();
        }
        /// <summary>
        /// Funkcje obsługi tabel
        /// </summary>
             //Bury - Wczytywanie danych z bazy danych do aplikacji
        private void UsersTable()
        {
            dataTable = bazySQL.getTables("users");
            usersTable.ItemsSource = dataTable.DefaultView;
        }
        private void AuthorsTable()
        {
            dataTableAuthors = bazySQL.getTables("authors");
            authorTable.ItemsSource = dataTableAuthors.DefaultView;
        }
        private void SongsTableRefresh()
        {
            dataTableSongs = bazySQL.getTables("songs");
            SongsTable.ItemsSource = dataTableSongs.DefaultView;
            listOfSongsAlbum.ItemsSource = dataTableSongs.DefaultView;
        }
        private void AlbumsTable()
        {
            dataTableAlbums = bazySQL.getTables("albums");
            albumList.ItemsSource = dataTableAlbums.DefaultView;
        }
        private void OrderTable()
        {
            dataTableOrders = bazySQL.getTables("orders");
            orderList.ItemsSource = dataTableOrders.DefaultView;
        }
        /// <summary>
        /// Funkcje obsługi Userów
        /// </summary>

        //Bury - wczytywanie danych użytkowników do poszczególnych rubryk z bazy danych do odpowiednich okien, sprawdza czy jest adminem oraz
        //sprawdza czy wybrany user jest zalogowany jeśli tak to wyłącza mu dostęd do zmiany danych
        private void usersTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView dataRow = (DataRowView)usersTable.SelectedItem;
            if (dataRow != null)
            {
                int ID = Convert.ToInt32(dataRow.Row[0]);
                saveUserID = Convert.ToInt32(dataRow.Row[0]);

                selectedUserUserName.Text = dataRow.Row[1].ToString();
                selectedUserName.Text = dataRow.Row[4].ToString();
                selectedUserSurname.Text = dataRow.Row[5].ToString();
                selectedUserAddress.Text = dataRow.Row[7].ToString();
                selectedUserCity.Text = dataRow.Row[8].ToString();
                selectedUserCash.Text = dataRow.Row[6].ToString();
                if ((int)dataRow.Row[9] == 0)
                {
                    selectedUserAdmin.IsChecked = false;
                }
                else
                {
                    selectedUserAdmin.IsChecked = true;
                }
                if ((int)dataRow.Row[10] != 1)
                {
                    if (ID == (int)userList[0])
                    {
                        selectedUserAdmin.IsEnabled = false;
                    }
                    else
                    {
                        selectedUserAdmin.IsEnabled = true;
                    }
                }
                else
                {
                    selectedUserAdmin.IsEnabled = false;
                }
                if (ID == (int)userList[0])
                {
                    selectedUserUserName.IsEnabled = selectedUserName.IsEnabled = selectedUserSurname.IsEnabled = selectedUserAddress.IsEnabled
                        = selectedUserCity.IsEnabled = selectedUserAdmin.IsEnabled = selectedUserCash.IsEnabled = false;
                }
                else
                {
                    selectedUserUserName.IsEnabled = selectedUserName.IsEnabled = selectedUserSurname.IsEnabled = selectedUserAddress.IsEnabled
                        = selectedUserCity.IsEnabled = selectedUserAdmin.IsEnabled = selectedUserCash.IsEnabled = true;
                }
            }
        }
        //Bury - Gdy zmienimy dane użytkownika po wciśnięciu przycisku Zapisz zostają one zapisane do bazy danych
        private void saveUser_Click(object sender, RoutedEventArgs e)
        {
            if (saveUserID != (int)userList[0])
            {
                if ((selectedUserUserName.Text != "" || selectedUserUserName.Text == null) && (selectedUserName.Text != "" || selectedUserName.Text == null) &&
                (selectedUserSurname.Text != "" || selectedUserSurname.Text == null) && (selectedUserAddress.Text != "" || selectedUserAddress.Text == null) &&
                (selectedUserCity.Text != "" || selectedUserCity.Text == null) && ((selectedUserCash.Text != "" || selectedUserCash.Text == null)))
                {
                    int admin = 0;
                    if (selectedUserAdmin.IsChecked == true)
                    {
                        admin = 1;
                    }

                    bazySQL.saveUser(saveUserID, selectedUserUserName.Text, selectedUserName.Text, selectedUserSurname.Text, selectedUserCash.Text, selectedUserAddress.Text,
                    selectedUserCity.Text, admin, (string)userList[1]);
                }
            }
            UsersTable();
        }
        //Bury - Funkcja która pozwala na usunięcia danego użytkownika z aplikacji oraz bazy danych
        private void deleteUser_Click(object sender, RoutedEventArgs e)
        {
            if (saveUserID != (int)userList[0])
            {
                if ((selectedUserUserName.Text != "" || selectedUserUserName.Text == null) && (selectedUserName.Text != "" || selectedUserName.Text == null) &&
                (selectedUserSurname.Text != "" || selectedUserSurname.Text == null) && (selectedUserAddress.Text != "" || selectedUserAddress.Text == null) &&
                (selectedUserCity.Text != "" || selectedUserCity.Text == null))
                {
                    int admin = 0;
                    if (selectedUserAdmin.IsChecked == true)
                    {
                        admin = 1;
                    }
                    bazySQL.deleteUserAuthorSongAlbum("Users", saveUserID, (string)userList[1]);
                    //bazySQL.deleteUser(saveUserID, (string)userList[1]);
                }
            }
            UsersTable();
        }
        private void selectedUserCash_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            //Regex regex = new Regex("^\\d{0,5}\\.\\d{1,2}$");
            //e.Handled = !regexCeny.IsMatch(e.Text);
        }
        //Bury - funkcja która pozawla na wybranie zdjęcia dla poszczegulnych autorów  
        private void selectPhoto_Click(object sender, RoutedEventArgs e)
        {
            BinaryDataImage = GetImageSelectPhoto("author");
        }
        /// <summary>
        /// Funkcje obsługi autorów
        /// </summary>

        //Bury - Funckja pozwalająca na zapisanie danych autora do bazy danych, pyta także czy jesteśmy pewnie wybrania zdjęcia które chcemy wgrać 
        //pozwala ona również na edytowanie danych istniejącego już autora gdybyśmy chceli coś przy nim zmienić
        private void saveAuthor_Click(object sender, RoutedEventArgs e)
        {
            string messageBoxText = Properties.Resources.sureAuthor;
            string messageBoxEditText = Properties.Resources.addOrEditAuthor;
            string caption = Properties.Resources.authorImage;
            string caption2 = Properties.Resources.addOrEditAuthor;
            bool checkIfAuthorIs = false;
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result, addOrEdit;
            if (selectedAuthorID != -1 && nameOfAuthor.Text != null && nameOfAuthor.Text != "")
            {
                checkIfAuthorIs = bazySQL.checkIfUserAuthorSongAlbumExist("author", selectedAuthorID, nameOfAuthor.Text);
            }
            result = System.Windows.MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);
            if (result == MessageBoxResult.Yes)
            {
                if (selectedFileName.Text != "" && selectedFileName.Text != null && BinaryDataImage != null)
                {
                    if (checkIfAuthorIs == true)
                    {
                        addOrEdit = MessageBox.Show(messageBoxEditText, caption2, button, icon, MessageBoxResult.Yes);
                        if (addOrEdit == MessageBoxResult.Yes)
                        {
                            bazySQL.addSaveAuthor(1, nameOfAuthor.Text, selectedFileName.Text, BinaryDataImage);
                        }
                        else
                        {
                            bazySQL.addSaveAuthor(2, nameOfAuthor.Text, selectedFileName.Text, BinaryDataImage, selectedAuthorID);
                        }
                    }
                    else
                    {
                        bazySQL.addSaveAuthor(1, nameOfAuthor.Text, selectedFileName.Text, BinaryDataImage);
                    }
                }
                else
                {
                    if (checkIfAuthorIs == true)
                    {
                        addOrEdit = MessageBox.Show(messageBoxEditText, caption2, button, icon, MessageBoxResult.Yes);
                        if (addOrEdit == MessageBoxResult.Yes)
                        {
                            bazySQL.addSaveAuthor(1, nameOfAuthor.Text);
                        }
                        else
                        {
                            bazySQL.addSaveAuthor(2, nameOfAuthor.Text, "null", null, selectedAuthorID);
                        }
                    }
                    else
                    {
                        bazySQL.addSaveAuthor(1, nameOfAuthor.Text);
                    }
                }
            }
            AuthorsTable();
            addAuthorsToCombobox();
        }
        //Bury - po kliknięciu na wybranego autora ładuje jego dane aby wyświetlić je w aplikacji
        private void authorTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedFilePath.Text = "";
            byte[] byteToImage = null;
            dataRowAuthor = (DataRowView)authorTable.SelectedItem;
            if (dataRowAuthor != null)
            {
                selectedIDsAuthorsTXT.Text = Properties.Resources.selectedID + ": " + dataRowAuthor.Row[0].ToString()
                    + " " + Properties.Resources.authorName + ": " + dataRowAuthor.Row[1].ToString();
                int ID = Convert.ToInt32(dataRowAuthor.Row[0]);
                selectedAuthorID = Convert.ToInt32(dataRowAuthor.Row[0]);
                nameOfAuthor.Text = dataRowAuthor.Row[1].ToString();
                selectedFileName.Text = dataRowAuthor.Row[2].ToString();
                if (dataRowAuthor.Row[3] != DBNull.Value)
                {
                    byteToImage = (byte[])dataRowAuthor.Row[3];
                }

                if (byteToImage != null && byteToImage.Length != 0)
                {
                    AuthorImage.Source = photoService(byteToImage);
                }

                AuthorsTable();
            }
        }
        //Bury - funkcja pozwalająca na usunięcie wybranego autora 
        private void deleteAuthor_Click(object sender, RoutedEventArgs e)
        {
            if (selectedAuthorID != -1)
            {
                bazySQL.deleteUserAuthorSongAlbum("Authors", selectedAuthorID);
            }
            AuthorsTable();
            nameOfAuthor.Text = selectedFileName.Text = selectedFilePath.Text = "";
            addAuthorsToCombobox();
        }
        //Bury - funckja dodające dane wszystkich autorów do combobox aby później można było ich użyć przy utworach
        public void addAuthorsToCombobox()
        {
            cmbAuthorSong.ItemsSource = null;
            selectedAuthorImage.Source = null;

            DataSet authorsTable = new DataSet();
            authorsTable = bazySQL.getAuthorsToCombobox();
            if (authorsTable != null)
            {
                cmbAuthorSong.ItemsSource = authorsTable.Tables["Authors"].DefaultView;
                cmbAuthorSong.DisplayMemberPath = "AuthorName";
                cmbAuthorSong.SelectedValuePath = "ID";
                cmbAuthorSong.SelectedIndex = -1;
                cmbAuthorSong.SelectedValue = 0;
            }
            else
            {
                cmbAuthorSong.ItemsSource = null;
            }
        }
        //Bury - obsługa comboboxa dla autorów
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int ID;
            if (cmbAuthorSong.Items.Count > 0)
            {
                try
                {
                    ID = (int)(cmbAuthorSong.SelectedValue);
                }
                catch (System.NullReferenceException ex)
                {
                    ID = -1;
                }
                if (ID != -1)
                {
                    byte[] byteToImage = bazySQL.getAuthorImage(ID);

                    if (byteToImage != null && byteToImage.Length != 0)
                    {
                        selectedAuthorImage.Source = photoService(byteToImage);
                    }
                }
            }
        }
        /// <summary>
        /// Funkcje obsługi utworów
        /// </summary>

        //Bury - gdy wybierzemy z listy dany utwór jego dane zostają pobrane i wyświetlone w aplikacji w oknie utwory, sprawdza także czy wybrany autor istnieje
        private void SongsTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedImageFilePathSong.Text = "";
            dataRowSong = (DataRowView)SongsTable.SelectedItem;
            if (dataRowSong != null)
            {
                selectedSongIDsTXT.Text = Properties.Resources.selectedID + ": " + dataRowSong.Row[0].ToString()
                    + Properties.Resources.songName + ": " + dataRowSong.Row[1].ToString();
                byte[] byteToImage = null;
                int ID = Convert.ToInt32(dataRowSong.Row[0]);
                selectedSongID = Convert.ToInt32(dataRowSong.Row[0]);
                nameOfSong1.Text = dataRowSong.Row[1].ToString();
                selectedImageFileNameSong.Text = dataRowSong.Row[2].ToString();
                if (dataRowSong.Row[3] != DBNull.Value)
                {
                    byteToImage = (byte[])dataRowSong.Row[3];
                }
                string AuthorsID = dataRowSong.Row[4].ToString();
                string PriceSong = dataRowSong.Row[5].ToString();
                string DiscountSong = dataRowSong.Row[6].ToString();
                priceOfSongTXT.Text = PriceSong;
                if (cmbAuthorSong.Items.Count >= int.Parse(AuthorsID))
                {
                    cmbAuthorSong.SelectedValue = int.Parse(AuthorsID);
                }
                else
                {
                    MessageBox.Show(Properties.Resources.authorNotExistDB);
                }
                cmbSongDiscount.SelectedItem = DiscountSong;

                if (byteToImage != null && byteToImage.Length != 0)
                {
                    SongImage.Source = photoService(byteToImage);
                }
                AuthorsTable();
            }
        }
        private void priceOfSongTXT_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            //e.Handled = !regexCeny.IsMatch(e.Text);
        }
        //Bury - po zapytaniu czy jesteśmy pewni zdjęcia zapisuje dane zdjęcie oraz dane które wpisaliśmy dla wybranego utworu 
        //po czym zapisuje w bazie danych nowy utwór lub edytuje dane juz zapisanego
        private void saveSong_Click(object sender, RoutedEventArgs e)
        {
            string messageBoxText = Properties.Resources.sureSong;
            string messageBoxEditText = Properties.Resources.AddEditSong;
            string caption = Properties.Resources.songImage;
            string caption2 = Properties.Resources.addOrEditSong;
            bool checkIfSongIs = false;
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result, addOrEdit;
            if (selectedSongID != -1 && nameOfSong1.Text != null && nameOfSong1.Text != "")
            {
                checkIfSongIs = bazySQL.checkIfUserAuthorSongAlbumExist("song", selectedSongID, nameOfSong1.Text);
            }
            result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);
            if (result == MessageBoxResult.Yes)
            {
                if (selectedImageFileNameSong.Text != "" && selectedImageFileNameSong.Text != null && BinaryDataImage != null &&
                    priceOfSongTXT.Text != null && priceOfSongTXT.Text != null)
                {
                    if (checkIfSongIs == true)
                    {
                        addOrEdit = MessageBox.Show(messageBoxEditText, caption2, button, icon, MessageBoxResult.Yes);
                        if (addOrEdit == MessageBoxResult.Yes)
                        {
                            bazySQL.addSaveSong(1, nameOfSong1.Text, cmbAuthorSong.SelectedValue.ToString(), priceOfSongTXT.Text,
                                cmbSongDiscount.SelectedItem.ToString(), -1, selectedImageFileNameSong.Text, BinaryDataImageSong);
                        }
                        else
                        {
                            bazySQL.addSaveSong(2, nameOfSong1.Text, cmbAuthorSong.SelectedValue.ToString(), priceOfSongTXT.Text
                                , cmbSongDiscount.SelectedItem.ToString(), selectedSongID, selectedImageFileNameSong.Text, BinaryDataImageSong);
                        }
                    }
                    else
                    {
                        bazySQL.addSaveSong(1, nameOfSong1.Text, cmbAuthorSong.SelectedValue.ToString(), priceOfSongTXT.Text,
                                cmbSongDiscount.SelectedItem.ToString(), -1, selectedImageFileNameSong.Text, BinaryDataImageSong);
                    }
                }
                else
                {
                    if (checkIfSongIs == true)
                    {
                        addOrEdit = MessageBox.Show(messageBoxEditText, caption2, button, icon, MessageBoxResult.Yes);
                        if (addOrEdit == MessageBoxResult.Yes)
                        {
                            bazySQL.addSaveSong(1, nameOfSong1.Text, cmbAuthorSong.SelectedValue.ToString(), priceOfSongTXT.Text,
                                cmbSongDiscount.SelectedItem.ToString(), -1, "null", null);
                        }
                        else
                        {
                            bazySQL.addSaveSong(2, nameOfSong1.Text, cmbAuthorSong.SelectedValue.ToString(), priceOfSongTXT.Text
                                , cmbSongDiscount.SelectedItem.ToString(), selectedSongID, "null", null);
                        }
                    }
                    else
                    {
                        bazySQL.addSaveSong(1, nameOfSong1.Text, cmbAuthorSong.SelectedValue.ToString(), priceOfSongTXT.Text,
                                cmbSongDiscount.SelectedItem.ToString(), -1, "null", null);
                    }
                }
                SongsTableRefresh();
            }
        }
        //Bury - oprogramowanie przycisku Wyłącz Program
        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        //Hubert - Przycisk odpowiedzialny za zmiane motywu
        //Hubert - pierwsza funkcjonalność zmiana motywu (jasny/ciemny)
        //Po naciśnięciu tego przycisku motywy które są ustawione w każdym pliku xaml.cs zostają zmienione na motyw 0 (jasny) lub 1 (ciemny)
        //jest to zrobione przez przypisanie do każdego elementu koloru aby po wciśnięciu przyciski zostały one zmnienione na inny kolor

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            changeTheme();
        }
        //Bury - oprogramowanie przycisku regulamin: po wciśnięciu otwiera się nowe okno zawierające regulamin, motyw ustawiony automatycznie na podstawie ustawien;
        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            Regulamin regulamin = new Regulamin(theme);
            regulamin.Show();
        }
        //Bury - wyświetlanie listy utworów w oknie albumów aby wybrać który utwór chcemy dodać do danego albumu
        //oraz wypełniają okienka gdzie pokazywane są id wybranych utworów
        private void listOfSongsAlbum_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            songList = listOfSongsAlbum.SelectedItems;
            selectedAlbumSongsIDTXT.Text = null;
            if (songList.Count > 0)
            {
                foreach (DataRowView drv in songList)
                {
                    selectedAlbumSongsIDTXT.Text = selectedAlbumSongsIDTXT.Text + drv.Row[0].ToString() + " ,";
                }
            }
            else selectedAlbumSongsIDTXT.Text = null;
            selectedAlbumSongsIDTXT.Text.TrimEnd(' ', ',');
        }
        //Bury - Funkcja pozwalająca na usunięcie wybranego utworu
        private void deleteSong_Click(object sender, RoutedEventArgs e)
        {
            if (selectedSongID != -1)
            {
                bazySQL.deleteUserAuthorSongAlbum("Songs", selectedSongID);
            }
            SongsTableRefresh();
        }
        //Bury - Funkcja pozwalająca na wybranie zdjęcia dla danego utworu
        private void selectPhotoSong_Click(object sender, RoutedEventArgs e)
        {
            BinaryDataImageSong = GetImageSelectPhoto("song");
        }
        //Bury - funkcja pozwalająca na kupienie wybranego utworu, sprawdza też czy masz wystarczająco pieniędzy na koncie aby kupic wybrany utwór
        //dodaje zamówienie do listy zamówień oraz do bazy danych
        //Hubert - Obliczanie rabatu wzorem i podanie ostatecznej ceny
        private void buyThisSong_Click(object sender, RoutedEventArgs e)
        {
            if (dataRowSong != null)
            {
                string price = dataRowSong.Row[5].ToString();
                string discount = dataRowSong.Row[6].ToString();
                decimal priceOfSong = convertPrice(price);
                int discountOfSong = convertDiscount(discount);
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
                    int SongID = (int)dataRowSong.Row[0];
                    bazySQL.addSaveOrder(1, (int)userList[0], SongID, "Songs", "Something", finishPrice.ToString().Replace(',', '.'), String.Format(userList[7] + " " + userList[8]), -1);
                    bazySQL.updateUserCash((int)userList[0], userList[1].ToString(), difference.ToString());
                    MySqlDataReader userTemp = bazySQL.infoAboutUserByID((int)userList[0]);
                    updateUserList(userTemp);
                    UsersTable();
                    OrderTable();
                }
                else
                {
                    youCantBuyIt();
                }
            }
        }
        // Bury - oprogramowanie przycisku wyloguj; po wciśnięciu następuje wylogowanie użytkownika/administratora oraz przeniesienie do okna logowania
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            userList = null;
            options = null;
            dataRowAuthor = dataRowSong = dataRowAlbum = dataRowOrder = null;
            dataTable = dataTableAuthors = dataTableSongs = dataTableAlbums = dataTableSongsOfAlbum = dataTableOrders = null;
            BinaryDataImage = BinaryDataImageSong = BinaryDataImageAlbum = BinaryDataImageStudio = null;
            NavigationService.Navigate(new Logowanie(bazySQL));
        }

        /// <summary>
        /// Funkcje obsługi albumów
        /// </summary>

        //Bury - funkcja która zapisuje album który stworzyłeś lub edytuje już stworzony
        private void saveAlbum_Click(object sender, RoutedEventArgs e)
        {
            string messageBoxText = Properties.Resources.sureAlbum;
            string messageBoxEditText = Properties.Resources.addOrEditAlbum;
            string caption = Properties.Resources.albumImage;
            string caption2 = Properties.Resources.addOrEditAlbum;
            bool checkIfAlbumIs = false;
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result, addOrEdit;
            string Songs = "";
            if (songList != null)
            {
                if (songList.Count > 0)
                {
                    foreach (DataRowView element in songList)
                    {
                        Songs = Songs + element.Row[0].ToString() + ", ";
                    }
                    Songs = Songs.TrimEnd(',', ' ');
                }
            }
            if (selectedAlbumID != -1 && nameOfAlbum.Text != null && nameOfAlbum.Text != "")
            {
                checkIfAlbumIs = bazySQL.checkIfUserAuthorSongAlbumExist("album", selectedAlbumID, nameOfAlbum.Text);
            }
            result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);
            if (result == MessageBoxResult.Yes)
            {
                if (nameOfAlbum.Text != "" && nameOfAlbum.Text != null && selectedImageFileNameAlbum.Text != null && BinaryDataImageAlbum != null && songList != null)
                {
                    if (checkIfAlbumIs == true)
                    {
                        addOrEdit = MessageBox.Show(messageBoxEditText, caption2, button, icon, MessageBoxResult.Yes);
                        if (addOrEdit == MessageBoxResult.Yes)
                        {
                            bazySQL.addSaveAlbum(1, nameOfAlbum.Text, Songs, priceOfAlbumTXT.Text, cmbAlbumDiscount.SelectedItem.ToString(), -1,
                                selectedImageFileNameAlbum.Text, BinaryDataImageAlbum);
                        }
                        else
                        {
                            bazySQL.addSaveAlbum(2, nameOfAlbum.Text, Songs, priceOfAlbumTXT.Text, cmbAlbumDiscount.SelectedItem.ToString(),
                                selectedAlbumID, selectedImageFileNameAlbum.Text, BinaryDataImageAlbum);
                        }
                    }
                    else
                    {
                        bazySQL.addSaveAlbum(1, nameOfAlbum.Text, Songs, priceOfAlbumTXT.Text, cmbAlbumDiscount.SelectedItem.ToString(), -1,
                                selectedImageFileNameAlbum.Text, BinaryDataImageAlbum);
                    }
                }
                else if (nameOfAlbum.Text != "" && nameOfAlbum.Text != null && songList != null)
                {
                    if (checkIfAlbumIs == true)
                    {
                        addOrEdit = MessageBox.Show(messageBoxEditText, caption2, button, icon, MessageBoxResult.Yes);
                        if (addOrEdit == MessageBoxResult.Yes)
                        {
                            bazySQL.addSaveAlbum(1, nameOfAlbum.Text, Songs, priceOfAlbumTXT.Text, cmbAlbumDiscount.SelectedItem.ToString());
                        }
                        else
                        {
                            bazySQL.addSaveAlbum(2, nameOfAlbum.Text, Songs, priceOfAlbumTXT.Text, cmbAlbumDiscount.SelectedItem.ToString(),
                                selectedAlbumID, "null", null);
                        }
                    }
                    else
                    {
                        bazySQL.addSaveAlbum(1, nameOfAlbum.Text, Songs, priceOfAlbumTXT.Text, cmbAlbumDiscount.SelectedItem.ToString());
                    }
                }
                AlbumsTable();
            }
        }
        //Bury - wyświtla dane aktualnie wybranego utworu oraz jego obrazy
        private void albumList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedImageFilePathSong.Text = "";
            dataRowAlbum = (DataRowView)albumList.SelectedItem;
            if (dataRowAlbum != null)
            {
                selectedAlbumIDTXT.Text = Properties.Resources.selectedID + ": " + dataRowAlbum.Row[0].ToString() + " "
                    + Properties.Resources.nameOfAlbum + ": " + dataRowAlbum.Row[1].ToString();
                byte[] byteToImage = null;
                int ID = Convert.ToInt32(dataRowAlbum.Row[0]);
                selectedAlbumID = Convert.ToInt32(dataRowAlbum.Row[0]);
                nameOfAlbum.Text = dataRowAlbum.Row[1].ToString();
                selectedImageFileNameAlbum.Text = dataRowAlbum.Row[2].ToString();
                if (dataRowAlbum.Row[3] != DBNull.Value)
                {
                    byteToImage = (byte[])dataRowAlbum.Row[3];
                }
                string SongsString = dataRowAlbum.Row[4].ToString();

                dataTableSongsOfAlbum = bazySQL.getSongsOfAlbum(dataRowAlbum.Row[4].ToString());

                string[] songList = dataRowAlbum.Row[4].ToString().Split(',');
                //albumListOfSongs.Items.Clear();
                albumListOfSongs.ItemsSource = null;
                albumListOfSongs.ItemsSource = dataTableSongsOfAlbum.DefaultView;
                priceOfAlbumTXT.Text = dataRowAlbum.Row[5].ToString();

                cmbAlbumDiscount.SelectedItem = dataRowAlbum.Row[6].ToString();

                if (byteToImage != null && byteToImage.Length != 0)
                {
                    AlbumImage.Source = photoService(byteToImage);
                }
            }
        }
        //Bury - Funkcja które pozwala na wybranie obrazu dla wybranego albumu
        private void selectAlbumPhotoImage_Click(object sender, RoutedEventArgs e)
        {
            BinaryDataImageAlbum = GetImageSelectPhoto("album");
        }
        private void deleteAlbum_Click(object sender, RoutedEventArgs e)
        {
            if (selectedAlbumID != -1)
            {
                bazySQL.deleteUserAuthorSongAlbum("Albums", selectedAlbumID);
            }
            albumListOfSongs.ItemsSource = null;
            selectedAlbumIDTXT.Text = string.Empty;
            selectedAlbumSongsIDTXT.Text = string.Empty;
            AlbumsTable();
        }

        /// <summary>
        ///  Funkcje obsługi zamowień
        /// </summary>

        //Bury - funckja która wyświetla dane wybranego zamówienia w oknie zamówienia 
        private void orderList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dataRowOrder = (DataRowView)orderList.SelectedItem;
            if (dataRowOrder != null)
            {
                selectedOrderIDsTXT.Text = Properties.Resources.selectedID + ": " + dataRowOrder.Row[0].ToString();
                byte[] byteToImage = null;
                int ID = Convert.ToInt32(dataRowOrder.Row[0]);
                selectedOrderID = Convert.ToInt32(dataRowOrder.Row[0]);
                orderUserIDLabelTXT.Text = dataRowOrder.Row[1].ToString();
                MySqlDataReader getUserInfo = bazySQL.infoAboutUserByID(int.Parse(dataRowOrder[1].ToString()));
                MySqlDataReader getElementInfo = bazySQL.getOrderedProductinfo(dataRowOrder[3].ToString(), int.Parse(dataRowOrder[2].ToString()));
                if (getUserInfo != null)
                {
                    orderUserIDLabelTXT.Text = getUserInfo.GetValue(1).ToString();
                    orderUserNameSurnameLabelTXT.Text = getUserInfo.GetValue(4).ToString() + " " + getUserInfo.GetValue(5).ToString();
                    orderUserAddressTXT.Text = getUserInfo.GetValue(7).ToString() + " " + getUserInfo.GetValue(8).ToString();
                }

                if (getElementInfo != null)
                {
                    if (getElementInfo.GetValue(3) != DBNull.Value && (byte[])getElementInfo.GetValue(3) != null)
                    {
                        orderedProductCover.Source = photoService((byte[])getElementInfo.GetValue(3));
                    }
                    if (dataRowOrder[3].ToString() == "Songs")
                    {
                        MySqlDataReader authorSong = bazySQL.getInfoAboutAuthor(int.Parse(getElementInfo.GetValue(4).ToString()));
                        if (authorSong != null)
                        {
                            orderProductTXT.Text = authorSong.GetValue(1).ToString() + " - " + getElementInfo.GetValue(1).ToString();
                        }
                        else
                        {
                            orderProductTXT.Text = getElementInfo.GetValue(1).ToString();
                        }
                    }
                }
                orderCostTXT.Text = dataRowOrder[5].ToString();
            }
        }
        //Bury - funkcja pozwalająca na usunięcie wybranego zamówienia
        private void deleteOrder_Click(object sender, RoutedEventArgs e)
        {
            if (selectedOrderID != -1)
            {
                bazySQL.deleteUserAuthorSongAlbum("Orders", selectedOrderID);
            }
            OrderTable();
        }
        //Bury - FUnkcja pozwalająca na kupienie wybranego albumu, sprawdzenie czy stać cie na dany utwór
        //Hubet - Obliczenie rabatu z wzoru oraz ustawienie go jako cene ostateczną
        private void buyThisAlbum_Click(object sender, RoutedEventArgs e)
        {
            if (dataRowAlbum != null)
            {
                string price = dataRowAlbum.Row[5].ToString();
                string discount = dataRowAlbum.Row[6].ToString();
                decimal priceOfAlbum = convertPrice(price);
                int discountOfAlbum = convertDiscount(discount);
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
                    int AlbumID = (int)dataRowAlbum.Row[0];
                    bazySQL.addSaveOrder(1, (int)userList[0], AlbumID, "Albums", "Something", finishPrice.ToString().Replace(',', '.'), String.Format(userList[7] + " " + userList[8]), -1);
                    bazySQL.updateUserCash((int)userList[0], userList[1].ToString(), difference.ToString());
                    UsersTable();
                    MySqlDataReader userTemp = bazySQL.infoAboutUserByID((int)userList[0]);
                    updateUserList(userTemp);
                    OrderTable();
                }
                else
                {
                    youCantBuyIt();
                }
            }
        }
    }
}