using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OneDrive.CloudCore.Common;

namespace OneDrive.CloudCore.Drives
{
    /// <summary>
    /// A handle to a cloud drive. e.g. SkyDrive, GDrive or DropBox.
    /// </summary>
    public interface ICloudDrive : ICloudSecurity
    {
        /// <summary>
        /// Returns path to the root directory of this cloud drive.
        /// </summary>
        /// <returns>Path to the root directory of this cloud drive.</returns>
        Task<ICloudFolder> GetRootFolderAsync();
        /// <summary>
        /// Returns all folder paths in the given parent folder.
        /// </summary>
        /// <param name="parent">The folder whose sub folders has to be retrieved.</param>
        /// <returns>Array of all child folders.</returns>
        Task<ICloudFolder[]> GetFoldersAsync(ICloudFolder parent);

        /// <summary>
        /// Returns all folder paths in the given parent folder.
        /// </summary>
        /// <param name="parent">The folder whose sub folders has to be retrieved.</param>
        /// <param name="filter">A windows style filter for the folders</param>
        /// <returns>Paths to all child folders.</returns>
        Task<ICloudFolder[]> GetFoldersAsync(ICloudFolder parent, string filter);

        /// <summary>
        /// Returns all file paths in the given parent folder.
        /// </summary>
        /// <param name="parent">The parent folder from where files need to be retrieved.</param>
        /// <returns>Path to all child files.</returns>
        Task<ICloudFile[]> GetFilesAsync(ICloudFolder parent);

        /// <summary>
        /// Returns all file paths in the given parent folder.
        /// </summary>
        /// <param name="parent">The parent folder from where files need to be retrieved.</param>
        /// <param name="filter">A windows style filter for the files.</param>
        /// <returns>Path to all child files.</returns>
        Task<ICloudFile[]> GetFilesAsync(ICloudFolder parent, string filter);

        /// <summary>
        /// The cloud service provider information.
        /// </summary>
        ICloudProvider CloudProvider { get; }
    }

}
