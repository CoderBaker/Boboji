using System.Windows;
using ContextRecord.Wpf.ViewModels;

namespace ContextRecord.Wpf
{
    /// <summary>
    /// Interaction logic for RecordSelectorWindow.xaml
    /// </summary>
    public partial class RecordSelectorWindow : Window
    {
        public RecordSelectorWindow()
        {
            InitializeComponent();
            DataContext = new RecordSelectorViewModel();
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
