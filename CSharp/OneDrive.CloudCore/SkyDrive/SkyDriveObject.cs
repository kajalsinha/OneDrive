using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OneDrive.CloudCore.Common;
using OneDrive.CloudCore.Providers;
using System.Linq;
using OneDrive.CloudCore;
namespace OneDrive.CloudCore.SkyDrive
{
    /// <summary>
    /// Cloud object implementation for SkyDrive objects.
    /// </summary>
    public abstract class SkyDriveObject : ICloudObject
    {
        protected dynamic Dictionary;
        private readonly DateTime _objectCachedOn;
        protected SkyDriveObject(ICloudObject parent, IDictionary<string, object> objectDictionary)
        {
            _objectCachedOn = DateTime.Now;
            this.Parent = parent;
            // TODO: Complete member initialization
            this.Dictionary = objectDictionary;

            this.Id = Dictionary.id;
            this.Name = Dictionary.name;
            CreatedBy = Dictionary.from.name;
            CreatedByUserId = Dictionary.from.id;
            this.Size = Dictionary.size ?? 0;
            this.Description = Dictionary.description;
            this.FullPath = Dictionary.link;
            this.CreatedOn = DynamicExtension.ToDateTime(Dictionary.created_time);

            this.UpdatedOn = DynamicExtension.ToDateTime(Dictionary.updated_time);

            this.SharedWith = GetSharedWith(Dictionary.shared_with);
            this.UploadLocation = Dictionary.upload_location;

            this.CommentsCount = Dictionary.comments_count;
            this.CommentsEnabled = Dictionary.comments_enabled;
        }

        private string[] GetSharedWith(IDictionary<string, object> sharedWith)
        {
            return new string[]{sharedWith["access"].ToString()};
            //TODO: Complete this by adding other user information as well.
        }

        /// <summary>
        /// The unique id which can be used to identify the cloud object individually.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The name of ICloudObject
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// User Name who created this Cloud Object.
        /// </summary>
        public string CreatedBy { get; private set; }

        /// <summary>
        /// User Id of individual who created this Cloud Object.
        /// </summary>
        public string CreatedByUserId { get; private set; }

        /// <summary>
        /// Parent of ICloudObject
        /// </summary>
        public ICloudObject Parent { get; set; }

        /// <summary>
        /// Description text for this Cloud Object.
        /// </summary>
        public string Description { get; private set; }


        /// <summary>
        /// Returns the full path of this Cloud Object. Web path in case of an online folder.
        /// </summary>
        public string FullPath { get; private set; }

        /// <summary>
        /// Date and Time when this Cloud Object was created.
        /// </summary>
        public DateTime CreatedOn { get; private set; }

        /// <summary>
        /// Date and Time when this Cloud Object was last accessed.
        /// </summary>
        public DateTime UpdatedOn { get; private set; }

        /// <summary>
        /// Total number of comments for this Cloud Object.
        /// </summary>
        public int CommentsCount { get; private set; }

        /// <summary>
        /// Returns true of the comments are enabled for this Cloud Object.
        /// </summary>
        public bool CommentsEnabled { get; private set; }

        /// <summary>
        /// Returns if this folder is shared with the users including "Just me."
        /// </summary>
        public string[] SharedWith { get; private set; }

        /// <summary>
        /// The location where the files/folder related to this folder can be uploaded.
        /// </summary>
        public string UploadLocation { get; set; }

        /// <summary>
        /// Method to download this ICloudObject.
        /// </summary>
        /// <returns>Running task which is downloading the ICloudObject</returns>
        public abstract Task<IDownloadToken> DownloadAsync();
        
        /// <summary>
        /// Saves the ICloudObject to the given destination. 
        /// </summary>
        /// <param name="destination"></param>
        /// <returns></returns>
        public Task<CloudResult> Save(string destination)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes the given ICloudObject
        /// </summary>
        /// <returns></returns>
        public bool Delete()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Renames the current ICloudObject to a newName.
        /// </summary>
        /// <param name="newName">The newName of ICloudObject.</param>
        /// <returns>True if rename is successful else false.</returns>
        public bool Rename(string newName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Indicates if the given ICloudObject is valid as it may exists on multiple locations.
        /// </summary>
        public bool IsValid {
            get
            {
                TimeSpan difference = DateTime.Now - this._objectCachedOn;
                if (difference.TotalSeconds > OneDriveSettings.MAX_RETENTION_SECONDS)
                    return false;

                return true;
            }
        }

        /// <summary>
        /// Returns the permissions associated with the current ICloudObject.
        /// </summary>
        /// <returns></returns>
        public CloudPermission GetPermissions()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns true if this ICloudObject is available offline.
        /// </summary>
        public bool IsAvailableOffline { get; private set; }

        /// <summary>
        /// Returns true if this ICloudObject is partitioned and exists on multiple locations as splitted files or folders.
        /// </summary>
        public bool IsPartitioned { get; private set; }

        /// <summary>
        /// Brings a copy of this ICloudObject as offline.
        /// </summary>
        /// <returns></returns>
        public Task<CloudResult> TakeOffline()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// A path which is virtual but something which is supported by OneDrive. e.g. OneDrive://SkyDrive/me/abc/def and using a PathTranslator this could translate to something like folder.72981f91d7bb629c in case of SkyDrive
        /// </summary>
        public string GetOneDriveFullPath()
        {
          
                ICloudObject obj = this;
                var paths = new List<string>();
                do
                {
                    paths.Add(obj.Name);
                    obj = obj.Parent;
                    if (obj == null)
                        break;
                } while (true);

                paths.Reverse();
                return OneDriveSettings.ONE_DRIVE_PROTOCOL + string.Join(OneDriveSettings.PATH_SEPERATOR.ToString(), paths);
        }

        /// <summary>
        /// If a OneDrive Object shared and partitioned across multiple cloud providers then the partition information so that the details can be retrieved.
        /// </summary>
        public IList<IObjectPartition> Partitions { get; private set; }


        /// <summary>
        /// The combined size of current ICloudObject in bytes.
        /// </summary>
        public long Size { get; private set; }

  
    }
}