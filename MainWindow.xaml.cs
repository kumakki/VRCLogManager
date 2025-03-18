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
using System.Windows.Shapes;
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
            
        }

        if (lastTime.Hour == 5 && nowTime.Hour == 6)    //朝6時のみ実行
        {
            
        }
    }
}