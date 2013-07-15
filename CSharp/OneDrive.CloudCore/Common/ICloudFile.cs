namespace OneDrive.CloudCore.Common
{
    /// <summary>
    /// A file on any cloud based storage system.
    /// </summary>
    public interface ICloudFile : ICloudObject
    {

        /// <summary>
        /// The location from where the given cloud object can be downloaded.
        /// </summary>
        string DownloadSourceLocation { get; }
    }
}
