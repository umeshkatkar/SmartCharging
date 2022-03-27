using Microsoft.Extensions.Logging;
using Moq;
using SmartCharging.Business;
using SmartCharging.Repository;
using SmartCharging.Utilities;
using System;
using System.Threading.Tasks;
using Xunit;

namespace SmartCharging.Tests
{
    public class ConnectorBusinessTests
    {

        public Mock<IChargingConnectorRepository> _mockChargingConnectorRepository;
        public IChargingConnectorBusiness _chargingConnectorBusiness;
        public Mock<ILogger<ChargingConnectorBusiness>> _logger;

        public ConnectorBusinessTests()
        {
            _mockChargingConnectorRepository = new Mock<IChargingConnectorRepository>();
            _logger = new Mock<ILogger<ChargingConnectorBusiness>>();
            _chargingConnectorBusiness = new ChargingConnectorBusiness(_mockChargingConnectorRepository.Object, _logger.Object);
        }
        [Fact]
        public void Test_CreateConnector_ShouldReturnSuccess_WhenCreatedInDatabase()
        {
            var connector = new ChargingConnector()
            {
                 ConnectorIdentifier = "15dd5dff-a01d-4716-b236-fa0e7b183926",
                 ConnectorId=1,
                 MaxCurrent=20,
                 StationIdentifier=2
            };
            _mockChargingConnectorRepository.Setup(p => p.GetConnectorCount(It.IsAny<int>())).Returns(2);
            _mockChargingConnectorRepository.Setup(p => p.CheckGroupsCapacity(It.IsAny<int>(), It.IsAny<int>())).Returns(true);
            _mockChargingConnectorRepository.Setup(p => p.CreateChargingConnector(It.IsAny<ChargingConnector>())).ReturnsAsync(connector);
            var response = _chargingConnectorBusiness.CreateChargingConnector(connector);
            Assert.NotNull(response);
            Assert.Equal(connector.ConnectorIdentifier, response?.Result?.ConnectorIdentifier);
        }

        [Fact]
        public void Test_CreateConnector_ShouldReturnException_WhenNotCreatedInDatabase()
        {
            var connector = new ChargingConnector()
            {
                ConnectorIdentifier = "15dd5dff-a01d-4716-b236-fa0e7b183926",
                ConnectorId = 1,
                MaxCurrent = 20,
                StationIdentifier = 2
            };
            _mockChargingConnectorRepository.Setup(p => p.CreateChargingConnector(It.IsAny<ChargingConnector>())).Throws(new Exception());
            var response = _chargingConnectorBusiness.CreateChargingConnector(connector);
            Assert.ThrowsAsync<Exception>(() => response);
        }

        [Fact]
        public void Test_GetChargingConnectorById_ShouldReturnSuccess_WhenRecordFoundInDatabase()
        {
            var connector = new ChargingConnector()
            {
                ConnectorIdentifier = "15dd5dff-a01d-4716-b236-fa0e7b183926",
                ConnectorId = 1,
                MaxCurrent = 20,
                StationIdentifier = 2
            };
            _mockChargingConnectorRepository.Setup(p => p.GetChargingConnectorById(It.IsAny<int>())).ReturnsAsync(connector);
            var response = _chargingConnectorBusiness.GetChargingConnectorById(5);
            Assert.NotNull(response);
            Assert.Equal(connector.ConnectorIdentifier, response?.Result?.ConnectorIdentifier);

        }
        [Fact]
        public void Test_GetChargingConnectorById_ShouldReturnException_WhenNotFoundInDatabase()
        {
            _mockChargingConnectorRepository.Setup(p => p.GetChargingConnectorById(It.IsAny<int>())).Throws(new Exception());
            var response = _chargingConnectorBusiness.GetChargingConnectorById(5);
            Assert.ThrowsAsync<Exception>(() => response);

        }
        [Fact]
        public void Test_DeleteChargingConnectorById_ShouldReturnSuccess_WhenRecordDeletedFromDatabase()
        {
            var connector = new ChargingConnector()
            {
                ConnectorIdentifier = "15dd5dff-a01d-4716-b236-fa0e7b183926",
                ConnectorId = 1,
                MaxCurrent = 20,
                StationIdentifier = 2
            };
            bool isDeleted = true;
            _mockChargingConnectorRepository.Setup(p => p.DeleteChargingConnectorById(It.IsAny<int>())).ReturnsAsync(isDeleted);
            var response = _chargingConnectorBusiness.DeleteChargingConnectorById(5);
            Assert.NotNull(response);
            Assert.True(response.Result);
        }

