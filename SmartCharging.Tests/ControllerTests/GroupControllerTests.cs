using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using SmartCharging.Business;
using SmartCharging.Controllers;
using SmartCharging.Utilities;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;


namespace SmartCharging.Tests.ControllerTests
{
     public  class GroupControllerTests
    {
        public Mock<IChargingGroupBusiness> _mockChargingGroupBusiness;
        public ChargingGroupController _chargingGroupController;
        public Mock<ILogger<ChargingGroupController>> _logger;

        public GroupControllerTests()
        {
            _mockChargingGroupBusiness = new Mock<IChargingGroupBusiness>();
            _logger= new Mock < ILogger < ChargingGroupController >> ();
            _chargingGroupController = new ChargingGroupController(_mockChargingGroupBusiness.Object, _logger.Object);
        }

        [Fact]
        public async void Test_CreateGroup_ShouldReturnSuccess_WhenCreatedInDatabase()
        {
            var group = new ChargingGroup()
            {
                GroupName = "Group1",
                GroupIdentifier = 1,
                CapacityInAmps = 100
            };
            _mockChargingGroupBusiness.Setup(p => p.CreateChargingGroup(It.IsAny<ChargingGroup>())).ReturnsAsync(group);
            var response = await _chargingGroupController.CreateGroups(group);
            Assert.NotNull(response);
            Assert.IsType<OkObjectResult>(response.Result);
        }
        [Fact]
        public async void Test_CreateGroup_ShouldReturnBadRequest_WhenIncorrectInput()
        {
            var group = new ChargingGroup();
            _mockChargingGroupBusiness.Setup(p => p.CreateChargingGroup(It.IsAny<ChargingGroup>())).ReturnsAsync(group);
            var response = await _chargingGroupController.CreateGroups(group);
             Assert.IsType<BadRequestResult>(response.Result);
           
        }
        [Fact]
        public async void Test_UpdateGroups_ShouldReturnSuccess_WhenUpdatedInDatabase()
        {
            var originalGroup = new ChargingGroup()
            {
                GroupName = "Group1",
                GroupIdentifier = 1,
                CapacityInAmps = 100
            };
            var updatedGroup = new ChargingGroup()
            {
                GroupName = "Group2",
                GroupIdentifier = 1,
                CapacityInAmps = 110
            };
            _mockChargingGroupBusiness.Setup(p => p.UpdateChargingGroup(It.IsAny<ChargingGroup>())).ReturnsAsync(updatedGroup);
            var response = await _chargingGroupController.UpdateChargingGroup(1,originalGroup);
            Assert.NotNull(response);
            Assert.IsType<OkResult>(response);
        }
        [Fact]
        public async void Test_UpdateGroup_ShouldReturnBadRequest_WhenIncorrectInput()
        {
            var group = new ChargingGroup();
            _mockChargingGroupBusiness.Setup(p => p.CreateChargingGroup(It.IsAny<ChargingGroup>())).ReturnsAsync(group);
            var response = await _chargingGroupController.UpdateChargingGroup(1,group);
            Assert.IsType<BadRequestResult>(response);

        }
        [Fact]
        public async void Test_DeleteGroupById_ShouldReturnSuccess_WhenGroupDeletedInDatabase()
        {
           _mockChargingGroupBusiness.Setup(p => p.DeleteChargingGroupById(It.IsAny<int>())).ReturnsAsync(true);
            var response = await _chargingGroupController.DeleteChargingGroupById(4);
            Assert.NotNull(response);
            Assert.IsType<OkResult>(response);
          
        }
        [Fact]
        public async void Test_DeleteGroupById_ShouldReturnBadRequest_WhenIncorrectInput()
        {
            _mockChargingGroupBusiness.Setup(p => p.DeleteChargingGroupById(It.IsAny<int>())).ReturnsAsync(false);
            var response = await _chargingGroupController.DeleteChargingGroupById(-1);
             Assert.IsType<BadRequestResult>(response);

        }
        [Fact]
        public async void Test_GetGroupById_ShouldReturnSuccess_WhenGroupExistInDatabase()
        {
            var group = new ChargingGroup()
            {
                GroupName = "Group1",
                GroupIdentifier = 1,
                CapacityInAmps = 100
            };
            _mockChargingGroupBusiness.Setup(p => p.GetChargingGroupById(It.IsAny<int>())).ReturnsAsync(group);
            var response = await _chargingGroupController.GetChargingGroupById(1);
            Assert.NotNull(response);
            Assert.IsType<OkObjectResult>(response.Result);

        }
        [Fact]
        public async void Test_GetGroupById_ShouldReturnBadRequest_WhenIncorrectInput()
        {
            var group = new ChargingGroup();
           
            _mockChargingGroupBusiness.Setup(p => p.GetChargingGroupById(It.IsAny<int>())).ReturnsAsync(group);
            var response = await _chargingGroupController.GetChargingGroupById(-1);
            Assert.IsType<BadRequestResult>(response.Result);

        }

    }
}
