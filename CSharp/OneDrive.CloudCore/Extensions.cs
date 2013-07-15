using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneDrive.CloudCore
{
    static class DynamicExtension
    {
        public static DateTime ToDateTime(object input)
        {
            return (input == null) ? new DateTime() : Convert.ToDateTime(input);
        }
    }
}