        [Fact]
        public void Test_DeleteChargingConnectorById_ShouldReturnDataNotFoundException_WhenRecordNotDeletedFromDatabase()
        {
            _mockChargingConnectorRepository.Setup(p => p.DeleteChargingConnectorById(It.IsAny<int>())).ThrowsAsync(new DataNotFoundException());
            var response = _chargingConnectorBusiness.DeleteChargingConnectorById(5);
            Assert.NotNull(response);
            Assert.ThrowsAsync<DataNotFoundException>(() => response);
        }
        [Fact]
        public void Test_DeleteChargingConnectorById_ShouldReturnException_WhenRecordNotDeletedFromDatabase()
        {
            _mockChargingConnectorRepository.Setup(p => p.DeleteChargingConnectorById(It.IsAny<int>())).ThrowsAsync(new Exception());
            var response = _chargingConnectorBusiness.DeleteChargingConnectorById(5);
            Assert.ThrowsAsync<Exception>(() => response);
        }

        [Fact]
        public void Test_UpdateChargingConnector_ShouldReturnSuccess_WhenRecordIsUpdated()
        {
            var updatedConnector = new ChargingConnector()
            {
                ConnectorIdentifier = "15dd5dff-a01d-4716-b236-fa0e7b183926",
                ConnectorId = 1,
                MaxCurrent = 20,
                StationIdentifier = 2
            };
            var originalConnector = new ChargingConnector()
            {
                ConnectorIdentifier = "15dd5dff-a01d-4716-b236-3a0e7b183926",
                ConnectorId = 1,
                MaxCurrent = 10,
                StationIdentifier = 2
            };
            _mockChargingConnectorRepository.Setup(p => p.UpdateChargingConnector(It.IsAny<ChargingConnector>())).ReturnsAsync(updatedConnector);
            var response = _chargingConnectorBusiness.UpdateChargingConnector(originalConnector);
            Assert.NotNull(response);
            Assert.Equal(response.Result.ConnectorIdentifier, updatedConnector.ConnectorIdentifier);
            Assert.Equal(response.Result.MaxCurrent, updatedConnector.MaxCurrent);
        }
        [Fact]
        public void Test_UpdateChargingConnector_ShouldReturnException_WhenExceptionOccured()
        {
            var connector = new ChargingConnector()
            {
                ConnectorIdentifier = "15dd5dff-a01d-4716-b236-fa0e7b183926",
                ConnectorId = 1,
                MaxCurrent = 20,
                StationIdentifier = 2
            };
            _mockChargingConnectorRepository.Setup(p => p.UpdateChargingConnector(It.IsAny<ChargingConnector>())).Throws(new Exception());
            var response = _chargingConnectorBusiness.UpdateChargingConnector(connector);
            Assert.ThrowsAsync<Exception>(() => response);
        }

        [Fact]
        public void Test_UpdateChargingConnector_ShouldReturnDataNotFoundException_WhenRecordNotFound()
        {
            var connector = new ChargingConnector()
            {
                ConnectorIdentifier = "15dd5dff-a01d-4716-b236-fa0e7b183926",
                ConnectorId = 1,
                MaxCurrent = 20,
                StationIdentifier = 2
            };
            _mockChargingConnectorRepository.Setup(p => p.UpdateChargingConnector(It.IsAny<ChargingConnector>())).Throws(new DataNotFoundException());
            var response = _chargingConnectorBusiness.UpdateChargingConnector(connector);
            Assert.ThrowsAsync<DataNotFoundException>(() => response);
        }
    }
}
