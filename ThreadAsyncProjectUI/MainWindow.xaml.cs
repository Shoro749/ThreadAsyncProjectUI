﻿using System.IO;
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
        CancellationTokenSource cts = new CancellationTokenSource();
        private string word = "Hello";
        private int checked_ = 0;
        private object locker = new object();
        private int countFiles;
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            List<string> files = new List<string>();

            await Task.Run(() =>
            {
                GetFolders("C:\\Users\\Роман\\Downloads\\GeneralFolder", files);
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
                    //textBlock.Text = await sr.ReadToEndAsync();
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
                Dispatcher.Invoke(() => l_count.Content = $"{checked_}/{countFiles}");
            }
            Dispatcher.Invoke(() => textBlock.Text += $"{System.IO.Path.GetFileName(path)}: {count}{Environment.NewLine}");
        }

        private void GetFileName(string name)
        {
            Dispatcher.Invoke(() => textBlock.Text += System.IO.Path.GetFileName(name) + Environment.NewLine);
        }

        private async Task TestCancellation(CancellationToken token)
        {
            await Task.Run(() =>
            {
                int i = 0;
                while (true)
                {
                    if (token.IsCancellationRequested)
                    {
                        // cancellation login
                        Dispatcher.Invoke(() => textBlock.Text = "Done");
                        return;
                    }
                    Dispatcher.Invoke(() => textBlock.Text = (i++).ToString());
                    Thread.Sleep(100);
                }
            });
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

        //private async Task SearchWord(string path)
        //{
        //    try
        //    {
        //        foreach (string file in Directory.EnumerateFiles(path))
        //        {
        //            textBlock.Text += file;
        //            //await SearchingInFile(file);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        //private async Task SearchingInFile(string path)
        //{
        //    using (StreamReader reader = new StreamReader(path))
        //    {
        //        string text = await reader.ReadToEndAsync();
        //        foreach (var letter in text)
        //        {
        //            textBlock.Text += letter;
        //        }
        //    }
        //}

        //private async Task<string> ReaderAsync()
        //{
        //    using (StreamReader reader = new StreamReader(path.Text))
        //    {
        //        return await reader.ReadToEndAsync();
        //    }
        //}
    }
}
