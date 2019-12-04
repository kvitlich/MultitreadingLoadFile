using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LoadingPhotoMultithreading
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

        private void Download(object sender, RoutedEventArgs e)
        {
            downloadingProgressBar.IsIndeterminate = true;
            var tread = new Thread(DownloadFileProces);
            tread.IsBackground = true;
            tread.Start(fileUriSourse.Text);
        }

        private void DownloadFileProces(object uri)
        {
            try
            {
                WebClient client = new WebClient();
                MessageBox.Show($"{uri}");
                client.DownloadFile($"{uri}", $"{Directory.GetCurrentDirectory()}/{System.IO.Path.GetFileName($"{uri}")}");
                Thread.Sleep(200);
            }
            catch
            {
                MessageBox.Show("Ошибка, возможно указан неверный URL");
            }
                this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, (ThreadStart)delegate
                {
                    downloadingProgressBar.IsIndeterminate = false;
                });
        }
    }
}
