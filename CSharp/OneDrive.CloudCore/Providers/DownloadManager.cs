using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OneDrive.CloudCore.Common;
using Windows.Storage;

namespace OneDrive.CloudCore.Providers
{

    public class BackgroundDownloadPackage
    {
        public BackgroundDownloadPackage(ICloudObject cloudObject, IStorageFile storageFile)
        {
            this.CloudObject = cloudObject;
            this.StorageFile = storageFile;

        }

        public IStorageFile StorageFile { get; private set; }

        public ICloudObject CloudObject { get; private set; }

        public DownloadStatus DownloadStatus { get; private set; }
    }

    public interface IDownloadManager
    {
        Task<bool> CancelAllDownloadsAsync();
        Task<bool> CancelDownload(ICloudObject cloudObject);
        Task<DownloadStatus> GetDownloadStatus(ICloudObject cloudObject);
        Task<bool> IsObjectInDownloadQueue(ICloudObject cloudObject);
        Task<List<BackgroundDownloadPackage>> GetAllDownloads();

        Task<BackgroundDownloadPackage> CreateDownloadPackage(Uri uri, DestinationStorage destination,
                                                              string fileNameAtDestination);

        Task<IStorageFile> CreateStorageFileAsync(ICloudFile fileToDownload);
        Task<IStorageFile> DownloadStorageFileAsync(ICloudFile fileToDownload);
    }

    public enum DestinationStorage : byte
    {
        LocalAppStorage,
        DocumentsLibrary,
        MusicLibrary,
        PicturesLibrary,
        VideosLibrary
    }

    public enum DownloadStatus
    {
        Starting,
        Paused,
        Downloading,
        Failed,
        Cancelled,
        Completed
    }

    public class DownloadManager : IDownloadManager
    {
        public List<ICloudObject> AllDownloads = new List<ICloudObject>();
        public DownloadManager()
        {
            
        }

        public Task<bool> CancelAllDownloadsAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> CancelDownload(ICloudObject cloudObject)
        {
            throw new System.NotImplementedException();
        }

        public Task<DownloadStatus> GetDownloadStatus(ICloudObject cloudObject)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> IsObjectInDownloadQueue(ICloudObject cloudObject)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<BackgroundDownloadPackage>> GetAllDownloads()
        {
            throw new System.NotImplementedException();
        }

        public Task<BackgroundDownloadPackage> CreateDownloadPackage(Uri uri, DestinationStorage destination, string fileNameAtDestination)
        {
            throw new NotImplementedException();
        }

        public Task<IStorageFile> CreateStorageFileAsync(ICloudFile fileToDownload)
        {
            throw new NotImplementedException();
        }

        public Task<IStorageFile> DownloadStorageFileAsync(ICloudFile fileToDownload)
        {
            throw new NotImplementedException();
        }
    }
}