using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace VRCLogManager.Pages
{
    public partial class MainPage : System.Windows.Controls.Page
    {
        private BD_MainPage bd = new BD_MainPage();

        private string? bufPlayerName;
        private string? bufWorldName;

        public MainPage()
        {
            InitializeComponent();
            Loaded += wfpLoaded;
        }

        private void wfpLoaded(object sender, RoutedEventArgs e)
        {
            //バインドデータセット
            DataContext = bd;

            //プレイヤーリストの初期化
            bd.PlayerList = DB.GetPlayerDatas();

            //ワールドリストの初期化
            bd.WorldList = DB.GetWorldDatas();
        }

        private void OnClick_DB(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(PageManager.db);
        }

        private void cmbPlayer_SelectionChanged(object sender, RoutedEventArgs e)
        {
            PlayerData? buf = bd.SelectedItem_Player;
            if (buf != null)
            {
                bufPlayerName = buf.PlayerName;
                bd.PlayerID = buf.PlayerID;
            }
        }

        private void cmbPlayer_KeyDown(object sender, RoutedEventArgs e)
        {
            if (bufPlayerName != bd.PlayerName)
            {
                bd.PlayerID = "";
            }
        }

        private void cmbWorld_SelectionChanged(object sender, RoutedEventArgs e)
        {
            WorldData? buf = bd.SelectedItem_World;
            if (buf != null)
            {
                bufWorldName = buf.WorldName;
                bd.WorldID = buf.WorldID;
            }
        }

        private void cmbWorld_KeyDown(object sender, RoutedEventArgs e)
        {
            if (bufWorldName != bd.WorldName)
            {
                bd.WorldID = "";
            }
        }

        private void datePicker1_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}