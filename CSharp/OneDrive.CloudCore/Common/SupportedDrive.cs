using System;

namespace OneDrive.CloudCore.Common
{
    /// <summary>
    /// Flags to indicate the supported drives by this App.
    /// </summary>
    [Flags]
    public enum SupportedDrive
    {
        /// <summary>
        /// Supports access to locally connected or internal drive.
        /// </summary>
        LocalDrive = 0,

        /// <summary>
        /// Supports access to externally connected drive.
        /// </summary>
        ExternalDrive = 1,

        /// <summary>
        /// Supports access to drives connected/mapped over the network.
        /// </summary>
        NetworkDrive = 2,

        /// <summary>
        /// Supports OneDrive cloud storage.
        /// </summary>
        OneDrive = 4,

        /// <summary>
        /// Supports SkyDrive cloud storage.
        /// </summary>
        SkyDrive = 8,

        /// <summary>
        /// Supports GoogleDrive cloud storage.
        /// </summary>
        GoogleDrive = 16,

        /// <summary>
        /// Supports DropBox cloud storage.
        /// </summary>
        DropBox = 32,

        /// <summary>
        /// Supports Box cloud storage.
        /// </summary>
        Box = 64
    }
}
