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
using System.Xml.Linq;
using static System.Net.WebRequestMethods;

namespace ThreadAsyncProjectUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CancellationTokenSource cts = new CancellationTokenSource();
        private string word;
        private int checked_ = 0;
        private object locker = new object();
        private int countFiles;

        public MainWindow()
        {
            InitializeComponent();
            pb.Value = 0;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tb_path.Text)) throw new ArgumentException(nameof(tb_path.Text));

            word = tb_word.Text;
            List<string> files = new List<string>();

            await Task.Run(() =>
            {
                Dispatcher.Invoke(() => GetFolders(tb_path.Text, files));
                countFiles = files.Count;
                Parallel.ForEach(files, (file) => ReadFile(file).Wait());
            });
        }

        private async Task ReadFile(string path)
        {
            int count = 0;
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    while (!sr.EndOfStream)
                    {
                        string? text = await sr.ReadLineAsync();
                        if (text != null && text.Contains(word))
                        {
                            count++;
                        }
                    }
                }
            }

            lock (locker)
            {
                checked_ += 1;
                Dispatcher.Invoke(() => pb.Value = (checked_ * 100) / countFiles);
                Dispatcher.Invoke(() => l_count.Content = $"{checked_}/{countFiles}");
            }
            Dispatcher.Invoke(() => textBlock.Text += $"{System.IO.Path.GetFileName(path)}: {count} | {path}{Environment.NewLine}");
        }

        private void GetFileName(string name)
        {
            Dispatcher.Invoke(() => textBlock.Text += System.IO.Path.GetFileName(name) + Environment.NewLine);
        }

        private void GetFolders(string path, List<string> files)
        {
            foreach (var file in Directory.EnumerateFiles(path))
            {
                files.Add(file);
            }

            foreach (var folder in Directory.EnumerateDirectories(path))
            {
                GetFolders(folder, files);
            }
        }
    }
}
