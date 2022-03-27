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

    public class ChargingConnectorController : ControllerBase
    {
        private readonly IChargingConnectorBusiness _chargingConnectorBusiness;
        private readonly ILogger<ChargingConnectorController> _logger;
        public ChargingConnectorController(IChargingConnectorBusiness chargingConnectorBusiness, ILogger<ChargingConnectorController> logger)
        {
            _chargingConnectorBusiness = chargingConnectorBusiness;
            _logger = logger;
        }
        [HttpGet]
        [Route("/Connector")]
        public async Task<ActionResult> GetChargingConnectors()
        {
            try
            {
                var response = await _chargingConnectorBusiness.GetChargingConnectors();
                if (response != null)
                    return new OkObjectResult(response);

            }
            catch (Exception ex)
            {
                _logger.LogWarning("Exception occured ChargingConnectorController.GetChargingConnectors", ex);
                throw;

            }
            return NotFound();
        }

        [HttpGet]
        [Route("/Connector/{id}")]
        public async Task<ActionResult<ChargingConnector>> GetChargingConnectorById(int id)
        {
            try
            {
                if (id <= 0)
                    return new BadRequestResult();
                var response = await _chargingConnectorBusiness.GetChargingConnectorById(id);
                if (response != null)
                    return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Exception occured ChargingConnectorController.GetChargingConnectorById", ex);
                throw;

            }
            return NotFound();
        }

        [HttpPost]
        [Route("/CreateConnector")]
        public async Task<ActionResult<ChargingConnector>> CreateConnectors(ChargingConnector chargingConnector)
        {
            try
            {
                if (string.IsNullOrEmpty( chargingConnector.ConnectorIdentifier))
                {
                    return new BadRequestResult();
                }
                var result = await _chargingConnectorBusiness.CreateChargingConnector(chargingConnector);
                if (result != null)
                    return new OkObjectResult(result);
                return new BadRequestResult();
            }
            catch (Exception ex)
            { 
                _logger.LogWarning("Exception occured ChargingConnectorController.CreateConnectors", ex);
                
                throw; }
        }

        [HttpPut("UpdateConnector/{id}")]
        public async Task<ActionResult> UpdateChargingConnector(int id, ChargingConnector chargingConnector)
        {
            try
            {
                if (id != chargingConnector.ConnectorId)
                    return new BadRequestResult();

                await _chargingConnectorBusiness.UpdateChargingConnector(chargingConnector);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Exception occured ChargingConnectorController.UpdateChargingConnector", ex);
                return NotFound();
                throw;
            }
        }
        [HttpDelete("/DeleteConnector/{id}")]
        public async Task<ActionResult> DeleteChargingConnectorById(int id)
        {
            try
            {
                if (id < 1)
                    return new BadRequestResult();
                var result = await _chargingConnectorBusiness.DeleteChargingConnectorById(id);
                if (result)
                    return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Exception occured ChargingConnectorController.DeleteChargingConnectorById", ex);
                throw;

            }
            return NotFound(); ;
        }
    }
}
