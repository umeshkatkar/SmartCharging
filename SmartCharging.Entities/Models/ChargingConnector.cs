using System;
using System.Collections.Generic;

namespace SmartCharging
{
    public partial class ChargingConnector
    {
        public int ConnectorId { get; set; }
        public string ConnectorIdentifier { get; set; }
        public int MaxCurrent { get; set; }
        public int StationIdentifier { get; set; }
        public ChargingStation ChargingStation { get; set; }
    }
}
