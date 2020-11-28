using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PdfUrlViewTool
{
    /// <summary>
    /// InputTextDialog.xaml 的交互逻辑
    /// </summary>
    public partial class InputTextDialog : Window
    {
        public InputTextDialog()
        {
            InitializeComponent();
        }

        public static DependencyProperty TxtBoxValueProperty = DependencyProperty.Register(
            "TxtBoxValue", typeof(String), typeof(InputTextDialog));

        public String TxtBoxValue
        {
            get { return (String)GetValue(TxtBoxValueProperty); }
            set
            {
                SetValue(TxtBoxValueProperty, value);
            }
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (e.Property == TxtBoxValueProperty)
            {
                // Do whatever you want with it
            }
        }

        private void ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
