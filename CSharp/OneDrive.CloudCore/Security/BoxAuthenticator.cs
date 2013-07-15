namespace OneDrive.CloudCore.Security
{
    public class BoxAuthenticator : CloudAuthenticator
    {
        internal override object GetSession()
        {
            throw new System.NotImplementedException();
        }

        public override System.Threading.Tasks.Task<AuthenticationResult> AuthenticateAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}