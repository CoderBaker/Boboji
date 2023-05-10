using System.Windows;

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
        }

        public string RecordName { get; private set; } = string.Empty;

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            this.RecordName = this.TextBoxInput.Text;
            this.DialogResult = true;
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.RecordName = string.Empty;
            this.DialogResult = false;
        }
    }
}
