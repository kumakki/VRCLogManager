using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace VRCLogManager.Pages
{
    public partial class MainPage : Page
    {
        public BD_MainPage bd = new BD_MainPage();

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

            //バインドのイベントを登録
            bd.PropertyChanged += BD_PropertyChanged;

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
                bd.PlayerName = buf.PlayerName;
            }
        }

        private void BD_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "PlayerName")
            {
                if (bufPlayerName != bd.PlayerName)
                {
                    bd.PlayerID = "";
                }
                else
                {
                    if (bd.SelectedItem_Player != null)
                    {
                        bd.PlayerID = bd.SelectedItem_Player.PlayerID;
                    }
                }
            }
            else if (e.PropertyName == "WorldName")
            {
                if (bufWorldName != bd.WorldName)
                {
                    bd.WorldID = "";
                }
                else
                {
                    if (bd.SelectedItem_World != null)
                    {
                        bd.WorldID = bd.SelectedItem_World.WorldID;
                    }
                }
            }
        }

        private void cmbWorld_SelectionChanged(object sender, RoutedEventArgs e)
        {
            WorldData? buf = bd.SelectedItem_World;
            if (buf != null)
            {
                bufWorldName = buf.WorldName;
                bd.WorldName = buf.WorldName;
            }
        }

        private void OnClick_ShowID(object sender, RoutedEventArgs e)
        {

        }

        private void datePicker1_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}