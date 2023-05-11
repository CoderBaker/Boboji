using System.Collections.ObjectModel;
using ContextRecord.Wpf.DataStructures;

namespace ContextRecord.Wpf.ViewModels
{
    public class RecordSelectorViewModel
    {
        public ObservableCollection<Record> Records { get; set; } = new ObservableCollection<Record>();

        public Record? SelectedItem { get; set; }
    }
}
