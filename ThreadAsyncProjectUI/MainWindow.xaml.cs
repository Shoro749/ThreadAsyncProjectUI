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
using static System.Net.WebRequestMethods;

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
            await SearchWord("C:\\Users\\Роман\\Downloads\\GeneralFolder");
        }

        private async Task SearchWord(string path)
        {
            try
            {
                foreach (string file in Directory.EnumerateFiles(path))
                {
                    textBlock.Text += file;
                    //await SearchingInFile(file);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async Task SearchingInFile(string path)
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string text = await reader.ReadToEndAsync();
                foreach (var letter in text)
                {
                    textBlock.Text += letter;
                }
            }
        }

        //private async Task<string> ReaderAsync()
        //{
        //    using (StreamReader reader = new StreamReader(path.Text))
        //    {
        //        return await reader.ReadToEndAsync();
        //    }
        //}
    }
}
