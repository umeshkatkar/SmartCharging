using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartCharging.Repository
{
    public interface IChargingStationRepository
    {
        Task<IEnumerable<ChargingStation>> GetChargingStations();
        Task<ChargingStation> GetChargingStationById(int stationId);
        Task<ChargingStation> CreateChargingStation(ChargingStation  chargingStation);
        Task<ChargingStation> UpdateChargingStation(ChargingStation  chargingStation);
        Task<bool> DeleteChargingStationById(int id);

    }
}
