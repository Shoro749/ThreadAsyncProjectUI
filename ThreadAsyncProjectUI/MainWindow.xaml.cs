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

namespace ThreadAsyncProjectUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int number;
        private int milliseconds;
        private bool stop;
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            //Thread thread = new Thread(ShowMessage);
            //thread.Start();
            stop = !stop;
        }

        private async Task StartTimer()
        {
            while (true)
            {
                await Task.Delay(1000);
                if (stop) continue;
                number++;
                textBlock.Text = number.ToString();
            }
        }

        private async Task StartTimer2()
        {
            while (true)
            {
                await Task.Delay(10);
                if (stop) continue;
                milliseconds++;
                textBlock2.Text = milliseconds.ToString();
                if (milliseconds == 100) milliseconds = 0;
            }
        }

        async Task ShowMessageAsync()
        {
            await Task.Delay(1000);
            button.Content = "Thread works";
        }

        private async void startButton_Click(object sender, RoutedEventArgs e)
        {
            StartTimer();
            await StartTimer2();
        }

        //void ShowMessage()
        //{
        //    Thread.Sleep(5000);
        //    Dispatcher.Invoke(() =>
        //    {
        //        button.Content = "Thread works";
        //    });
        //}
    }
}
