using Microsoft.Extensions.Logging;
using SmartCharging.Repository;
using SmartCharging.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartCharging.Business
{
    public class ChargingStationBusiness:IChargingStationBusiness
    {
        private readonly IChargingStationRepository _chargingStationRepository;
        private readonly ILogger<ChargingStationBusiness> _logger;
        public ChargingStationBusiness(IChargingStationRepository chargingStationRepository,  ILogger<ChargingStationBusiness> logger)
        {

            _chargingStationRepository = chargingStationRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<ChargingStation>> GetChargingStations()
        {
            try
            {
                var result = await _chargingStationRepository.GetChargingStations();
                return (result != null) ? result : throw new DataNotFoundException("Data Not Found");

            }
            catch (DataNotFoundException ex)
            {
                _logger.LogWarning("Exception occured ChargingStationBusiness.GetChargingStations", ex);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Exception occured ChargingStationBusiness.GetChargingStations", ex);
                throw;
            }

        }

        public async Task<ChargingStation> GetChargingStationById(int groupId)
        {
            try
            {
                var result = await _chargingStationRepository.GetChargingStationById(groupId);
                return (result != null) ? result : throw new DataNotFoundException("Data Not Found");

            }
            catch (DataNotFoundException ex)
            {
                _logger.LogWarning("Exception occured ChargingStationBusiness.GetChargingStationById", ex);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Exception occured ChargingStationBusiness.GetChargingStationById", ex);
                throw;
            }

        }

        public async Task<ChargingStation> CreateChargingStation(ChargingStation chargingGroup)
        {
            try
            {
                var result = await _chargingStationRepository.CreateChargingStation(chargingGroup);
                return (result != null) ? result : null;
            }
            catch (Exception ex)

            {
                _logger.LogWarning("Exception occured ChargingStationBusiness.CreateChargingStation", ex);
                throw;
            }

        }

        public async Task<ChargingStation> UpdateChargingStation(ChargingStation chargingGroups)
        {
            try
            {
                var result = await _chargingStationRepository.UpdateChargingStation(chargingGroups);

                return (result != null) ? result : throw new DataNotFoundException("Data Not Found");
            }
            catch (DataNotFoundException ex)
            {
                _logger.LogWarning("Exception occured ChargingStationBusiness.UpdateChargingStation", ex);
                throw;
            }
            catch (Exception ex) { 
                _logger.LogWarning("Exception occured ChargingStationBusiness.UpdateChargingStation", ex);
                throw;
            }

        }

        public async Task<bool> DeleteChargingStationById(int id)
        {
            try
            {
                var result = await _chargingStationRepository.DeleteChargingStationById(id);
                return (result) ? result : throw new DataNotFoundException("Data Not Found");
            }
            catch (DataNotFoundException ex)
            {
                _logger.LogWarning("Exception occured ChargingStationBusiness.DeleteChargingStationById", ex);
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Exception occured ChargingStationBusiness.DeleteChargingStationById", ex);
                throw;
            }
        }
    }
}
