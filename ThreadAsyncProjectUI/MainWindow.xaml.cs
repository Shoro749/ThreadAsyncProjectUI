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
        }

        private async void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (char.ToLower(e.Key.ToString()[0]).ToString() == "s")
            {
                start = !start;
                if (start)
                {
                    GenerateNumbers();
                }
            }
        }

        private async Task GenerateNumbers()
        {
            while (true)
            {
                Random rnd = new Random();
                Dispatcher.Invoke(() =>
                {
                    textBlock.Text = rnd.Next().ToString();
                });
                await Task.Delay(1000);

                if (!start) { return; }
            }
        }
    }
}
