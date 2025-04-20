
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AutoRoids.Test
{
    internal class clsRename
    {

        static void Main(string[] args)
        {
            string directoryPath = @"C:\YourDirectory"; // Directory containing the files
            string outputPath = @"C:\YourDirectory\sortedFiles.txt"; // Output file to save sorted filenames with index

            // Get all files in the directory
            var files = Directory.GetFiles(directoryPath);

            // Sort files using natural sort order
            var sortedFiles = files.OrderBy(f => f, new NaturalStringComparer()).ToList();

            // Save each filename with its index number
            using (var writer = new StreamWriter(outputPath))
            {
                for (int i = 0; i < sortedFiles.Count; i++)
                {
                    string filename = Path.GetFileName(sortedFiles[i]);
                    writer.WriteLine($"{i + 1}: {filename}");
                }
            }

            Console.WriteLine("Files have been sorted and saved.");
        }
    }

    // Custom comparer for natural string sorting
    public class NaturalStringComparer : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            // Use regular expressions to separate numbers from text
            var regex = new Regex(@"(\d+)|(\D+)");
            var xMatches = regex.Matches(x);
            var yMatches = regex.Matches(y);

            for (int i = 0; i < Math.Min(xMatches.Count, yMatches.Count); i++)
            {
                // Compare text
                int textResult = string.Compare(xMatches[i].Value, yMatches[i].Value, StringComparison.OrdinalIgnoreCase);
                if (textResult != 0)
                    return textResult;

                // If texts are equal, compare numbers
                if (int.TryParse(xMatches[i].Value, out int xNum) && int.TryParse(yMatches[i].Value, out int yNum))
                {
                    int numberResult = xNum.CompareTo(yNum);
                    if (numberResult != 0)
                        return numberResult;
                }
            }

            // If one string is a prefix of the other, the shorter string is considered smaller
            return xMatches.Count.CompareTo(yMatches.Count);
        }
    }


}

