using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OneDrive.CloudCore.Providers
{

    /// <summary>
    /// TODO: Write localization code builder.
    /// </summary>
    public enum OneDriveErrorCode : short
    {
        UnknownError = 0,
        UniquePathOutOfSync,
        OneDriveIsOutOfSync,
        CloudObjectNotFound,
        CloudObjectTypeChanged
    }



    /// <summary>
    /// Exception to be thrown when OneDrive faces any error or uncertainity.
    /// </summary>
    public sealed class OneDriveOutOfSyncException : Exception
    {
        public OneDriveErrorCode ErrorCode { get; private set; }

        /// <summary>
        /// TODO: This is half cooked! please look for name in the Message property.
        /// </summary>
        public string CloudObjectName { get; private set; }

        public OneDriveOutOfSyncException(string cloudObjectName, OneDriveErrorCode errorCode = OneDriveErrorCode.UnknownError)
            : base(DeriveMessage(errorCode, cloudObjectName))
        {
            ErrorCode = errorCode;          
        }

        private static string DeriveMessage(OneDriveErrorCode errorCode, string cloudObjectName)
        {//TODO: derive it to message + cloudobjectname;
            switch (errorCode)
            {
                case OneDriveErrorCode.UnknownError:
                default:
                    return cloudObjectName;
            }
        }
    }
}
