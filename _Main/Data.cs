using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Deadletter_Monitor
{
    public partial class Data : INotifyPropertyChanged
    {
        private bool isChecked;
        public bool IsChecked
        {
            get { return isChecked; }
            set { isChecked = value; OnPropertyChanged(); }
        }

        public string Location { get; set; }
        public int Count { get; set; }
        public int Aged { get; set; }
        public int AgedTotal { get; set; }
        public int Total { get; set; }
        public int Failed { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            Console.WriteLine("Data PropertyChanged");
        }
    }
}
