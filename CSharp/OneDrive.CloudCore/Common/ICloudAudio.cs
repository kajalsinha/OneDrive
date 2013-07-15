namespace OneDrive.CloudCore.Common
{
    public interface ICloudAudio : ICloudFile
    {
        string Title { get; }
        string Artist { get; }
        string Album { get; }
        string AlbumArtist { get; }
        string Genre { get; }
        int Duration { get; }
        string Picture { get; }
    }

  
}
