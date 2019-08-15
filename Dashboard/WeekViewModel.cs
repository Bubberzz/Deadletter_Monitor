using System.Collections.Generic;
using System.IO;

namespace Deadletter_Monitor
{
    internal class WeekViewModel
    {
        public List<WeeklyDeadletters> ThisWeek { get; }

        public WeekViewModel(WeeklyDeadletters addDeadletters)
        {
            ThisWeek = new List<WeeklyDeadletters>
            {
                addDeadletters
            };
        }

        internal class WeeklyDeadletters
        {
            public string Title { get; }
            public int Percentage { get; }

            public WeeklyDeadletters()
            {
                Title = "Deadletters";
                Percentage = CalculatePercentage();
            }

            private static int CalculatePercentage()
            {
                var count = Directory.GetFiles(SetFilePath.Archive, "*.failed", SearchOption.TopDirectoryOnly).Length;
                return count;
            }
        }
    }
}
