using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartCharging
{
    public partial class ChargingStation
    {
        public int StationIdentifier { get; set; }
        public string StationName { get; set; }
        public int GroupIdentifier { get; set; }
        [NotMapped]
        public ChargingGroup ChargingGroup { get; set; }
        public ICollection<ChargingConnector> ChargingConnectors { get; set; }
    }
}
