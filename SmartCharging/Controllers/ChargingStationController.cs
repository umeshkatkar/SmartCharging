using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartCharging.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartCharging.Controllers
{
    [ApiController]
    public class ChargingStationController : ControllerBase
    {
        private readonly IChargingStationBusiness _chargingStationBusiness;
        private readonly ILogger<ChargingStationController> _logger;
        public ChargingStationController(IChargingStationBusiness chargingConnectorBusiness,ILogger<ChargingStationController> logger)
        {
            _chargingStationBusiness = chargingConnectorBusiness;
            _logger = logger;
        }
        [HttpGet]
        [Route("/Station")]
        public async Task<ActionResult> GetChargingStations()
        {
            try
            {
                var response = await _chargingStationBusiness.GetChargingStations();
                if (response != null)
                    return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Exception occured ChargingStationController.GetChargingStations", ex);
                throw;
            }
            return NotFound();
        }

        [HttpGet]
        [Route("/Station/{id}")]
        public async Task<ActionResult<ChargingStation>> GetChargingStationById(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest();
                var response = await _chargingStationBusiness.GetChargingStationById(id);
                if (response != null)
                    return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Exception occured ChargingStationController.GetChargingStationById", ex);
                throw;

            }
            return NotFound();
        }
       
        [HttpPost]
        [Route("/CreateStation")]
        public async Task<ActionResult<ChargingStation>> CreateStations(ChargingStation chargingStation)
        {
            try
            {
                if (string.IsNullOrEmpty( chargingStation.StationName))
                {
                    return new BadRequestResult();
                }
                var result = await _chargingStationBusiness.CreateChargingStation(chargingStation);

                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Exception occured ChargingStationController.CreateStations", ex);
                throw;
            }
        }

        [HttpPut("UpdateStation/{id}")]
        public async Task<ActionResult> UpdateChargingStation(int id, ChargingStation chargingStation)
        {
            try
            {
                if (id != chargingStation.StationIdentifier)
                    return new BadRequestResult ();

                await _chargingStationBusiness.UpdateChargingStation(chargingStation);
                return Ok();
             
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Exception occured ChargingStationController.UpdateChargingStation", ex);
                return NotFound();
                throw;
            }
        }
        [HttpDelete("/DeleteStation/{id}")]
        public async Task<ActionResult> DeleteChargingStationById(int id)
        {
            try
            {
                if (id < 1)
                    return BadRequest();
                var result = await _chargingStationBusiness.DeleteChargingStationById(id);
                if (result)
                    return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Exception occured ChargingStationController.DeleteChargingStationById", ex);
                throw;

            }
            return NotFound(); ;
        }
    }
}
