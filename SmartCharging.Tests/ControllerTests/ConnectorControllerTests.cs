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
    public class ConnectorControllerTests
    {
        public Mock<IChargingConnectorBusiness> _mockChargingConnectorBusiness;
        public ChargingConnectorController _chargingConnectorController;
        public Mock<ILogger<ChargingConnectorController>> _logger;

        public ConnectorControllerTests()
        {
            _mockChargingConnectorBusiness = new Mock<IChargingConnectorBusiness>();
            _logger = new Mock<ILogger<ChargingConnectorController>>();
            _chargingConnectorController = new ChargingConnectorController(_mockChargingConnectorBusiness.Object, _logger.Object);
        }

        [Fact]
        public async void Test_CreateConnector_ShouldReturnSuccess_WhenCreatedInDatabase()
        {
            var connector = new ChargingConnector()
            {
                ConnectorIdentifier = "ffnxcknz346489313bnd",
                MaxCurrent = 11,
                ConnectorId = 1,
                StationIdentifier = 2
            };
            _mockChargingConnectorBusiness.Setup(p => p.CreateChargingConnector(It.IsAny<ChargingConnector>())).ReturnsAsync(connector);
            var response = await _chargingConnectorController.CreateConnectors(connector);
            Assert.NotNull(response);
            Assert.IsType<OkObjectResult>(response.Result);
        }
        [Fact]
        public async void Test_CreateConnector_ShouldReturnBadRequest_WhenIncorrectInput()
        {
            var station = new ChargingConnector();
            _mockChargingConnectorBusiness.Setup(p => p.CreateChargingConnector(It.IsAny<ChargingConnector>())).ReturnsAsync(station);
            var response = await _chargingConnectorController.CreateConnectors(station);
            Assert.IsType<BadRequestResult>(response.Result);

        }
        [Fact]
        public async void Test_UpdateConnectors_ShouldReturnSuccess_WhenUpdatedInDatabase()
        {
            var originalConnector = new ChargingConnector()
            {
                ConnectorIdentifier = "ffnxcknz346489313bnd",
                MaxCurrent = 11,
                ConnectorId = 1,
                StationIdentifier = 2
            };
            var updatedConnector = new ChargingConnector()
            {
                ConnectorIdentifier = "ffnxcknz346489313bndeew33",
                MaxCurrent = 13,
                ConnectorId = 1,
                StationIdentifier = 2
            };
            _mockChargingConnectorBusiness.Setup(p => p.UpdateChargingConnector(It.IsAny<ChargingConnector>())).ReturnsAsync(updatedConnector);
            var response = await _chargingConnectorController.UpdateChargingConnector(1, originalConnector);
            Assert.NotNull(response);
            Assert.IsType<OkResult>(response);
        }
        [Fact]
        public async void Test_UpdateConnector_ShouldReturnBadRequest_WhenIncorrectInput()
        {
            var connector = new ChargingConnector();
            _mockChargingConnectorBusiness.Setup(p => p.CreateChargingConnector(It.IsAny<ChargingConnector>())).ReturnsAsync(connector);
            var response = await _chargingConnectorController.UpdateChargingConnector(1, connector);
            Assert.IsType<BadRequestResult>(response);

        }
        [Fact]
        public async void Test_DeleteConnectorById_ShouldReturnSuccess_WhenConnectorDeletedInDatabase()
        {
            _mockChargingConnectorBusiness.Setup(p => p.DeleteChargingConnectorById(It.IsAny<int>())).ReturnsAsync(true);
            var response = await _chargingConnectorController.DeleteChargingConnectorById(4);
            Assert.NotNull(response);
            Assert.IsType<OkResult>(response);

        }
        [Fact]
        public async void Test_DeleteConnectorById_ShouldReturnBadRequest_WhenIncorrectInput()
        {
            _mockChargingConnectorBusiness.Setup(p => p.DeleteChargingConnectorById(It.IsAny<int>())).ReturnsAsync(false);
            var response = await _chargingConnectorController.DeleteChargingConnectorById(-1);
            Assert.IsType<BadRequestResult>(response);

        }
        [Fact]
        public async void Test_GetConnectorById_ShouldReturnSuccess_WhenConnectorExistInDatabase()
        {
            var connector = new ChargingConnector()
            {
                ConnectorIdentifier = "ffnxcknz346489313bndeew33",
                MaxCurrent = 13,
                ConnectorId = 1,
                StationIdentifier = 2
            };
            _mockChargingConnectorBusiness.Setup(p => p.GetChargingConnectorById(It.IsAny<int>())).ReturnsAsync(connector);
            var response = await _chargingConnectorController.GetChargingConnectorById(1);
            Assert.NotNull(response);
            Assert.IsType<OkObjectResult>(response.Result);

        }
        [Fact]
        public async void Test_GetConnectorById_ShouldReturnBadRequest_WhenIncorrectInput()
        {
            var station = new ChargingConnector();

            _mockChargingConnectorBusiness.Setup(p => p.GetChargingConnectorById(It.IsAny<int>())).ReturnsAsync(station);
            var response = await _chargingConnectorController.DeleteChargingConnectorById(-1);
            Assert.IsType<BadRequestResult>(response);

        }

    }
}
