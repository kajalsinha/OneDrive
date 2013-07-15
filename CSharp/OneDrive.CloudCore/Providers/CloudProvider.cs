using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OneDrive.CloudCore.Common;

namespace OneDrive.CloudCore.Providers
{
    /// <summary>
    /// The base class for any cloud based provider.
    /// </summary>
    public abstract class CloudProvider : ICloudProvider
    {
        public static readonly IDownloadManager DownloadManager = new DownloadManager();

        protected CloudProvider(string name, SupportedDrive drive, string baseUrl)
        {
            this.Name = name;
            this.Drive = drive;
            this.BaseUrl = baseUrl;
        }


        public string GeneratePathIncludingProviderName(string[] directories)
        {
            return OneDriveSettings.ONE_DRIVE_PROTOCOL + string.Join(OneDriveSettings.PATH_SEPERATOR.ToString(), directories);
        }

        /// <summary>
        /// Name of the cloud provider.
        /// </summary>
        public string Name
        {
            get;
            private set;

        }

        /// <summary>
        /// Returns the Drive for which this provider is constructed.
        /// </summary>
        public SupportedDrive Drive
        {
            get;
            private set;
        }

        /// <summary>
        /// URL of the cloud provider.
        /// </summary>
        public string BaseUrl
        {
            get;
            private set;
        }

        /// <summary>
        /// UserName for the given CloudDrive
        /// </summary>
        public string UserName
        {
            get;
            protected set;
        }

        /// <summary>
        /// Returns the cloud object under the given unique path. If the cloud object is a folder then this method also populates a collection containing the child objects and its parent. 
        /// </summary>
        /// <param name="uniquePath"></param>
        /// <param name="fetchChildren"></param>
        /// <returns></returns>
        public abstract Task<ICloudObject> GetCloudObjectAsync(string uniquePath, bool fetchChildren = true);

        public abstract Task<ICloudFolder> GetRootDirectoryAsync();

        /// <summary>
        /// Returns the translated path for the given unique onedrive path. e.g. OneDrive://SkyDrive/ can be translated to me/SkyDrive or folder.72981f91d7bb629c.
        /// </summary>
        /// <param name="uniqueOneDrivePath"></param>
        /// <returns></returns>
        public abstract Task<string> GetTranslatedPathFromAsync(string uniqueOneDrivePath);


        /// <summary>
        /// Method to download this ICloudObject.
        /// </summary>
        /// <returns>Running task which is downloading the ICloudObject</returns>
        public abstract Task<bool> OpenFileAsync(ICloudFile cloudFile, bool forceDownload = false, bool showOpenWith = false);
    }
}
