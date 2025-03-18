using System.Collections.ObjectModel;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Media3D;
using System.Windows.Media;

namespace VRCLogManager.Pages
{
    public partial class DBMaintenance : Page
    {
        BD_DBMaintenance bd = new BD_DBMaintenance();

        public DBMaintenance()
        {
            InitializeComponent();
            Loaded += wfpLoaded;
        }

        private void wfpLoaded(object sender, RoutedEventArgs e)
        {
            LoadTables();
            DataContext = bd;
            bd.SelectedIndex = -1;
            bd.LoadEnable = false;
        }

        private void OnClick_Back(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(PageManager.main);
        }

        private void LoadTables()
        {
            bd.TableLists = DB.GetTables();
        }

        [Obsolete]
        private void OnClick_Load(object sender, RoutedEventArgs e)
        {
            //格納するリスト
            ObservableCollection<DBDataRecord> DBDataRecords = new ObservableCollection<DBDataRecord>();
            string targetTable = bd.SelectedItem;
            //まずはテーブル情報を取得
            List<string> tableColumns = DB.GetTableColumns(targetTable);

            //カラム数カウント
            int columnCount = 0;
            foreach (var column in tableColumns)
            {
                if (column != "")
                {
                    columnCount += 1;
                }
                else
                {
                    break;
                }
            }

            //データ本体を取得
            ObservableCollection<DBDataRecord> records = DB.GetAllData(targetTable, columnCount);

            //先頭のカラム名を格納
            DBDataRecord bufColumn = new DBDataRecord();
            bufColumn.Column1 = tableColumns[0];
            bufColumn.Column2 = tableColumns[1];
            bufColumn.Column3 = tableColumns[2];
            bufColumn.Column4 = tableColumns[3];
            bufColumn.Column5 = tableColumns[4];
            bufColumn.Column6 = tableColumns[5];
            bufColumn.Column7 = tableColumns[6];
            bufColumn.Column8 = tableColumns[7];
            bufColumn.Column9 = tableColumns[8];
            bufColumn.Column10 = tableColumns[9];
            bd.DBDataColumn = bufColumn;

            //本体バインドデータのセット
            bd.DBDataRecords = records;
            for (int i = 0; i < 10; i++)
            {
                gridViewDB.Columns[i].Width = DB.GetColumnWidth(tableColumns[i]);
            }

        }

        [Obsolete]
        private double MeasureTextWidth(string text, double fontSize)
        {
            FormattedText formattedText = new FormattedText(
                text,
                System.Globalization.CultureInfo.CurrentCulture,
                FlowDirection.LeftToRight,
                new Typeface("Segoe UI"),
                fontSize,
                Brushes.Black,
                new NumberSubstitution(),
                TextFormattingMode.Display
            );

            return formattedText.Width;
        }

        private void List_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (bd.SelectedIndex != -1)
            {
                bd.LoadEnable = true;
            }
            else
            {
                bd.LoadEnable = false;
            }
        }
    }
}