using OneDrive.CloudCore.Common;
using OneDrive.CloudCore.Drives;
using OneDrive.CloudCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace OneDrive.CloudCore
{


    [DataContract]
    public class OneDriveSettings
    {
        public static readonly OneDriveSettings _defaultSettings = new OneDriveSettings();

        /// <summary>
        /// Initialize basic settings in OneDriveSettings instance.
        /// </summary>
        public OneDriveSettings()
        {
            //TODO: Supports only SkyDrive for the initial launch.
            this.SupportedDrives = SupportedDrive.SkyDrive;

        }

        public static OneDriveSettings DefaultSettings
        {
            get { return _defaultSettings; }
        }


        /// <summary>
        /// Flags which indicates the supported drives by this app.
        /// </summary>
        [DataMember]
        public SupportedDrive SupportedDrives { get; protected set; }

        /// <summary>
        /// Enables One Drive to allow partitioning of files.
        /// </summary>
        [DataMember]
        public bool AllowPartitions { get; set; }

        /// <summary>
        /// Allows Offline availablility of files.
        /// </summary>
        [DataMember]
        public bool AllowOffline { get; set; }


        /// <summary>
        /// Allow Caching of files. This is different from bringing files offline. Offline files are suggested by users whereas Cachine of files is done automatically by the OneDrive system based on the usage or size of files.
        /// </summary>
        [DataMember]
        public bool AllowCaching { get; set; }


        /// <summary>
        /// Flag to allow indexing. Indexing happens offline to make the search fast.
        /// </summary>
        [DataMember]
        public bool AllowIndexing { get; set; }


        /// <summary>
        /// Indicates how many hours can an object be kept in cache. The user should be able to clear cache and the offline files as well.
        /// </summary>
        [DataMember]
        public int HoursToKeepObjectInCache { get; set; }



        [DataMember]
        public OneDriveColumn[] SortOrders { get; set; }

        [DataMember]
        public OneDriveColumn[] ShowColumnsInOrder { get; set; }


        /// <summary>
        /// Indicates if the content need to be compressed.
        /// </summary>
        [DataMember]
        public bool Compress { get; set; }

        /// <summary>
        /// TODO: Allow encoding. This is experimental.
        /// </summary>
        [DataMember]
        public bool AllowSubZeroEncoding { get; set; }

        /// <summary>
        /// Used to allow mirroring of One drive across various devices or DriveMap locations.
        /// For examples across various accounts, FirstDriveMap (SkyDrive + DropBox) = 9.5GB , SecondDriveMap (SkyDrive + DropBox) = 9.5GB 
        /// FirstDriveMap changes are mirrored to SecondDriveMap.
        /// </summary>
        [DataMember]
        public bool AllowMirroring { get; set; }

        /// <summary>
        /// The mapping of cloud where various drives should be considered as one.
        /// For example if SkyDrive offers 7 GB and DropBox offers 2.5 GB then in the Map the drive is considered as One 9.5 GB.
        /// </summary>
        [DataMember]
        public IList<ICloudDrive> CloudMap { get; set; }

        /// <summary>
        /// The mapping of cloud where various drives should be considered as one.
        /// For example if SkyDrive offers 7 GB and DropBox offers 2.5 GB then in the Map the drive is considered as One 9.5 GB.
        /// But, this Map should be considered for Backup from CloudMap.
        /// </summary>
        [DataMember]
        public IList<ICloudDrive> BackupCloudMap { get; set; }

        /// <summary>
        /// Indicates the hours to keep objects offline.
        /// </summary>
        [DataMember]
        public int HoursToKeepObjectsOffline { get; set; }

        /// <summary>
        /// The maximum cache size.
        /// </summary>
        [DataMember]
        public int MaxCacheSize { get; set; }

        /// <summary>
        /// The maximum size of object in cache.
        /// </summary>
        [DataMember]
        public int MaxCachedObjectSize { get; set; }

        /// <summary>
        /// The maximum size of the object which can be kept offline.
        /// </summary>
        [DataMember]
        public int MaxOfflineObjectSize { get; set; }

        /// <summary>
        /// The maximum offline size.
        /// </summary>
        [DataMember]
        public int MaxOfflineSize { get; set; }


        /// <summary>
        /// Returns the PathSeparator for OneDrive directories. It  is '/'
        /// </summary>
        [IgnoreDataMember] public const char PATH_SEPERATOR = '/';

        /// <summary>
        /// The one drive protocol. "OneDrive:"
        /// </summary>
        [IgnoreDataMember] public const string ONE_DRIVE_PROTOCOL = "OneDrive:";

        [IgnoreDataMember] public const double MAX_RETENTION_SECONDS = 100;

        [DataMember] public static int MaxSmallDownloadSize = 200*1024; //200KB default
    }
}