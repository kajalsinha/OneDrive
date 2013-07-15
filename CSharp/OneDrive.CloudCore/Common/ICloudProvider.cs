using System.Threading.Tasks;

namespace OneDrive.CloudCore.Common
{
    /// <summary>
    /// The cloud provider which will provide basic tasks related to accessing the cloud objects.
    /// </summary>
    public interface ICloudProvider
    {
        /// <summary>
        /// Name of the cloud provider.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Returns the Drive for which this provider is constructed.
        /// </summary>
        SupportedDrive Drive { get; }

        /// <summary>
        /// URL of the cloud provider.
        /// </summary>
        string BaseUrl { get; }

        /// <summary>
        /// UserName for the given CloudDrive
        /// </summary>
        string UserName { get; }

        /// <summary>
        /// Returns the cloud object under the given unique path. If the cloud object is a folder then this method also populates a collection containing the child objects and its parent. 
        /// </summary>
        /// <param name="uniqueOneDrivePath"></param>
        /// <param name="fetchChildren"></param>
        /// <returns></returns>
        Task<ICloudObject> GetCloudObjectAsync(string uniqueOneDrivePath, bool fetchChildren = true);

        /// <summary>
        /// Returns the root directory for the current cloud provider.
        /// </summary>
        /// <returns></returns>
        Task<ICloudFolder> GetRootDirectoryAsync();

        /// <summary>
        /// Returns the translated path for the given unique onedrive path. e.g. OneDrive://SkyDrive/UserName can be translated to me/SkyDrive or folder.72981f91d7bb629c.
        /// </summary>
        /// <param name="uniqueOneDrivePath"></param>
        /// <returns></returns>
        Task<string> GetTranslatedPathFromAsync(string uniqueOneDrivePath);

        /// <summary>
        /// Method to download this ICloudFile.
        /// </summary>
        /// <returns>Running task which is downloading the ICloudFile</returns>
        Task<bool> OpenFileAsync(ICloudFile objectToDownload, bool forceDownload = false, bool showOpenWith = false);

        string GeneratePathIncludingProviderName(string[] directories);
    }
}
