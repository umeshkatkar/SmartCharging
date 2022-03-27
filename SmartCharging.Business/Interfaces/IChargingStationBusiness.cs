using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartCharging.Business
{
    public interface IChargingStationBusiness
    {
        Task<IEnumerable<ChargingStation>> GetChargingStations();
        Task<ChargingStation> GetChargingStationById(int groupId);
        Task<ChargingStation> CreateChargingStation(ChargingStation  chargingStation);
        Task<ChargingStation> UpdateChargingStation(ChargingStation  chargingStation);
        Task<bool> DeleteChargingStationById(int id);
    }
}
