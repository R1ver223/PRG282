using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace BaobabWildlifeRehab.Features
{
    public static class SummaryReport
    {
        
        public static string BuildReport(List<Animal> animals)
        {
            string[] statuses = { "Critical", "Serious", "Stable", "Recovering", "Release-Ready" };
            string averageAge = "N/A";
            string averageScore = "N/A";

            // averages only exist when there is at least one animal
            if (animals.Count > 0)
            {
                averageAge = animals.Average(a => a.Age).ToString("F2", CultureInfo.InvariantCulture);
                averageScore = animals.Average(a => a.RecoveryScore).ToString("F2", CultureInfo.InvariantCulture);
            }

            StringBuilder report = new StringBuilder();
            report.AppendLine("BAOBAB RIDGE WILDLIFE REHABILITATION CENTRE");
            report.AppendLine("ANIMAL SUMMARY REPORT");
            report.AppendLine("Generated: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture));
            report.AppendLine(new string('-', 40));
            report.AppendLine("Total animals:".PadRight(26) + animals.Count);
            report.AppendLine("Average age (years):".PadRight(26) + averageAge);
            report.AppendLine("Average recovery score:".PadRight(26) + averageScore);
            report.AppendLine(new string('-', 40));
            report.AppendLine("Animals per status:");

            
            foreach (string status in statuses)
            {
                int count = animals.Count(a => a.Status == status);
                report.AppendLine(("  " + status + ":").PadRight(26) + count);
            }

            return report.ToString();
        }

        
        public static void SaveReport(string report)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "summary.txt");
            File.WriteAllText(path, report);
        }
    }
}