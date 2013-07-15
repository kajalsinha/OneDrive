using System;
using System.Threading.Tasks;
using Microsoft.Live;
using OneDrive.CloudCore.Common;
using OneDrive.CloudCore.Security;
using OneDrive.CloudCore.SkyDrive;
using System.Collections.Generic;
using System.Linq;
using Windows.Foundation;
using Windows.Networking.BackgroundTransfer;
using Windows.Storage;
using Windows.System;

namespace OneDrive.CloudCore.Providers
{
    /// <summary>
    /// Provider class to access SkyDrive contents.
    /// </summary>
    internal sealed class SkyDriveProvider : CloudProvider
    {
        private static readonly string SkyDrivePrefix = OneDriveSettings.ONE_DRIVE_PROTOCOL + "SkyDrive" + OneDriveSettings.PATH_SEPERATOR;
        private LiveConnectClient _liveClient;
        private const string RootDirectory = "me/skydrive";
        private  string _absoluteRootDirectory; //USER_ID/skydrive

        private Dictionary<string, ICloudObject> _driveMapping = new Dictionary<string, ICloudObject>();

        private ICloudFolder _rootCloudObject = null;
      
        /// <summary>
        ///  Provider class to access SkyDrive contents.
        /// </summary>
        /// <param name="session">A live connect session which will be used to retrieve cloud content.</param>
        public SkyDriveProvider(LiveConnectSession session)
            : base("SkyDrive", SupportedDrive.SkyDrive, RootDirectory)
        {

            Init(session);

        }

        private async void Init(LiveConnectSession session)
        {
            if (session == null)
                throw new NullReferenceException("The 'session' parameter cannot be null");
            _liveClient = new LiveConnectClient(session);
            _absoluteRootDirectory = RootDirectory;

            LiveOperationResult operationResult = await _liveClient.GetAsync("me"); //me, or user id will be taken cared of by the session object which will specifically connect to a particular user. so no need to worry about supporting multiple user accounts here.
            dynamic result = operationResult.Result;
            this.UserName = result.name;

            _rootCloudObject = await GetRootDirectoryAsync();


            //_rootCloudObject = new SkyDriveObject(operationResult.Result.Result);
        }

        private ICloudFolder FindParent(ICloudFolder searchFrom, string childId)
        {
            foreach (ICloudObject obj in searchFrom.Children)
            {
                if (obj.Id == childId)
                    return searchFrom;
                else
                {
                    if (obj is ICloudFolder)
                    {
                        ICloudFolder parent = FindParent((ICloudFolder)obj, childId);
                        if (parent != null)
                            return parent;
                    }
                }

            }
            return null;
        }

        private async Task<ICloudObject> _GetCloudObjectFromSkyDriveObjectIdAsync(string skyDriveObjectId, bool fetchChildren = true)
        {
            ICloudObject cloudObject = null;
            //TODO: Validate if session requires renewal. Skydrive does not supports unique paths with sub directories.
            //do some recursive calls to construct sub directory paths with some caching so that the path can be optimized.
            //do make sure that the path exists aka is not deleted or moved.
            LiveOperationResult operationResult = await _liveClient.GetAsync(skyDriveObjectId);

            dynamic result = operationResult.Result;


            ICloudFolder parent = FindParent(_rootCloudObject, result.id);

            if (result.type == "folder")
            {
                if (fetchChildren)
                {
                    LiveOperationResult childrenResult = await _liveClient.GetAsync(skyDriveObjectId + "/files");
                    cloudObject = new SkyDriveFolder(parent, result, ((dynamic)childrenResult.Result).data);
                }
                else
                {
                    cloudObject = new SkyDriveFolder(parent, result, null);
                }
            }
            else if (result.type == "album")
            {
                if (fetchChildren)
                {
                    LiveOperationResult childrenResult = await _liveClient.GetAsync(skyDriveObjectId + "/files");
                    cloudObject = new SkyDriveAlbum(parent, result, ((dynamic)childrenResult.Result).data);
                }
                else
                {
                    cloudObject = new SkyDriveAlbum(parent, result, null);
                }
            }
            else if (result.type == "file")
            {
                cloudObject = new SkyDriveFile(parent, result);
            }
            else if (result.type == "photo")
            {
                cloudObject = new SkyDrivePhoto(parent, result);
            }

            //Replace the oldChildRef with the new ref. We need to do it everytime to keep the offline cache updated.
            if (cloudObject != null && parent != null)
            {
                ICloudObject oldChildRef = parent.Children.FirstOrDefault(obj => obj.Id == cloudObject.Id);
                if (oldChildRef != null)
                {
                    int index = parent.Children.IndexOf(oldChildRef);
                    parent.Children.Remove(oldChildRef);
                    parent.Children.Insert(index, cloudObject);

                }
            }
            return cloudObject;
        }

