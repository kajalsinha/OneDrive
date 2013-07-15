using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OneDrive.CloudCore.Common;

namespace OneDrive.CloudCore.Drives
{
    /// <summary>
    /// Common point to provide cloud based security for any cloud drive.
    /// </summary>
    public interface ICloudSecurity
    {
        /// <summary>
        /// Login to the Cloud Storage Provider.
        /// </summary>
        /// <returns></returns>
        bool SignIn();

        /// <summary>
        /// Login to the Cloud Storage Provider.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        bool SignIn(string userName, string password, ICloudProvider provider);

        ///// <summary>
        ///// Logout from the cloud storage provider.
        ///// </summary>
        ///// <returns></returns>
        //bool SignOut();

        ///// <summary>
        ///// The security TOken of cloud storage provider.
        ///// </summary>
        //ISecurityToken SecurityToken { get; }
    }
}
