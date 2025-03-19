using System.Data;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Threading;
using VRCLogManager.Pages;

namespace VRCLogManager;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private BD_MainWindow bd = new BD_MainWindow();
    private DateTime lastTime;
    private DispatcherTimer timer = new DispatcherTimer(DispatcherPriority.Normal);

    public MainWindow()
    {
        InitializeComponent();
        Loaded += wfpLoaded;
    }

    private void wfpLoaded(object sender, RoutedEventArgs e)
    {
        DB.DBCheck();
        DataContext = bd;
        Frame1.Navigate(PageManager.main);
        lastTime = DateTime.Now;
        timer.Interval = new TimeSpan(1000);
        timer.Tick += new EventHandler(TimerRun);
    }

    private void TimerRun(object? sender, EventArgs e)
    {
        DateTime nowTime = DateTime.Now;
        if (lastTime.Hour < nowTime.Hour)   //毎時間実行
        {
            LogCopy();
        }

        if (lastTime.Hour == 5 && nowTime.Hour == 6)    //朝6時のみ実行
        {

        }
    }

    private void LogCopy()
    {
        //VRChatフォルダーを見てファイル一覧を取得
        string[] files =  Directory.GetFiles(MST.locallow + "\\VRChat\\VRChat\\");

        //コピー先存在チェック
        string dir = MST.vlmLocal + "\\LogData\\Done\\";
        if (Directory.Exists(dir) == false)
        {
            //無かったら作る
            Directory.CreateDirectory(dir);
        }

        //ログファイルならコピー
        foreach (string file in files)
        {
            if (CheckLogFile(file))
            {
                try
                {
                    //移動できるなら移動する
                    File.Move(file, MST.vlmLocal + "\\LogData\\" + Path.GetFileName(file));
                }
                catch
                {
                    //だめならまだVRChatが掴んでるので一旦放置
                }
            }
        }

    }

    private bool CheckLogFile(string file)      //ログファイルかどうかを判定
    {
        if (Path.GetExtension(file) != ".txt")
        {
            return false;
        }
        else if (Path.GetFileNameWithoutExtension(file).Length < 11)
        {
            return false;
        }
        else if (Path.GetFileNameWithoutExtension(file).Substring(0, 10) == "output_log")
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void LogImport()
    {

    }
}