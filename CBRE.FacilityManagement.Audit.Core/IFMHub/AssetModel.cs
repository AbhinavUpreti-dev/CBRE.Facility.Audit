using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBRE.FacilityManagement.Audit.Core.IFMHub
{
    public class AssetModel
    {
        public string AssetDescription { get; set; }

        public string AssetManufacturer { get; set; }

        public string DateInstalled { get; set; }

        public string AssetModelType { get; set; }
    }
}
