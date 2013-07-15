using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OneDrive.CloudCore
{
    /// <summary>
    /// The token which is returned 
    /// </summary>
    public interface IDownloadToken
    {
        bool CancelDownload();
        bool IsDownloadCancelled { get; }
        bool IsDownloadCompleted { get; }
        
    }
}
