using GalaSoft.MvvmLight;
using MoonPdfLib.MuPdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
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

namespace PdfUrlViewTool
{
    public class PdfEntry : ViewModelBase
    {
        private string url;
        public string Url
        {
            get { return url; }
            set { Set(ref url, value); }
        }

        private int index;
        public int Index
        {
            get { return index; }
            set { Set(ref index, value); }
        }
    }


    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = this;

            PdfEntries = new BindingList<PdfEntry>();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }


        public BindingList<PdfEntry> pdfEntries;
        public BindingList<PdfEntry> PdfEntries
        {
            get { return pdfEntries; }
            set
            {
                pdfEntries = value;
                pdfEntries.ListChanged += PdfEntries_ListChanged; ;
                RaisePropertyChanged("PdfEntries");
            }
        }

        private void PdfEntries_ListChanged(object sender, ListChangedEventArgs e)
        {
            //throw new NotImplementedException();
        }



        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Uri uri = new Uri((e.AddedItems[0] as PdfEntry).Url);

                using (var client = new WebClient())
                {
                    var pdf_data = client.DownloadData(uri);
                    if (pdf_data.Length <= 0)
                        throw new Exception(string.Format("pdf文件:{0} 有问题，未下载成功", uri));

                    moonPdfPanel.Open(new MemorySource(pdf_data));
                    //moonPdfPanel.ZoomToHeight();
                    moonPdfPanel.ZoomToWidth();
                    
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            InputTextDialog dlg = new InputTextDialog();
            if (dlg.ShowDialog() == true)
            {
                string txt = dlg.TxtBoxValue;
                string[] lines = txt.Split('\n');

                var tl = lines.Select((v, idx) => new PdfEntry { Url = v, Index = idx + 1 }).ToList();

                PdfEntries = new BindingList<PdfEntry>(tl);
            }
        }
    }
}
