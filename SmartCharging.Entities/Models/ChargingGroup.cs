using System;
using System.Collections.Generic;

namespace SmartCharging
{
    public partial class ChargingGroup
    {
        public int GroupIdentifier { get; set; }
        public string GroupName { get; set; }
        public int CapacityInAmps { get; set; }
        public ICollection<ChargingStation> ChargingStations { get; set; }
    }
}
