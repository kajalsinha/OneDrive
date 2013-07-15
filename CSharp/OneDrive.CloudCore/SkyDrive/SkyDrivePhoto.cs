using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OneDrive.CloudCore;
using OneDrive.CloudCore.Common;

namespace OneDrive.CloudCore.SkyDrive
{
    sealed class SkyDrivePhoto : SkyDriveFile, ICloudPhoto
    {
        public SkyDrivePhoto(ICloudFolder parent, IDictionary<string, object> objectDictionary)
            : base(parent, objectDictionary)
        {
            this.TagsCount = Dictionary.tags_count;
            this.TagsEnabled = Dictionary.tags_enabled;
            this.IsEmbeddable = Dictionary.is_embeddable;
            this.PictureLocation = Dictionary.picture;
            this.WhenTaken = DynamicExtension.ToDateTime(Dictionary.when_taken);
            this.Height = Dictionary.height;
            this.Width = Dictionary.width;
            this.CameraMake = Dictionary.camera_make;
            this.CameraModel = Dictionary.camera_model;
            this.FocalRatio = Dictionary.focal_ration;
            this.FocalLength = Dictionary.focal_length;
            this.ExposureNumerator = Dictionary.exposure_numerator;
            this.ExposureDenominator = Dictionary.exposure_denominator;
            this.PhotographyLocation = Dictionary.location ?? "Location Unavailable.";
            this.Photos = new Dictionary<PhotoType, PhotoCard>(Dictionary.images.Count);
            foreach (dynamic photo in Dictionary.images)
            {
                var card = new PhotoCard();
                card.Height = photo.height;
                card.Width = photo.width;
                card.PhotoType = (PhotoType) Enum.Parse(typeof (PhotoType), photo.type, true);
                card.DownloadSourceLocation = photo.source;
                if (this.Photos.ContainsKey(card.PhotoType))
                    this.Photos.Remove(card.PhotoType);

                this.Photos.Add(card.PhotoType, card);
            }


        }


        public int TagsCount { get; private set; }
        public bool TagsEnabled { get; private set; }
        public bool IsEmbeddable { get; private set; }
        public string PictureLocation { get; private set; }
        public Dictionary<PhotoType, PhotoCard> Photos { get; private set; }
        public DateTime WhenTaken { get; private set; }
        public int Height { get; private set; }
        public int Width { get; private set; }
        public string PhotographyLocation { get; private set; }
        public string CameraMake { get; private set; }
        public string CameraModel { get; private set; }
        public string FocalRatio { get; private set; }
        public float FocalLength { get; private set; }
        public int ExposureNumerator { get; private set; }
        public int ExposureDenominator { get; private set; }
    }
}
