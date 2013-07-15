using System.Collections.Generic;

namespace OneDrive.CloudCore.Common
{
    /// <summary>
    /// A folder on any cloud based storage system.
    /// </summary>
    public interface ICloudFolder : ICloudObject
    {
        /// <summary>
        /// Returns the number of items inside this object on the cloud, if a folder.
        /// </summary>
        int ChildObjectCountOnTheCloud { get; }

        /// <summary>
        /// Returns a list of Child objects in case the current object is a folder. The list of child objects will contain meta data aka properties but not the content (me/SkyDrive/files) a content will be downloaded on demand using a separate approach.
        /// </summary>
        IList<ICloudObject> Children { get; }
    }
}
