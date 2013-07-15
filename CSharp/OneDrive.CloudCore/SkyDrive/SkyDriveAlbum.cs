using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OneDrive.CloudCore.Common;

namespace OneDrive.CloudCore.SkyDrive
{
    internal class SkyDriveAlbum : SkyDriveFolder
    {
        public SkyDriveAlbum(ICloudObject parent, IDictionary<string, object> objectDictionary, IEnumerable<object> children)
            : base(parent, objectDictionary, children)
        {
        }
       
        public override async Task<IDownloadToken> DownloadAsync()
        {
            throw new NotImplementedException();
        }
    }
}
