using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartCharging.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SmartCharging.Controllers
{
    [ApiController]
        public class ChargingGroupController : ControllerBase

    {   private readonly IChargingGroupBusiness _chargingGroupBusiness;
        private readonly ILogger<ChargingGroupController> _logger;
        public ChargingGroupController(IChargingGroupBusiness chargingGroupBusiness, ILogger<ChargingGroupController> logger)
        {
            _chargingGroupBusiness = chargingGroupBusiness;
            _logger = logger;
        }

        [HttpGet]
        [Route("/Group")]
        public async Task< ActionResult> GetChargingGroups()
        {
            try
            {
                var response = await _chargingGroupBusiness.GetChargingGroups();
                if (response != null)
                    return  new OkObjectResult(response);

            }
            catch (Exception ex)
            {
                _logger.LogWarning("Exception occured ChargingGroupController.GetChargingGroups", ex);
                throw;

            }
            return NotFound();
        }

        [HttpGet]
        [Route("/Group/{id}")]
        public async Task<ActionResult<ChargingGroup>> GetChargingGroupById(int id)
        {
            try
            {
                if (id <= 0)
                    return new BadRequestResult();
                var response = await _chargingGroupBusiness.GetChargingGroupById(id);
                
                if (response != null)
                    return new OkObjectResult(response);
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Exception occured ChargingGroupController.GetChargingGroupById", ex);
                throw;

            }
            return NotFound();
        }

        [HttpPost]
        [Route("/CreateGroup")]
        public async Task<ActionResult<ChargingGroup>> CreateGroups(ChargingGroup chargingGroup)
        {
            try
            {
                if (string.IsNullOrEmpty(chargingGroup.GroupName))
                {
                    return new BadRequestResult();
                }
                var result = await _chargingGroupBusiness.CreateChargingGroup(chargingGroup);

                return new OkObjectResult(result);

            }
            catch (Exception ex)
            { 
                _logger.LogWarning("Exception occured ChargingGroupController.CreateGroups", ex); 
                throw; 
            }
        }

        [HttpPut("UpdateGroup/{id}")]
        public async Task<ActionResult> UpdateChargingGroup(int id, ChargingGroup chargingGroup)
        {
            try
            {  
                if (id != chargingGroup.GroupIdentifier)
                    return new BadRequestResult();

                await _chargingGroupBusiness.UpdateChargingGroup(chargingGroup);
                 return Ok();
            }
            catch (Exception ex)
            { 
                _logger.LogWarning("Exception occured ChargingGroupController.UpdateChargingGroup", ex);
                return NotFound();
                throw;
            }
        }
          [HttpDelete("/DeleteGroup/{id}")]
        public async Task<ActionResult> DeleteChargingGroupById(int id)
        {
            try
            {
                if (id <1)
                    return BadRequest();
                var result= await _chargingGroupBusiness.DeleteChargingGroupById(id);
                if(result)
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Exception occured ChargingGroupController.DeleteChargingGroupById", ex);
                throw;

            }
            return NotFound(); ;
        }
    }
}
