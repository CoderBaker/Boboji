using System.Windows;
using ContextRecord.Wpf.ViewModels;

namespace ContextRecord.Wpf
{
    /// <summary>
    /// Interaction logic for RecordNameInputWindow.xaml
    /// </summary>
    public partial class RecordNameInputWindow : Window
    {
        public RecordNameInputWindow()
        {
            InitializeComponent();
            DataContext = new RecordNameInputViewModel();
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
