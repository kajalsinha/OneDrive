
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OneDrive.CloudCore.Common;

namespace OneDrive.CloudCore.SkyDrive
{
    public class SkyDriveFile : SkyDriveObject, ICloudFile
    {

        public SkyDriveFile(ICloudObject parent, IDictionary<string, object> objectDictionary)
            : base(parent, objectDictionary)
        {
            this.DownloadSourceLocation = Dictionary.source;
        }

        /// <summary>
        /// The location from where the given cloud object can be downloaded.
        /// </summary>
        public string DownloadSourceLocation
        {
            get;
            protected set;
        }

        public override async Task<IDownloadToken> DownloadAsync()
        {
            throw new NotImplementedException();
        }
    }
}
