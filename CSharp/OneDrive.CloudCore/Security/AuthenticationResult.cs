using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OneDrive.CloudCore.Security
{
    /// <summary>
    /// Denotes the result of an authentication process.
    /// </summary>
    public enum AuthenticationResult : byte
    {
        /// <summary>
        /// The authentication was successful.
        /// </summary>
        Success,

        /// <summary>
        /// The authentication was failed due to some reason.
        /// </summary>
        Failed,

        /// <summary>
        /// The authentication process was cancelled by the user.
        /// </summary>
        Cancelled,

        
        /// <summary>
        /// The authentication result was unknow. This is a wierd and should not be the case for any developer.
        /// </summary>
        Unknown

    }
}
