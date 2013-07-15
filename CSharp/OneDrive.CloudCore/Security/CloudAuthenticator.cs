using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using OneDrive.CloudCore.Common;

namespace OneDrive.CloudCore.Security
{
    /// <summary>
    /// Base class for all type of authenticators for the given members in <typeparamref name="SupportedDrive"/>
    /// </summary>
    public abstract class CloudAuthenticator
    {
        protected readonly List<string> AuthorizationScopesList = new List<string>();
        protected readonly Dictionary<AuthorizationScope, IList<string>> AuthorizationScopes = new Dictionary<AuthorizationScope, IList<string>>(); 

        protected static readonly Dictionary<SupportedDrive, CloudAuthenticator> Authenticators = new Dictionary<SupportedDrive, CloudAuthenticator>(); 
        static CloudAuthenticator()
        {
            Authenticators.Add(SupportedDrive.SkyDrive,new SkyDriveAuthenticator());
            Authenticators.Add(SupportedDrive.DropBox, new DropBoxAuthenticator());
            Authenticators.Add(SupportedDrive.Box, new BoxAuthenticator());
            Authenticators.Add(SupportedDrive.GoogleDrive, new GoogleDriveAuthenticator());
            Authenticators.Add(SupportedDrive.LocalDrive, new LocalDriveAuthenticator());
            Authenticators.Add(SupportedDrive.NetworkDrive, new NetworkDriveAuthenticator());
            Authenticators.Add(SupportedDrive.ExternalDrive, new ExternalDriveAuthenticator());
        }

        /// <summary>
        /// Manages all authentication process for the given drive.
        /// </summary>
        /// <returns></returns>
        public abstract  Task<AuthenticationResult> AuthenticateAsync();

        protected string[] GetAllAuthorizationScopes()
        {
            if (AuthorizationScopesList.Count == 0 && AuthorizationScopes.Count > 0)
            {
                foreach (string[] values in AuthorizationScopes.Values)
                {
                    AuthorizationScopesList.AddRange(values);
                }
            }
            return AuthorizationScopesList.ToArray();
        }
        
        /// <summary>
        /// Returns the authenticator for the given SupportedDrive
        /// </summary>
        /// <param name="fromDrive"></param>
        /// <returns></returns>
        internal static CloudAuthenticator GetAuthenticator(SupportedDrive fromDrive)
        {
            if (Authenticators.ContainsKey(fromDrive))
            {
                return Authenticators[fromDrive];
            }
            return null;
        }

        internal abstract object GetSession();

    }
}