using System;
using System.Collections.Generic;

namespace OneDrive.CloudCore.Common
{
    public interface ICloudPhoto : ICloudFile
    {
        int TagsCount { get; }
        bool TagsEnabled { get; }
        bool IsEmbeddable { get; }
        string PictureLocation { get; }
        Dictionary<PhotoType, PhotoCard> Photos { get;}
        DateTime WhenTaken { get; }
        int Height { get; }
        int Width { get; }
        string PhotographyLocation { get; } //Longitude, Latitude?
        string CameraMake { get; }
        string CameraModel { get; }
        string FocalRatio { get; }
        float FocalLength { get; }
        int ExposureNumerator { get; }
        int ExposureDenominator { get; }

    }
}