using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneDrive.CloudCore.Common
{
    public static class CloudExtensions
    {
        public static bool IsQualifiedAsSmallDownload(this ICloudFile objectToDownload)
        {
            return objectToDownload.Size <= OneDriveSettings.MaxSmallDownloadSize;
        }
    }
}


