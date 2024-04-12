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
    public partial class MainWindow : Window
    {
        private bool start;
        public MainWindow()
        {
            InitializeComponent();
            start = false;
            Thread thread = new Thread(Thread1);
            thread.Start();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (char.ToLower(e.Key.ToString()[0]).ToString() == "s")
            {
                start = !start;
            }
        }

        private void Thread1()
        {
            Random rnd = new Random();
            while (true)
            {
                Thread.Sleep(1000);
                if (start == false) continue;
                Dispatcher.Invoke(() =>
                {
                    textBlock.Text = rnd.Next().ToString();
                });
            }
        }
    }
}
