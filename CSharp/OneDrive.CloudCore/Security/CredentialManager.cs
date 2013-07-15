using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Live;
using OneDrive.CloudCore.Common;
using OneDrive.CloudCore.Providers;

namespace OneDrive.CloudCore.Security
{
    /// <summary>
    /// The class which manages all security features within various drives.
    /// </summary>
    public static class CredentialManager
    {
        private static readonly Dictionary<SupportedDrive, List<ICloudProvider>> DriveProviderCollection =
            new Dictionary<SupportedDrive, List<ICloudProvider>>();

        /// <summary>
        /// Returns the list of providers associated with the drive. This means, Google drive can have multiple providers and one provider per account resulting in supporting multiple accounts on the same provider.
        /// </summary>
        /// <param name="drive"></param>
        /// <returns></returns>
        public static ICloudProvider[] GetProvider(SupportedDrive drive)
        {
            if (DriveProviderCollection.ContainsKey(drive))
            {
                return DriveProviderCollection[drive].ToArray();
            }
            return null;
        }

        /// <summary>
        /// Registers a provider for a particular drive and user account. Reregistration of provider throws exception of type ProviderRegistrationException.
        /// </summary>
        /// <param name="newProvider"></param>
        private static void RegisterProvider(ICloudProvider newProvider)
        {
            if (!DriveProviderCollection.ContainsKey(newProvider.Drive))
                DriveProviderCollection.Add(newProvider.Drive, new List<ICloudProvider>());

            List<ICloudProvider> providers = DriveProviderCollection[newProvider.Drive];
            if (providers.Find(provider => provider.UserName == newProvider.UserName) != null)
            {
                string message =
                    string.Format(
                        "The provider with username {0} is already registered with {1}. Please register with a different user id. Multiple accounts from same cloud provider is supported.",
                        newProvider.UserName, newProvider.Drive);
                throw new ProviderRegistrationException(message);
            }
            providers.Add(newProvider);
        }

        /// <summary>
        /// Verifies if the drive is authenticated.
        /// </summary>
        /// <param name="drive"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static bool IsAuthenticated(SupportedDrive drive, string userName = null)
        {
            if (string.IsNullOrWhiteSpace(userName))
                return DriveProviderCollection.ContainsKey(drive);
            if (DriveProviderCollection.ContainsKey(drive))
            {
                if (DriveProviderCollection[drive].Any(provider => provider.UserName == userName))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Authenticates a particular drive with the given user name and password. Please not that this method may deprecate in future considering each account can provide their own mechanism of authentication like skydrive does not allows to capture user credentials and it provides its own interface OAuth 2.0
        /// </summary>
        /// <param name="drive"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool Authenticate(SupportedDrive drive, string userName, string password)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns all of the providers in the collection.
        /// </summary>
        /// <returns></returns>
        public static Dictionary<SupportedDrive, List<ICloudProvider>> GetAllProviders()
        {
            return DriveProviderCollection; //TODO: This is dangerous. Return the readonly collection.
            //return driveProviderCollection;
        }

        /// <summary>
        /// Returns the provider for a particular drive and fixed user name.
        /// </summary>
        /// <param name="fromDrive"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static ICloudProvider GetProvider(SupportedDrive fromDrive, string userName)
        {
            if (DriveProviderCollection.ContainsKey(fromDrive))
            {
                return DriveProviderCollection[fromDrive].FirstOrDefault(provider => provider.UserName == userName);
            }
            return null; //Provider not registered.
        }

        /// <summary>
        /// Authenticates the given drive. If one account is already authenticated then the method forces authentication of the drive on a different account. This also registers a provider for the supported drive.
        /// </summary>
        /// <param name="drive"></param>
        public static async Task<AuthenticationResult> AuthenticateAsync(SupportedDrive drive)
        {
            CloudAuthenticator authenticator = CloudAuthenticator.GetAuthenticator(drive);
            AuthenticationResult result = await authenticator.AuthenticateAsync();
            if (result == AuthenticationResult.Success)
            {
                ICloudProvider provider = null;
                switch (drive)
                {
                    case SupportedDrive.SkyDrive:
                        provider = new SkyDriveProvider((LiveConnectSession) authenticator.GetSession());
                        break;
                    //case SupportedDrive.DropBox:
                    //    provider = new SkyDriveProvider((LiveConnectSession) authenticator.GetSession());
                    //    break;
                }
                RegisterProvider(provider);
            }
            return result;
        }
    }
}
