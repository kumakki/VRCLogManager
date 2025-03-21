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
        timer.Start();
        LogImport();
    }

    private void TimerRun(object? sender, EventArgs e)
    {
        DateTime nowTime = DateTime.Now;
        if (lastTime.Hour < nowTime.Hour)   //毎時間実行
        {
            //ログファイルコピー
            LogCopy();
        }

        if (lastTime.Hour == 5 && nowTime.Hour == 6)    //朝6時のみ実行
        {
            //コピーしたログファイルから取込
            LogImport();
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
        //ログフォルダー内取得
        string[] logFiles = Directory.GetFiles(MST.vlmLocal + "\\LogData");

        //ログファイル１つ１つを処理
        foreach (string logFile in logFiles)
        {
            if (CheckLogFile(logFile))
            {
                //ログファイルで間違いないなら読み込んで処理
                string[] lines = File.ReadAllLines(logFile);

                //現在のログインユーザーとワールドを保持
                string myUserID = "";
                string myUserName = "";
                string nowWorldID = "";
                string nowWorldName = "";
                bool flgWorldID = false;
                bool flgWorldName = false;

                foreach (string line in lines)
                {
                    int index = 0;

                    //ログインユーザーチェック
                    index = line.IndexOf("User Authenticated");
                    if (index != -1)
                    {
                        myUserID = line.Substring(line.Length - 41, 40);
                        myUserName = line.Substring(index + 20, line.Length - 43 - (index + 20));

                        DB.SetData(myUserID, myUserName, 0);
                    }

                    //現在のワールドチェック
                    //IDと名前それぞれでフラグを持っといて、2つのデータが揃ったらDBへ渡す
                    index = line.IndexOf("Unpacking World");
                    if (index != -1)
                    {
                        nowWorldID = line.Substring(index + 17, 40);
                        flgWorldID = true;
                        if (flgWorldID && flgWorldName)
                        {
                            DB.SetData(nowWorldID, nowWorldName, 2);
                            flgWorldID = false;
                            flgWorldName = false;
                        }
                    }
                    
                    index = line.IndexOf("Joining or Creating Room");
                    if (index != -1)
                    {
                        nowWorldName = line.Substring(index + 26, line.Length - (index + 26));
                        flgWorldName = true;
                        if (flgWorldID && flgWorldName)
                        {
                            DB.SetData(nowWorldID, nowWorldName, 2);
                            flgWorldID = false;
                            flgWorldName = false;
                        }
                    }

                    //プレイヤーのJoinチェック
                    index = line.IndexOf("[Behaviour] OnPlayerJoined ");
                    if (index != -1)
                    {
                        string bufID = line.Substring(line.Length - 41, 40);
                        string bufName = line.Substring(index + 27, line.Length - 43 - (index + 27));
                        
                        int logDate = Int32.Parse(line.Substring(0, 4) + line.Substring(5, 2) + line.Substring(8, 2));
                        int logTime = Int32.Parse(line.Substring(11, 2) + line.Substring(14, 2) + line.Substring(17, 2));
                        

                        DB.SetData(bufID, bufName, 1);
                        DB.SetJoinLeave(bufID, 0, nowWorldID);
                    }

                    //プレイヤーのLeaveチェック
                    index = line.IndexOf("[Behaviour] OnPlayerLeft ");
                    if (index != -1)
                    {
                        string bufID = line.Substring(line.Length - 41, 40);
                        string bufName = line.Substring(index + 25, line.Length - 43 - (index + 25));
                        DB.SetJoinLeave(bufID, 1, nowWorldID);
                    }

                }
            }
        }
    }

    
}