        public override async Task<ICloudObject> GetCloudObjectAsync(string uniqueOneDrivePath, bool fetchChildren = true)
        {
            if (IsRootDirectory(uniqueOneDrivePath))
                return await GetRootDirectoryAsync();

            string translatedPath = await GetTranslatedPathFromAsync(uniqueOneDrivePath);

            if (string.IsNullOrWhiteSpace(translatedPath))
                throw new OneDriveOutOfSyncException("The SkyDrive path is out of sync.");

            return await _GetCloudObjectFromSkyDriveObjectIdAsync(translatedPath, fetchChildren);
        }

        private bool IsRootDirectory(string uniqueOneDrivePath)
        {
            return uniqueOneDrivePath + OneDriveSettings.PATH_SEPERATOR == SkyDrivePrefix;        
        }

        public override async Task<ICloudFolder> GetRootDirectoryAsync()
        {
           
            dynamic result =  await _liveClient.GetAsync(RootDirectory);
            dynamic childrenResult = await _liveClient.GetAsync(RootDirectory + "/files");
            ICloudFolder cloudObject = new SkyDriveFolder(null, result.Result, childrenResult.Result.data);

            return cloudObject;
        }


        /// <summary>
        /// Returns the translated path for the given unique onedrive path. e.g. OneDrive://SkyDrive/ can be translated to me/SkyDrive or folder.72981f91d7bb629c.
        /// </summary>
        /// <param name="uniqueOneDrivePath"></param>
        /// <returns></returns>
        public override async Task<string> GetTranslatedPathFromAsync(string uniqueOneDrivePath)
        {
            string[] objectNames = uniqueOneDrivePath.Substring(SkyDrivePrefix.Length).Split(OneDriveSettings.PATH_SEPERATOR);

            if (objectNames == null || objectNames.Length == 0)
                throw new OneDriveOutOfSyncException("OneDrive is out of sync.", OneDriveErrorCode.OneDriveIsOutOfSync);

            if (_rootCloudObject.ChildObjectCountOnTheCloud == 0)
            {
                _rootCloudObject = await GetRootDirectoryAsync();
                if (_rootCloudObject.ChildObjectCountOnTheCloud == 0)
                {
                    throw new OneDriveOutOfSyncException("OneDrive is out of sync.", OneDriveErrorCode.OneDriveIsOutOfSync);
                }
            }
            return await TriangulateObjectIdFromPathAsync(objectNames, _rootCloudObject, 0);
        }

