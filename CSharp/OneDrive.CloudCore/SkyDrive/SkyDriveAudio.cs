using System.Collections.Generic;
using OneDrive.CloudCore.Common;

namespace OneDrive.CloudCore.SkyDrive
{
    public class SkyDriveAudio : SkyDriveFile, ICloudAudio
    {
        public SkyDriveAudio(ICloudObject parent, IDictionary<string, object> objectDictionary) : base(parent, objectDictionary)
        {
            Title = Dictionary.title;
            Artist = Dictionary.artist;
            Album = Dictionary.album;
            AlbumArtist = Dictionary.album_artist;
            Genre = Dictionary.genre;
            Duration = Dictionary.duration;
            Picture = Dictionary.picture;
        }

        public string Title { get; private set; }
        public string Artist { get; private set; }
        public string Album { get; private set; }
        public string AlbumArtist { get; private set; }
        public string Genre { get; private set; }
        public int Duration { get; private set; }
        public string Picture { get; private set; }
    }
}
