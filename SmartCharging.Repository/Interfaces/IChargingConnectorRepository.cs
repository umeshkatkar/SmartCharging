using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartCharging.Repository
{
    public interface IChargingConnectorRepository
    {
        Task<IEnumerable<ChargingConnector>> GetChargingConnectors();
        Task<ChargingConnector> GetChargingConnectorById(int connectorId);
        Task<ChargingConnector> CreateChargingConnector(ChargingConnector chargingConnector);
        Task<ChargingConnector> UpdateChargingConnector(ChargingConnector chargingConnector);
        Task<bool> DeleteChargingConnectorById(int id);
        int GetConnectorCount(int stationId);
        bool CheckGroupsCapacity(int stationId, int requestedCurrent);

        }
}
