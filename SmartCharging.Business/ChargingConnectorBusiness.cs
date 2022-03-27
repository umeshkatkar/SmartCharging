using Microsoft.Extensions.Logging;
using SmartCharging.Repository;
using SmartCharging.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartCharging.Business
{
  public  class ChargingConnectorBusiness:IChargingConnectorBusiness
    {
        private readonly IChargingConnectorRepository _chargingConnectorRepository;
        private readonly ILogger<ChargingConnectorBusiness> _logger;
        public ChargingConnectorBusiness(IChargingConnectorRepository chargingConnectorRepository, ILogger<ChargingConnectorBusiness> logger)
        {
            _chargingConnectorRepository = chargingConnectorRepository;
            _logger = logger;
        }
        public async Task<IEnumerable<ChargingConnector>> GetChargingConnectors()
        {
            try
            {
                var result = await _chargingConnectorRepository.GetChargingConnectors();
                return (result != null) ? result : throw new DataNotFoundException("Data Not Found");

            }
            catch (DataNotFoundException ex)
            {
                _logger.LogWarning("Exception occured ChargingConnectorBusiness.GetChargingConnectors", ex);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Exception occured ChargingConnectorBusiness.GetChargingConnectors", ex);
                throw;
            }

        }

        public async Task<ChargingConnector> GetChargingConnectorById(int connectorId)
        {
            try
            {
                var result = await _chargingConnectorRepository.GetChargingConnectorById(connectorId);
                return (result != null) ? result : throw new DataNotFoundException("Data Not Found");

            }
            catch (DataNotFoundException ex)

            {
                _logger.LogWarning("Exception occured ChargingConnectorBusiness.GetChargingConnectorById", ex);
                throw;
            }
            catch (Exception ex)

            {
                _logger.LogWarning("Exception occured ChargingConnectorBusiness.GetChargingConnectorById", ex);
                throw;
            }

        }

        public async Task<ChargingConnector> CreateChargingConnector(ChargingConnector chargingConnector)
        {
            try
            {                         
                
                var connectorCount = _chargingConnectorRepository.GetConnectorCount(chargingConnector.StationIdentifier);
                if (connectorCount < 4)
                {
                    var isGroupCapacityAvail = _chargingConnectorRepository.CheckGroupsCapacity(chargingConnector.StationIdentifier, chargingConnector.MaxCurrent);
                    if (isGroupCapacityAvail)
                    {
                        var result = await _chargingConnectorRepository.CreateChargingConnector(chargingConnector);
                        return (result != null) ? result : null;
                    }
                }
                return null;
            }
            catch (Exception ex)

            {
                _logger.LogWarning("Exception occured ChargingConnectorBusiness.CreateChargingConnector", ex);
                throw;
            }

        }

        public async Task<ChargingConnector> UpdateChargingConnector(ChargingConnector chargingConnectors)
        {
            try
            {
                var result = await _chargingConnectorRepository.UpdateChargingConnector(chargingConnectors);

                return (result != null) ? result : throw new DataNotFoundException("Data Not Found");
            }
            catch (DataNotFoundException ex)
            {
                _logger.LogWarning("Exception occured ChargingConnectorBusiness.UpdateChargingConnector", ex);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Exception occured ChargingConnectorBusiness.UpdateChargingConnector", ex);
                throw; 
            }

        }

        public async Task<bool> DeleteChargingConnectorById(int id)
        {
            try
            {
                var result = await _chargingConnectorRepository.DeleteChargingConnectorById(id);
                return (result) ? result : throw new DataNotFoundException("Data Not Found");
            }
            catch (DataNotFoundException ex)
            {
                _logger.LogWarning("Exception occured ChargingConnectorBusiness.DeleteChargingConnectorById", ex);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Exception occured ChargingConnectorBusiness.DeleteChargingConnectorById", ex);
                throw;
            }
        }
    }
}
