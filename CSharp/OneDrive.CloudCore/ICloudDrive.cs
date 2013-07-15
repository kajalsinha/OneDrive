using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneDrive.CloudCore
{


    /// <summary>
    /// Any object which is in the cloud  space.
    /// </summary>
    public interface ICloudObject
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public ICloudObject Parent { get; set; }

        public IDownloadToken Download();
        public bool Delete();
        public bool Rename(string newName);

        public bool IsValid { get; }
        public CloudPermission GetPermissions();

    }

    /// <summary>
    /// A folder on any cloud based storage system.
    /// </summary>
    public interface ICloudFolder : ICloudObject
    {

    }

    /// <summary>
    /// A file on any cloud based storage system.
    /// </summary>
    public interface ICloudFile : ICloudObject
    {

    }


    /// <summary>
    /// A handle to a cloud drive. e.g. SkyDrive, GDrive or DropBox.
    /// </summary>
    public interface ICloudDrive : ICloudSecurity
    {
        /// <summary>
        /// Returns path to the root directory of this cloud drive.
        /// </summary>
        /// <returns>Path to the root directory of this cloud drive.</returns>
        ICloudFolder GetRootFolder();
        /// <summary>
        /// Returns all folder paths in the given parent folder.
        /// </summary>
        /// <param name="parent">The folder whose sub folders has to be retrieved.</param>
        /// <returns>Array of all child folders.</returns>
        ICloudFolder[] GetFolders(ICloudFolder parent);

        /// <summary>
        /// Returns all folder paths in the given parent folder.
        /// </summary>
        /// <param name="parent">The folder whose sub folders has to be retrieved.</param>
        /// <param name="filter">A windows style filter for the folders</param>
        /// <returns>Paths to all child folders.</returns>
        ICloudFolder[] GetFolders(ICloudFolder parent, string filter);

        /// <summary>
        /// Returns all file paths in the given parent folder.
        /// </summary>
        /// <param name="parent">The parent folder from where files need to be retrieved.</param>
        /// <returns>Path to all child files.</returns>
        ICloudFile[] GetFiles(ICloudFolder parent);

        /// <summary>
        /// Returns all file paths in the given parent folder.
        /// </summary>
        /// <param name="parent">The parent folder from where files need to be retrieved.</param>
        /// <param name="filter">A windows style filter for the files.</param>
        /// <returns>Path to all child files.</returns>
        ICloudFile[] GetFiles(ICloudFolder parent, string filter);


    }


    /// <summary>
    /// Common point to provide cloud based security for any cloud drive.
    /// </summary>
    public interface ICloudSecurity
    {
        public bool SignIn();
        public bool SignIn(string userName, string password, ICloudProvider provider);
        public bool SignOut();
        public ISecurityToken SecurityToken { get; }
    }

  


    public interface ISkyDrive : ICloudDrive
    {
        public ICloudProvider CloudProvider { get; }
       
    }


}
