using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;

namespace Deadletter_Monitor
{
    public class GetLiveDeadletters : INotifyPropertyChanged
    {
        private ObservableCollection<Data> _dataGridInput;
        public ObservableCollection<Data> DataGridInput
        {
            get => _dataGridInput;
            set
            { ReprocessList = value; _dataGridInput = value; OnPropertyChanged(); }
        }
        public static ObservableCollection<Data> ReprocessList;
        private bool? _isAllSelected;
        public bool? IsAllSelected
        {
            get => _isAllSelected;
            set
            {
                _isAllSelected = value;
                OnPropertyChanged();
            }
        }
        public RelayCommand CheckStudentCommand { get; }
        public RelayCommand CheckAllStudentsCommand { get; }

        public GetLiveDeadletters()
        {
            DataGridInput = GetFiles();
            CheckStudentCommand = new RelayCommand(OnCheckLocation);
            CheckAllStudentsCommand = new RelayCommand(OnCheckAllLocations);
            IsAllSelected = false;
        }

        private void OnCheckAllLocations()
        {
            if (IsAllSelected == true)
            {
                foreach (var x in DataGridInput)
                {
                    x.IsChecked = true;
                    Console.WriteLine("IsAllSelect True Foreach IsChecked True");
                }
            }
            else
            {
                foreach (var x in DataGridInput)
                {
                    x.IsChecked = false;
                    Console.WriteLine("IsAllSelect False Foreach IsChecked False");
                }
            }
        }

        private void OnCheckLocation()
        {
            if (DataGridInput.All(x => x.IsChecked))
            {
                IsAllSelected = true;
                Console.WriteLine("OnCheckLocation IsAllSelect True");
            }
            else if (DataGridInput.All(x => !x.IsChecked))
            {
                IsAllSelected = false;
                Console.WriteLine("OnCheckLocation IsAllSelect False");
            }
            else
            {
                IsAllSelected = null;
                Console.WriteLine("IsAllSelect Null");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            Console.WriteLine("GetLiveDeadletters PropertyChanged");

        }

        public static ObservableCollection<Data> GetFiles()
        {
            var listViewInput = new ObservableCollection<Data>();
            var lines = File.ReadAllLines(SetFilePath.Locations);
            var locations = new ObservableCollection<string>(lines);
            var total = 0;
            var agedTotal = 0;
            var failed = Directory.GetFiles(SetFilePath.Failed).Length;

            foreach (var location in locations)
            {
                try
                {
                    var count = Directory.GetFiles(location, "*.failed", SearchOption.TopDirectoryOnly).Length;
                    total += count;
                    var agedList = Directory.GetFiles(location);
                    var aged = agedList.Count(file => File.GetCreationTime(file) < DateTime.Now.AddHours(-1));
                    agedTotal += aged;
                    listViewInput.Add(new Deadletter_Monitor.Data() { Location = location, Count = count, Aged = aged, AgedTotal = agedTotal, Total = total, Failed = failed });
                }
                catch (Exception x)
                {
                    MessageBox.Show(x.Message);
                }
            }
            return listViewInput;
        }
    }
}
