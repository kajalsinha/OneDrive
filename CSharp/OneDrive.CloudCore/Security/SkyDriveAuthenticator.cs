using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Live;

namespace OneDrive.CloudCore.Security
{
    public class SkyDriveAuthenticator : CloudAuthenticator
    {
        private readonly LiveAuthClient _authClient = new LiveAuthClient();
        private LiveLoginResult _result = null;
        public SkyDriveAuthenticator()
        {
            AuthorizationScopes.Add(AuthorizationScope.Login, new[] { "wl.signin" });
            AuthorizationScopes.Add(AuthorizationScope.Identity, new[] { "wl.basic" });
            AuthorizationScopes.Add(AuthorizationScope.DriveReadOnly, new[] { "wl.skydrive" });
            AuthorizationScopes.Add(AuthorizationScope.PhotosAll, new[] { "wl.contacts_photos" });
            AuthorizationScopes.Add(AuthorizationScope.ContactsAll, new[] { "wl.contacts_create" });
            AuthorizationScopes.Add(AuthorizationScope.Photos, new[] { "wl.photos" });
            AuthorizationScopes.Add(AuthorizationScope.DriveAll, new[] { "wl.skydrive_update" });

            //Add in-app purchase scopes here.
            GetAllAuthorizationScopes();
            //base.AuthorizationScopes.Add(AuthorizationScope.DriveAll, new[] { "wl.skydrive_update" });
        }

        public override async Task<AuthenticationResult> AuthenticateAsync()
        {
            var result = await _authClient.LoginAsync(this.AuthorizationScopesList.ToArray());
            if (result != null && result.Status == LiveConnectSessionStatus.Connected)
            {
                _result = result;
                return AuthenticationResult.Success;
            }
            if (result.Status == LiveConnectSessionStatus.Unknown)
                return AuthenticationResult.Unknown;
            else if (result.Status == LiveConnectSessionStatus.NotConnected)
                return AuthenticationResult.Failed;

            return AuthenticationResult.Unknown;
            //A really bad status to talk about!
            //var client = new LiveConnectClient(result.Session);
            //dynamic result2 = await client.GetAsync("me/skydrive/files");

        }

        internal override object GetSession()
        {
            return _result.Session;
        }
    }

    /// <summary>
    /// Defines the generic scope of permissions on a given cloud drive. E.g. *All means read + write permission for the give scope.
    /// </summary>
    public enum AuthorizationScope : byte
    {
        Login,
        Identity,
        DriveReadOnly,
        DriveAll,
        Contacts,
        Calendar,
        Photos,
        Birthday,
        BirthdayAll,
        ContactsAll,
        CalendarAll,
        PhotosAll,
        OfflineAccess
    }
}