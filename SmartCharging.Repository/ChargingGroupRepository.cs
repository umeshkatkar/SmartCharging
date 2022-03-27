
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SmartCharging.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartCharging.Repository
{
    public class ChargingGroupRepository : IChargingGroupRepository
    {
        private readonly DBSmartChargingContext _smartChargingDbContext;
        private readonly ILogger<ChargingGroupRepository> _logger;
        public ChargingGroupRepository(DBSmartChargingContext smartChargingDbContext, ILogger<ChargingGroupRepository> logger)
        {
            this._smartChargingDbContext = smartChargingDbContext;
            _logger=logger;
        }
        public async Task<IEnumerable<ChargingGroup>> GetChargingGroups()
        {
            try
            {
                return await _smartChargingDbContext.ChargingGroup.Include(x => x.ChargingStations).ThenInclude(y=>y.ChargingConnectors).ToListAsync();

            }
            catch (Exception ex)
            {
                _logger.LogWarning("Exception occured ChargingGroupRepository.GetChargingGroups", ex);
                throw;
            }
        }

        public async Task<ChargingGroup> GetChargingGroupById(int groupId)
        {
            try
            {
                return await _smartChargingDbContext?.ChargingGroup?.Include(x => x.ChargingStations).FirstOrDefaultAsync(x => x.GroupIdentifier == groupId);

            }
            catch (Exception ex)
            {
                _logger.LogWarning("Exception occured ChargingGroupRepository.GetChargingGroupById", ex);
                throw;
            }
        }

        public async Task<ChargingGroup> CreateChargingGroup(ChargingGroup chargingGroup)
        {
            try
            {
                await _smartChargingDbContext.ChargingGroup.AddAsync(chargingGroup);
                var recordsAffected = await _smartChargingDbContext.SaveChangesAsync();
                if (recordsAffected > 0)
                {
                    return await _smartChargingDbContext.ChargingGroup.FirstOrDefaultAsync(x => x.GroupIdentifier == chargingGroup.GroupIdentifier);

                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Exception occured ChargingGroupRepository.CreateChargingGroup", ex);
                throw;
            }
            return null;
        }

        public async Task<bool> DeleteChargingGroupById(int id)
        {
            try
            {
                int recordsAffected = 0;
                var record = await _smartChargingDbContext?.ChargingGroup?.FirstOrDefaultAsync(x => x.GroupIdentifier == id);
                if (record != null)
                {
                    _smartChargingDbContext?.ChargingGroup?.Remove(record);
                    recordsAffected = await _smartChargingDbContext.SaveChangesAsync();
                }

                return recordsAffected > 0 ? true : false;
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Exception occured ChargingGroupRepository.DeleteChargingGroupById", ex);
                throw;
            }
        }

        public async Task<ChargingGroup> UpdateChargingGroup(ChargingGroup chargingGroups)
        {
            try
            {
                _smartChargingDbContext.Update(chargingGroups);
                int affectedCount = await _smartChargingDbContext.SaveChangesAsync();
                return affectedCount > 0 ? chargingGroups : null;
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Exception occured ChargingGroupRepository.UpdateChargingGroup", ex);
                throw;
            }
        }
    }
}
