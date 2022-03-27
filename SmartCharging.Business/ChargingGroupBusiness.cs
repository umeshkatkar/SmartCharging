using Microsoft.Extensions.Logging;
using SmartCharging.Repository;
using SmartCharging.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartCharging.Business
{
    public class ChargingGroupBusiness : IChargingGroupBusiness
    {
        private readonly IChargingGroupRepository _chargingGroupRepository;
        private readonly ILogger<ChargingStationBusiness> _logger;

        public ChargingGroupBusiness(IChargingGroupRepository chargingGroupRepository, ILogger<ChargingStationBusiness> logger)
        {
            _chargingGroupRepository = chargingGroupRepository;
            _logger = logger;
        }
        public async Task<IEnumerable<ChargingGroup>> GetChargingGroups()
        {
            try
            {
                var result = await _chargingGroupRepository.GetChargingGroups();
                return (result != null) ? result : throw new DataNotFoundException("Data Not Found");

            }
            catch (DataNotFoundException ex)
            {
                _logger.LogWarning("Exception occured ChargingGroupBusiness.GetChargingGroups", ex);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Exception occured ChargingGroupBusiness.GetChargingGroups", ex);
                throw;
            }

        }

        public async Task<ChargingGroup> GetChargingGroupById(int groupId)
        {
            try
            {
                var result = await _chargingGroupRepository.GetChargingGroupById(groupId);
                return (result != null) ? result : throw new DataNotFoundException("Data Not Found");

            }
            catch (DataNotFoundException ex)
            {
                _logger.LogWarning("Exception occured ChargingGroupBusiness.GetChargingGroupById", ex);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Exception occured ChargingGroupBusiness.GetChargingGroupById", ex);
                throw;
            }

        }

        public async Task<ChargingGroup> CreateChargingGroup(ChargingGroup chargingGroup)
        {
            try
            {
                var result = await _chargingGroupRepository.CreateChargingGroup(chargingGroup);
                return (result != null) ? result : null;
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Exception occured ChargingGroupBusiness.CreateChargingGroup", ex);
                throw;
            }

        }

        public async Task<ChargingGroup> UpdateChargingGroup(ChargingGroup chargingGroups)
        {
            try
            {
                var result = await _chargingGroupRepository.UpdateChargingGroup(chargingGroups);

                return (result != null) ? result : throw new DataNotFoundException("Data Not Found");
            }
            catch (DataNotFoundException ex)
            {
                _logger.LogWarning("Exception occured ChargingGroupBusiness.UpdateChargingGroup", ex);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Exception occured ChargingGroupBusiness.UpdateChargingGroup", ex);
                throw; 
            }

        }

        public async Task<bool> DeleteChargingGroupById(int id)
        {
            try
            {
                var result = await _chargingGroupRepository.DeleteChargingGroupById(id);
                return (result) ? result : throw new DataNotFoundException("Data Not Found");
            }
            catch (DataNotFoundException ex)
            {
                _logger.LogWarning("Exception occured ChargingGroupBusiness.DeleteChargingGroupById", ex);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Exception occured ChargingGroupBusiness.DeleteChargingGroupById", ex);
                throw;
            }
        }
    }

}
