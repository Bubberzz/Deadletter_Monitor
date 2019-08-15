using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Deadletter_Monitor
{
    internal class TodayViewModel
    {
        public List<DailyDeadletters> Today { get; }

        public TodayViewModel(DailyDeadletters addDeadletters)
        {
            Today = new List<DailyDeadletters>
            {
                addDeadletters
            };
        }

        internal class DailyDeadletters
        {
            public string Title { get; }
            public int Percentage { get; }

            public DailyDeadletters()
            {
                Title = "Deadletters";
                Percentage = CalculatePercentage();
            }

            private static int CalculatePercentage()
            {
                var todayFiles = Directory.GetFiles(SetFilePath.Archive);
                return todayFiles.Count(file => File.GetCreationTime(file) > DateTime.Today);
            }
        }
    }
}
