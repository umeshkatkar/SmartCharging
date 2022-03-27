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
    public class StationControllerTests
    {
        public Mock<IChargingStationBusiness> _mockChargingStationBusiness;
        public ChargingStationController _chargingStationController;
        public Mock<ILogger<ChargingStationController>> _logger;

        public StationControllerTests()
        {
            _mockChargingStationBusiness = new Mock<IChargingStationBusiness>();
            _logger = new Mock<ILogger<ChargingStationController>>();
            _chargingStationController = new ChargingStationController(_mockChargingStationBusiness.Object, _logger.Object);
        }

        [Fact]
        public async void Test_CreateStation_ShouldReturnSuccess_WhenCreatedInDatabase()
        {
            var station = new ChargingStation()
            {
                StationIdentifier = 1,
                StationName = "Station1",
                GroupIdentifier = 1
            };
            _mockChargingStationBusiness.Setup(p => p.CreateChargingStation(It.IsAny<ChargingStation>())).ReturnsAsync(station);
            var response = await _chargingStationController.CreateStations(station);
            Assert.NotNull(response);
            Assert.IsType<OkObjectResult>(response.Result);
        }
        [Fact]
        public async void Test_CreateStation_ShouldReturnBadRequest_WhenIncorrectInput()
        {
            var station = new ChargingStation();
            _mockChargingStationBusiness.Setup(p => p.CreateChargingStation(It.IsAny<ChargingStation>())).ReturnsAsync(station);
            var response = await _chargingStationController.CreateStations(station);
            Assert.IsType<BadRequestResult>(response.Result);

        }
        [Fact]
        public async void Test_UpdateStations_ShouldReturnSuccess_WhenUpdatedInDatabase()
        {
            var originalStation = new ChargingStation()
            {
                StationIdentifier = 1,
                StationName = "Station1",
                GroupIdentifier = 1
            };
            var updatedStation = new ChargingStation()
            {
                StationIdentifier = 1,
                StationName = "Station2",
                GroupIdentifier = 1
            };
            _mockChargingStationBusiness.Setup(p => p.UpdateChargingStation(It.IsAny<ChargingStation>())).ReturnsAsync(updatedStation);
            var response = await _chargingStationController.UpdateChargingStation(1,originalStation);
            Assert.NotNull(response);
            Assert.IsType<OkResult>(response);
        }
        [Fact]
        public async void Test_UpdateStation_ShouldReturnBadRequest_WhenIncorrectInput()
        {
            var station = new ChargingStation();
            _mockChargingStationBusiness.Setup(p => p.CreateChargingStation(It.IsAny<ChargingStation>())).ReturnsAsync(station);
            var response = await _chargingStationController.UpdateChargingStation(1,station);
            Assert.IsType<BadRequestResult>(response);

        }
        [Fact]
        public async void Test_DeleteStationById_ShouldReturnSuccess_WhenStationDeletedInDatabase()
        {
            _mockChargingStationBusiness.Setup(p => p.DeleteChargingStationById(It.IsAny<int>())).ReturnsAsync(true);
            var response = await _chargingStationController.DeleteChargingStationById(4);
            Assert.NotNull(response);
            Assert.IsType<OkResult>(response);

        }
        [Fact]
        public async void Test_DeleteStationById_ShouldReturnBadRequest_WhenIncorrectInput()
        {
            _mockChargingStationBusiness.Setup(p => p.DeleteChargingStationById(It.IsAny<int>())).ReturnsAsync(false);
            var response = await _chargingStationController.DeleteChargingStationById(-1);
            Assert.IsType<BadRequestResult>(response);

        }
        [Fact]
        public async void Test_GetStationById_ShouldReturnSuccess_WhenStationExistInDatabase()
        {
            var station = new ChargingStation()
            {
                StationIdentifier = 1,
                StationName = "Station1",
                GroupIdentifier = 1
            };
            _mockChargingStationBusiness.Setup(p => p.GetChargingStationById(It.IsAny<int>())).ReturnsAsync(station);
            var response = await _chargingStationController.GetChargingStationById(1);
            Assert.NotNull(response);
            Assert.IsType<OkObjectResult>(response.Result);

        }
        [Fact]
        public async void Test_GetStationById_ShouldReturnBadRequest_WhenIncorrectInput()
        {
            var station = new ChargingStation();

            _mockChargingStationBusiness.Setup(p => p.GetChargingStationById(It.IsAny<int>())).ReturnsAsync(station);
            var response = await _chargingStationController.DeleteChargingStationById(-1);
            Assert.IsType<BadRequestResult>(response);

        }

    }
}
