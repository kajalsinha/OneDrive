using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OneDrive.CloudCore.Common;

namespace OneDrive.CloudCore.SkyDrive
{
    public class SkyDriveFolder : SkyDriveObject, ICloudFolder
    {
        private IList<ICloudObject> _children;
        public SkyDriveFolder(ICloudObject parent, IDictionary<string, object> objectDictionary, IEnumerable<object> children) : base(parent, objectDictionary)
        {
            Children = new List<ICloudObject>();
            if (Dictionary.type == "folder" || Dictionary.type == "album")
            {
                this.ChildObjectCountOnTheCloud = Dictionary.count ?? 0;
            }

            if (children != null)
            {
                PopulateChildren(children);
            }
        }

        /// <summary>
        /// Returns a list of Child objects in case the current object is a folder. The list of child objects will contain meta data aka properties but not the content (me/SkyDrive/files) a content will be downloaded on demand using a separate approach.
        /// </summary>
        public IList<ICloudObject> Children
        {
            get { return _children; }
            private set { _children = value; }
        }


        /// <summary>
        /// Returns the number of items inside this object if a folder.
        /// </summary>
        public int ChildObjectCountOnTheCloud { get; private set; }





        private void PopulateChildren(IEnumerable<object> children)
        {
            foreach (dynamic child in children)
            {
                SkyDriveObject skyDriveObject = null;
                if (child.type == "folder")
                {
                    skyDriveObject = new SkyDriveFolder(this, child, null);
                }
                else if (child.type == "file")
                {
                    skyDriveObject = new SkyDriveFile(this, child);
                }
                else if (child.type == "photo")
                {
                    skyDriveObject = new SkyDrivePhoto(this, child);
                }
                else if (child.type == "album")
                {
                    skyDriveObject = new SkyDriveAlbum(this, child, null);
                }
                else if (child.type == "audio")
                {
                    skyDriveObject = new SkyDriveAudio(this, child); 
                }
                if(skyDriveObject != null)
                this._children.Add(skyDriveObject);
            }
        }

        public override async Task<IDownloadToken> DownloadAsync()
        {
            throw new NotImplementedException();
        }
    }
}
