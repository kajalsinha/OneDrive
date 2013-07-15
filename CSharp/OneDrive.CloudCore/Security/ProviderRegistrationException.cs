using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OneDrive.CloudCore.Security
{
    class ProviderRegistrationException : Exception
    {
        public ProviderRegistrationException(string message) : base(message) { }
    }
}
