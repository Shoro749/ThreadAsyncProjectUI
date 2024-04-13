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
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            Horse1();
            await Horse2();
        }

        private async Task Horse1()
        {
            while (horse1.Value < 100)
            {
                Random rnd = new Random();
                int temp = rnd.Next(0, 20);
                if (100 - horse1.Value < temp)
                {
                    horse1.Value = 100;
                    if (horse2.Value != 100)
                    {
                        lb.Content = "Winner: Horse 1!";
                    }
                    break;
                }
                else { horse1.Value += temp; }

                await Task.Delay(1000);
            }
        }

        private async Task Horse2()
        {
            while (horse2.Value < 100)
            {
                Random rnd = new Random();
                int temp = rnd.Next(0, 20);
                if (100 - horse2.Value < temp)
                {
                    horse2.Value = 100;
                    if (horse1.Value != 100)
                    {
                        lb.Content = "Winner: Horse 2!";
                    }
                    break;
                }
                else { horse2.Value += temp; }

                await Task.Delay(1000);
            }
        }
    }
}
