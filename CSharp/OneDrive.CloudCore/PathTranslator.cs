using OneDrive.CloudCore.Common;
using OneDrive.CloudCore.Providers;

namespace OneDrive.CloudCore
{
   
    public static class PathTranslator
    {
        
        static readonly string DropBoxPrefix = OneDriveSettings.ONE_DRIVE_PROTOCOL + "DropBox" + OneDriveSettings.PATH_SEPERATOR;


        public static string TranslatePathForDrive(string uniquePath, SupportedDrive fromDrive)
        {
            switch (fromDrive)
            {
                case SupportedDrive.SkyDrive:
                    return GetTranslatePathForSkyDrive(uniquePath);

            }
            return null;
        }

        private static string GetTranslatePathForSkyDrive(string uniquePath)
        {
           // return uniquePath.Substring(SkyDriveProvider.SkyDrivePrefix.Length);
            //format : OneDrive://SkyDrive/UserName or Me/folderorfilepath
            return null;
        }
    }
}