using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;

namespace Deadletter_Monitor
{
    public class Remove
    {
        public static void remove()
        {
            var lines = File.ReadAllLines(SetFilePath.Locations);
            var reprocessList = GetLiveDeadletters.ReprocessList;
            var dir = new List<string>();

            foreach (var i in reprocessList)
            {
                if (i.IsChecked)
                {
                    dir.Add(Path.GetFullPath(i.Location));
                }
            }

            var newList = lines.Where(line => !dir.Contains(line)).ToList();

            var s = string.Join(Environment.NewLine, dir);
            var p = string.Join(Environment.NewLine, newList);

            File.WriteAllText(SetFilePath.Locations, p);
            MessageBox.Show(s, "Successfully removed!");

            // Write to log
            using (var w = File.AppendText(SetFilePath.Log))
            {
                Log.log($"Removed:\n{s}", w);
            }
        }
    }
}
