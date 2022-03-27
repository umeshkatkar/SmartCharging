using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartCharging.Repository
{
    public class ChargingStationRepository: IChargingStationRepository
    {
        private readonly DBSmartChargingContext _smartChargingDbContext;
        private readonly ILogger<ChargingStationRepository> _logger;
        public ChargingStationRepository(DBSmartChargingContext smartChargingDbContext, ILogger<ChargingStationRepository> logger)
        {
            _smartChargingDbContext = smartChargingDbContext;
            _logger = logger;
        }
        public async Task<IEnumerable<ChargingStation>> GetChargingStations()
        {
            try
            {
                return await _smartChargingDbContext.ChargingStation.Include(x => x.ChargingConnectors).ToListAsync();

            }
            catch (Exception ex)
            {
                _logger.LogWarning("Exception occured ChargingStationRepository.GetChargingStations", ex);
                throw;
            }
        }

        public async Task<ChargingStation> GetChargingStationById(int  stationId)
        {
            try
            {
                return await _smartChargingDbContext?.ChargingStation?.Include(x => x.ChargingConnectors).FirstOrDefaultAsync(x => x.StationIdentifier == stationId);

            }
            catch (Exception ex)
            {
                _logger.LogWarning("Exception occured ChargingStationRepository.GetChargingStationById", ex);
                throw;
            }
        }

        public async Task<ChargingStation> CreateChargingStation(ChargingStation chargingStation)
        {
            try
            {
                await _smartChargingDbContext.ChargingStation.AddAsync(chargingStation);
                var recordsAffected = await _smartChargingDbContext.SaveChangesAsync();
                if (recordsAffected > 0)
                {
                    return await _smartChargingDbContext.ChargingStation.FirstOrDefaultAsync(x => x.StationIdentifier == chargingStation.StationIdentifier);

                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Exception occured ChargingStationRepository.CreateChargingStation", ex);
                throw;
            }
            return null;
        }

        public async Task<bool> DeleteChargingStationById(int id)
        {
            try
            {
                int recordsAffected = 0;
                var record = await _smartChargingDbContext?.ChargingStation?.FirstOrDefaultAsync(x => x.StationIdentifier == id);
                if (record != null)
                {
                    _smartChargingDbContext?.ChargingStation?.Remove(record);
                    recordsAffected = await _smartChargingDbContext.SaveChangesAsync();
                }

                return recordsAffected > 0 ? true : false;
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Exception occured ChargingStationRepository.DeleteChargingStationById", ex);
                throw;
            }
        }

        public async Task<ChargingStation> UpdateChargingStation(ChargingStation  chargingStation)
        {
            try
            {
                _smartChargingDbContext.Update(chargingStation);
                int affectedCount = await _smartChargingDbContext.SaveChangesAsync();
                return affectedCount > 0 ? chargingStation : null;
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Exception occured ChargingStationRepository.UpdateChargingStation", ex);
                throw;
            }
        }
    }
}
