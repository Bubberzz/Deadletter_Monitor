using System;
using System.IO;
using System.Linq;
using System.Windows;

namespace Deadletter_Monitor
{
    public class Reprocess
    {
        public static void reprocess()
        {
            //Get files
            var reprocessList = GetLiveDeadletters.ReprocessList;
            var logCount = new int();
            foreach (var i in reprocessList)
            {
                if (!i.IsChecked) continue;
                var files = Directory.GetFiles(i.Location, "*.failed")
                    .ToArray();
                var dir = Path.GetFullPath(i.Location);
                var oneUpDir = Path.GetFullPath(Path.Combine(dir, @"..\"));
                var processingDir = oneUpDir + "processing\\";
                logCount += files.Length;

                //rename and move
                foreach (var file in files)
                {
                    var name = Path.GetFileName(file);
                    string nameDir = null;
                    nameDir = processingDir + name;
                    try
                    {
                        if (!File.Exists($"{SetFilePath.Archive}\\{name}"))
                        {
                            File.Copy(file, $"{SetFilePath.Archive}\\{name}");
                        }
                        else
                        {
                            File.Copy(file, $"{SetFilePath.Failed}\\{name}", true);
                        }
                        File.Move(file, Path.ChangeExtension(nameDir, ".processing"));
                    }
                    catch (Exception)
                    {
                        // ignored
                    }
                }
            }
            MessageBox.Show($"{logCount} deadletters processed");

            // Write to log
            using (var w = File.AppendText(SetFilePath.Log))
            {
                Log.log($"Reprocessed {logCount} deadletters", w);
            }
        }
    }
}
