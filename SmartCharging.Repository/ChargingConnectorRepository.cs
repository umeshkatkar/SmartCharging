using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCharging.Repository
{
    public class ChargingConnectorRepository : IChargingConnectorRepository
    {
        private readonly DBSmartChargingContext _smartChargingDbContext;
        private readonly ILogger<ChargingConnectorRepository> _logger;
        public ChargingConnectorRepository(DBSmartChargingContext smartChargingDbContext, ILogger<ChargingConnectorRepository> logger)
        {
            _smartChargingDbContext = smartChargingDbContext;
            _logger = logger;
        }
        public async Task<IEnumerable<ChargingConnector>> GetChargingConnectors()
        {
            try
            {

                return await _smartChargingDbContext.ChargingConnector.ToListAsync();

            }
            catch (Exception ex)
            {
                _logger.LogWarning("Exception occured ChargingConnectorRepository.GetChargingConnectors", ex);
                throw;
            }
        }

        public async Task<ChargingConnector> GetChargingConnectorById(int connectorId)
        {
            try
            {
                return await _smartChargingDbContext?.ChargingConnector?.FirstOrDefaultAsync(x => x.ConnectorId == connectorId);

            }
            catch (Exception ex)
            {
                _logger.LogWarning("Exception occured ChargingConnectorRepository.GetChargingConnectorById", ex);
                throw;
            }
        }

        public async Task<ChargingConnector> CreateChargingConnector(ChargingConnector chargingConnector)
        {
            try
            {
                await _smartChargingDbContext.ChargingConnector.AddAsync(chargingConnector);
                var recordsAffected = await _smartChargingDbContext.SaveChangesAsync();
                if (recordsAffected > 0)
                {
                    return await _smartChargingDbContext.ChargingConnector.FirstOrDefaultAsync(x => x.ConnectorId == chargingConnector.ConnectorId);

                }

            }
            catch (Exception ex)
            {
                _logger.LogWarning("Exception occured ChargingConnectorRepository.CreateChargingConnector", ex);
                throw;
            }
            return null;
        }


        public async Task<bool> DeleteChargingConnectorById(int id)
        {
            try
            {
                int recordsAffected = 0;
                var record = await _smartChargingDbContext?.ChargingConnector?.FirstOrDefaultAsync(x => x.ConnectorId == id);
                if (record != null)
                {
                    _smartChargingDbContext?.ChargingConnector?.Remove(record);
                    recordsAffected = await _smartChargingDbContext.SaveChangesAsync();
                }
                return recordsAffected > 0 ? true : false;
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Exception occured ChargingConnectorRepository.DeleteChargingConnectorById", ex);
                throw;
            }
        }

        public async Task<ChargingConnector> UpdateChargingConnector(ChargingConnector chargingConnectors)
        {
            try
            {
                _smartChargingDbContext.Update(chargingConnectors);
                int affectedCount = await _smartChargingDbContext.SaveChangesAsync();
                return affectedCount > 0 ? chargingConnectors : null;
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Exception occured ChargingConnectorRepository.UpdateChargingConnector", ex);
                throw;
            }
        }
        public int GetConnectorCount(int stationId)
        {
            try
            {
                return _smartChargingDbContext.ChargingConnector.Where(x => x.StationIdentifier == stationId).Count();
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Exception occured ChargingConnectorRepository.GetConnectorCount", ex);
                throw;
            }
        }
        public bool CheckGroupsCapacity(int stationId, int requestedCurrent)
        {

            try
            {
                int maxCurrent = 0;
                var result = _smartChargingDbContext.ChargingStation.Where(x => x.StationIdentifier == stationId).FirstOrDefault();

                int groupCapacity = _smartChargingDbContext.ChargingGroup.Where(x => x.GroupIdentifier == result.GroupIdentifier).Select(y => y.CapacityInAmps).FirstOrDefault();
                var stations = _smartChargingDbContext.ChargingStation.Where(x => x.GroupIdentifier == result.GroupIdentifier).ToList();
            

          stations.ForEach(station =>
            {
                var connectors = _smartChargingDbContext.ChargingConnector.Where(x => x.StationIdentifier == station.StationIdentifier)?.ToList();
                connectors.ForEach(conector =>
                {
                    maxCurrent = +_smartChargingDbContext.ChargingConnector.Sum(s => s.MaxCurrent);
                });
            });
                if (groupCapacity >= (requestedCurrent + maxCurrent))
                    return true;
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Exception occured ChargingConnectorRepository.CheckGroupsCapacity", ex);
                throw;
            }
            return false;
        }
    
    }
}
