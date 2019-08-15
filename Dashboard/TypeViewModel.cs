using System.Collections;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace Deadletter_Monitor
{
    public class TypeViewModel
    {
        public ObservableCollection<Data> ListViewInput { get; }
        public TypeViewModel()
        {
            ListViewInput = new ObservableCollection<Data>();
            var files = Directory.GetFiles(SetFilePath.Archive, "*.failed")
            .Select(Path.GetFileName)
            .ToArray();
            foreach (var file in files)
            {
                var name = "";
                foreach (var letter in file)
                {
                    var isLetterOrDigit = char.IsLetterOrDigit(letter);
                    if (isLetterOrDigit)
                    {
                        name += letter;
                    }
                    else
                    {
                        break;
                    }
                }
                var count = Directory.GetFiles(SetFilePath.Archive, $"{name}*.failed").Length;
                ListViewInput.Add(new Data() { Name = name, Number = count });
            }
        }
        public object SelectedItem { get; set; } = null;
    }

    // Class which represents a data point in the chart
    public partial class Data : IEnumerable
    {
        public string Name { get; set; }
        public int Number { get; set; }
        public IEnumerator GetEnumerator()
        {
            throw new System.NotImplementedException();
        }
    }
}

