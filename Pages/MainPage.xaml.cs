using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.InteropServices;
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

            //アカウントリストの初期化
            bd.AccountList = DB.GetAccountDatas();
            bd.AccountList.Insert(0, new AccountData() { AccountID = "", AccountName = "全アカウント" });
            bd.SelectedIndex_Account = 0;

            //その他項目初期化
            dataGrid.Columns[0].Visibility = Visibility.Collapsed;
            dataGrid.Columns[2].Visibility = Visibility.Collapsed;
            dataGrid.Columns[5].Visibility = Visibility.Collapsed;
            bd.PlayerExtraMatch = true;
            bd.WorldExtraMatch = true;
            bd.StartDate = DateTime.Now;
            bd.EndDate = DateTime.Now;

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
            if (bd.ShowID == true)
            {
            dataGrid.Columns[0].Visibility = Visibility.Visible;
            dataGrid.Columns[2].Visibility = Visibility.Visible;
            dataGrid.Columns[5].Visibility = Visibility.Visible;
            }
            else
            {
            dataGrid.Columns[0].Visibility = Visibility.Collapsed;
            dataGrid.Columns[2].Visibility = Visibility.Collapsed;
            dataGrid.Columns[5].Visibility = Visibility.Collapsed;
            }
        }

        private void OnClick_Search(object sender, RoutedEventArgs e)
        {
            //検索条件をセットして検索する
            SearchData search = new SearchData();

            //アカウント
            if (bd.SelectedItem_Account == null)
            {
                //ないとは思うがnullなら全て扱いとする
                search.AccountID = null;
                search.AccountName = "全て";
            }
            else if (bd.SelectedItem_Account.AccountID == "全て")
            {
                search.AccountID = null;
                search.AccountName = "全て";
            }
            else
            {
                search.AccountID = bd.SelectedItem_Account.AccountID;
                search.AccountName = bd.SelectedItem_Account.AccountName;
            }

            //開始日付
            if (bd.StartDate == null)
            {
                search.LogDateStart = null;
            }
            else
            {
                search.LogDateStart = int.Parse(bd.StartDate.HasValue ? bd.StartDate.Value.ToString("yyyyMMdd") : "00000000");  // null の場合の代替値
            }

            //終了日付
            if (bd.EndDate == null)
            {
                search.LogDateEnd = null;
            }
            else
            {
                search.LogDateEnd = int.Parse(bd.EndDate.HasValue ? bd.EndDate.Value.ToString("yyyyMMdd") : "00000000");  // null の場合の代替値
            }

            //プレイヤー
            if (bd.PlayerID != null && bd.PlayerID != "")
            {
                //ID入ってるなら名前と一緒に
                search.PlayerID = bd.PlayerID;
                search.PlayerName = bd.PlayerName;
            }
            else
            {
                if (bd.PlayerName == null || bd.PlayerName == "")
                {
                    //IDなくて名前が入ってないなら全件
                    search.PlayerID = null;
                    search.PlayerName = null;
                }
                else
                {
                    //IDなくて名前が入ってれば名前だけ
                    search.PlayerID = null;
                    search.PlayerName = bd.PlayerName;
                }
            }

            //ワールド
            if (bd.WorldID != null && bd.WorldID != "")
            {
                //ID入ってるなら名前と一緒に
                search.WorldID = bd.WorldID;
                search.WorldName = bd.WorldName;
            }
            else
            {
                if (bd.WorldName == null || bd.WorldName == "")
                {
                    //IDなくて名前が入ってないなら全件
                    search.WorldID = null;
                    search.WorldName = null;
                }
                else
                {
                    //IDなくて名前が入ってれば名前だけ
                    search.WorldID = null;
                    search.WorldName = bd.WorldName;
                }
            }

            //完全一致か部分一致か
            search.PlayerExtraMatch = bd.PlayerExtraMatch;
            search.WorldExtraMatch = bd.WorldExtraMatch;

            //検索条件渡して検索
            bd.JoinLeaveList = DB.SearchJoinLeave(search);

        }
    }
}