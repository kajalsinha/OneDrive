using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OneDrive.CloudCore.Common
{

    /// <summary>
    /// Any object which is in the cloud  space. The base class for all cloud objects.
    /// </summary>
    public interface ICloudObject
    {
        /// <summary>
        /// The unique id which can be used to identify the cloud object individually.
        /// </summary>
        string Id { get; }

        /// <summary>
        /// The name of ICloudObject
        /// </summary>
        string Name { get;}

        /// <summary>
        /// User Name who created this Cloud Object.
        /// </summary>
        string CreatedBy { get; }

        /// <summary>
        /// User Id of individual who created this Cloud Object.
        /// </summary>
        string CreatedByUserId { get; }

        /// <summary>
        /// Parent of current ICloudObject..
        /// </summary>
        ICloudObject Parent { get; }

        /// <summary>
        /// Description text for this Cloud Object.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Returns the full path of this Cloud Object. Web path in case of an online folder. e.g. c:\abc\xyz in case of a LocalDisk and something like folder.72981f91d7bb629c in case of skydrive as skydrive API does not supports '\' or '/' separated path.
        /// </summary>
        string FullPath { get; }

        /// <summary>
        /// Date and Time when this Cloud Object was created.
        /// </summary>
        DateTime CreatedOn { get; }

        /// <summary>
        /// Date and Time when this Cloud Object was last accessed.
        /// </summary>
        DateTime UpdatedOn { get; }

        /// <summary>
        /// Total number of comments for this Cloud Object.
        /// </summary>
        int CommentsCount { get; }

        /// <summary>
        /// Returns true of the comments are enabled for this Cloud Object.
        /// </summary>
        bool CommentsEnabled { get; }

        /// <summary>
        /// Returns if this folder is shared with the users including "Just me."
        /// </summary>
        string[] SharedWith { get; }

        /// <summary>
        /// The location where the files/folder related to this folder can be uploaded.
        /// </summary>
        string UploadLocation { get;}

        

        /// <summary>
        /// Saves the ICloudObject to the given destination. 
        /// </summary>
        /// <param name="destination"></param>
        /// <returns></returns>
        Task<CloudResult> Save(string destination);

        /// <summary>
        /// Deletes the given ICloudObject
        /// </summary>
        /// <returns></returns>
        bool Delete();

        /// <summary>
        /// Renames the current ICloudObject to a newName.
        /// </summary>
        /// <param name="newName">The newName of ICloudObject.</param>
        /// <returns>True if rename is successful else false.</returns>
        bool Rename(string newName);

        /// <summary>
        /// Indicates if the given ICloudObject is valid as it may exists on multiple locations.
        /// </summary>
        bool IsValid { get; }

        /// <summary>
        /// Returns the permissions associated with the current ICloudObject.
        /// </summary>
        /// <returns></returns>
        CloudPermission GetPermissions();

        /// <summary>
        /// Returns true if this ICloudObject is available offline.
        /// </summary>
        bool IsAvailableOffline { get; }

        /// <summary>
        /// Returns true if this ICloudObject is partitioned and exists on multiple locations as splitted files or folders.
        /// </summary>
        bool IsPartitioned { get; }

        /// <summary>
        /// Brings a copy of this ICloudObject as offline.
        /// </summary>
        /// <returns></returns>
        Task<CloudResult> TakeOffline();

        /// <summary>
        /// The combined size of current ICloudObject in bytes.
        /// </summary>
        long Size { get; }


        /// <summary>
        /// A path which is virtual but something which is supported by OneDrive. e.g. OneDrive://SkyDrive/me/abc/def and using a PathTranslator this could translate to something like folder.72981f91d7bb629c in case of SkyDrive
        /// </summary>
        string GetOneDriveFullPath();

        /// <summary>
        /// If a OneDrive Object shared and partitioned across multiple cloud providers then the partition information so that the details can be retrieved.
        /// </summary>
        IList<IObjectPartition> Partitions { get; }


    }
}