        private async Task<string> TriangulateObjectIdFromPathAsync(string[] objectNames, ICloudFolder searchFromFolder, ushort currentIndex)
        {
            if (objectNames == null || objectNames.Length < currentIndex)
                return string.Empty;

            ICloudObject objectFound = null;

            string searchFor = objectNames[currentIndex];

            //verify if searchFromFolder and objectNames are in sync. We should have something to search from in the following check.
            if (searchFromFolder.Children.Count == 0 && (searchFromFolder.ChildObjectCountOnTheCloud > 0 || currentIndex < objectNames.Length - 1))
            {
                searchFromFolder = (ICloudFolder)(await _GetCloudObjectFromSkyDriveObjectIdAsync(searchFromFolder.Id, true));
                if (searchFromFolder.Children.Count == 0)
                    throw new OneDriveOutOfSyncException("The Cloud Object cannot be found : " + searchFor, OneDriveErrorCode.CloudObjectNotFound);
            }

            //Check if the Cached collection tree has got the object information.
            foreach (ICloudObject obj in searchFromFolder.Children)
            {
                if (obj.Name == searchFor)
                {
                    objectFound = obj;
                    if (currentIndex < objectNames.Length - 1) //Search for more recursively! objectFound should be a folder.
                    {
                        var folder = objectFound as ICloudFolder;
                        if(folder != null)
                            return await TriangulateObjectIdFromPathAsync(objectNames, folder, ++currentIndex);
                        throw new OneDriveOutOfSyncException("The Cloud Object is no more a folder. It may have changed recently : " + searchFor, OneDriveErrorCode.CloudObjectTypeChanged);
                    }
                    break; //Object found but verify!
                }
            }
            if (currentIndex == objectNames.Length - 1)
            {
                if (objectFound != null)
                {
                    if (objectFound.IsValid)
                        return objectFound.Id;
                    searchFromFolder = (ICloudFolder)await _GetCloudObjectFromSkyDriveObjectIdAsync(searchFromFolder.Id);
                    if (searchFromFolder.ChildObjectCountOnTheCloud > 0)
                    {
                        //Verify by unique id that the object has not changed.
                        ICloudObject realObject = searchFromFolder.Children.First(child => child.Id == objectFound.Id);
                        return realObject.Id;
                    }
                    throw new OneDriveOutOfSyncException("The Cloud Object cannot be found : " + searchFor, OneDriveErrorCode.CloudObjectNotFound);
                }
                throw new OneDriveOutOfSyncException("The Cloud Object cannot be found : " + searchFor, OneDriveErrorCode.CloudObjectNotFound);
            }
            throw new OneDriveOutOfSyncException("The Cloud Object is no more a folder. It may have changed recently : " + searchFor, OneDriveErrorCode.CloudObjectTypeChanged);
        }

        public override async Task<bool> OpenFileAsync(ICloudFile fileToDownload, bool forceDownload = false, bool showOpenWith = false)
        {
            var sourceObject = new Uri(fileToDownload.DownloadSourceLocation);
            IStorageFile file = null;
            if (fileToDownload.IsQualifiedAsSmallDownload() && !forceDownload)
            {
                file = await StorageFile.CreateStreamedFileFromUriAsync(fileToDownload.Name,
                                                                                     sourceObject,
                                                                                     null);
            }
            else
            {
                //TODO: Add feature to send as attachment. :D
                //TODO: Verify the code waits till the background download is complete before launching the file.
                var downloader = new BackgroundDownloader();
                file = await DownloadManager.DownloadStorageFileAsync(fileToDownload);
                DownloadOperation operation = downloader.CreateDownload(sourceObject, file);
                var result = await operation.StartAsync();
                file = result.ResultFile;
            }

            //SAD: the following did not work somehow
            //var result = await _liveClient.BackgroundDownloadAsync(objectToDownload.DownloadSourceLocation,);
            //var options = new LauncherOptions();
            //options.DisplayApplicationPicker = true;
            //var result2 = await _liveClient.GetAsync(objectToDownload.Id);

            bool launchResult;
            //Consider LaunchUriAsync(...) for else part?
            if (showOpenWith)
            {
                var options = new LauncherOptions {DisplayApplicationPicker = true};
                launchResult = await Launcher.LaunchFileAsync(file, options);
            }
            else
                launchResult = await Launcher.LaunchFileAsync(file);

            return launchResult;
        }
    }
}