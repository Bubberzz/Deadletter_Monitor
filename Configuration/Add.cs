using System;
using System.IO;
using System.Windows;

namespace Deadletter_Monitor
{
    public class Add
    {
        public static void add()
        {
            var input =
                Microsoft.VisualBasic.Interaction.InputBox("Type in the deadletter location you want to add.",
                    "Add",
                    "C:\\Users\\Stan.bubbers\\Desktop\\Deadletters\\Deadletters2\\",
                    -1, -1);

            if (Directory.Exists(input))
            {
                File.AppendAllText(SetFilePath.Locations, Environment.NewLine + input);
                MessageBox.Show(input, "Successfully added!");

                // Write to log
                using (var w = File.AppendText(SetFilePath.Log))
                {
                    Log.log($"Added:\n{input}", w);
                }
            }
            else
            {
                MessageBox.Show(input, "Location doesn't exist");
                // Write to log
                using (var w = File.AppendText(SetFilePath.Log))
                {
                    Log.log($"Could not add:\n{input}", w);
                }
            }
        }
    }
}
