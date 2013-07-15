using OneDrive.CloudCore.Common;
using OneDrive.CloudCore.Drives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneDrive.CloudCore
{
    /// <summary>
    /// Represents a partition of a partitioned object in ICloudObject
    /// </summary>
    public interface IObjectPartition
    {
        int Index { get; }
        ICloudObject CloudObject { get; }
        int PartitionSize { get; }
        ICloudDrive Location { get; }
    }
}
