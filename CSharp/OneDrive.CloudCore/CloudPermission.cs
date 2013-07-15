using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OneDrive.CloudCore
{
    /// <summary>
    /// Indicates the permissions on the cloud based object.
    /// </summary>
    [Flags]
    public enum CloudPermission
    {
        /// <summary>
        /// Permission to read/view.
        /// </summary>
        Read = 1,

        /// <summary>
        /// Permission to write.
        /// </summary>
        Write = 2,

        /// <summary>
        /// Permission to delete.
        /// </summary>
        Delete = 4
    }
}
