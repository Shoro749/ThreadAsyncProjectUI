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
        private object locker = new object();
        private int[] arr = new int[8];
        public MainWindow()
        {
            InitializeComponent();
            Random rnd = new Random();
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = rnd.Next(0, 100);
            }

            foreach (int i in arr)
            {
                Arr.Text += i.ToString() + " ";
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (tb.Text.All(char.IsDigit))
            {
                Sorter();
                await Searcher();
            }
        }

        private async Task Sorter()
        {
            lock(locker)
            {
                Array.Sort(arr);
                result.Text += "Sorted array: ";
                foreach (int i in arr)
                {
                    result.Text += i.ToString() + " ";
                }
            }
        }

        private async Task Searcher()
        {
            lock (locker)
            {
                for (int i = 0; i < arr.Length; i++)
                {
                    if (arr[i] == Convert.ToInt32(tb.Text))
                    {
                        result.Text += "\nIndex: " + i.ToString();
                        break;
                    }
                }
            }
        }
    }
}
