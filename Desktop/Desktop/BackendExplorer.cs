using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Desktop
{
    public class BackendExplorer
    {
        public static void Laufwerke(Explorer ex)
        {
            string drivePath = "C:\\";

            DriveInfo driveInfo = new DriveInfo(drivePath);
            long freeSpaceInGB = driveInfo.AvailableFreeSpace / (1024 * 1024 * 1024);
            long totalSpaceInGB = driveInfo.TotalSize / (1024 * 1024 * 1024);

            ex.TextBoxSpeicherAnzeige.Text = $"{freeSpaceInGB} GB frei von {totalSpaceInGB} GB";

            double percentageUsed = ((double)(totalSpaceInGB - freeSpaceInGB) / totalSpaceInGB) * 100;
            ex.ProgressBarGB.Value = percentageUsed;

        }
    }
}
