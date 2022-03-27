using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartCharging.Business
{
    public interface IChargingConnectorBusiness
    {
        Task<IEnumerable<ChargingConnector>> GetChargingConnectors();
        Task<ChargingConnector> GetChargingConnectorById(int connectorId);
        Task<ChargingConnector> CreateChargingConnector(ChargingConnector chargingConnector);
        Task<ChargingConnector> UpdateChargingConnector(ChargingConnector chargingConnector);
        Task<bool> DeleteChargingConnectorById(int id);
    }
}
