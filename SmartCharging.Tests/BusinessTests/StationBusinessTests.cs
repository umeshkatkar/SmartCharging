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
    public class StationBusinessTests
    {

        public Mock<IChargingStationRepository> _mockChargingStationRepository;
        public IChargingStationBusiness _chargingStationBusiness;
        public Mock<ILogger<ChargingStationBusiness>> _logger;

        public StationBusinessTests()
        {
            _mockChargingStationRepository = new Mock<IChargingStationRepository>();
            _logger = new Mock<ILogger<ChargingStationBusiness>>();
            _chargingStationBusiness = new ChargingStationBusiness(_mockChargingStationRepository.Object, _logger.Object);
        }
        [Fact]
        public void Test_CreateStation_ShouldReturnSuccess_WhenCreatedInDatabase()
        {
            var station = new ChargingStation()
            {
               StationIdentifier=1,
               StationName="Station1",
               GroupIdentifier = 1              
               
            };
            _mockChargingStationRepository.Setup(p => p.CreateChargingStation(It.IsAny<ChargingStation>())).ReturnsAsync(station);
            var response = _chargingStationBusiness.CreateChargingStation(station);
            Assert.NotNull(response);
            Assert.Equal(station.StationName, response?.Result?.StationName);
        }

        [Fact]
        public void Test_CreateGroup_ShouldReturnException_WhenNotCreatedInDatabase()
        {
            var station = new ChargingStation()
            {
                StationIdentifier = 1,
                StationName = "Station1",
                GroupIdentifier = 1

            };
            _mockChargingStationRepository.Setup(p => p.CreateChargingStation(It.IsAny<ChargingStation>())).Throws(new Exception());
            var response = _chargingStationBusiness.CreateChargingStation(station);
            Assert.ThrowsAsync<Exception>(() => response);
        }

        [Fact]
        public void Test_GetChargingStationById_ShouldReturnSuccess_WhenRecordFoundInDatabase()
        {
            var station = new ChargingStation()
            {
                StationIdentifier = 1,
                StationName = "Station1",
                GroupIdentifier = 1

            };
            _mockChargingStationRepository.Setup(p => p.GetChargingStationById(It.IsAny<int>())).ReturnsAsync(station);
            var response = _chargingStationBusiness.GetChargingStationById(5);
            Assert.NotNull(response);
            Assert.Equal(station.StationName, response?.Result?.StationName);
        }
        [Fact]
        public void Test_GetChargingStationById_ShouldReturnException_WhenNotFoundInDatabase()
        {
            _mockChargingStationRepository.Setup(p => p.GetChargingStationById(It.IsAny<int>())).Throws(new Exception());
            var response = _chargingStationBusiness.GetChargingStationById(5);
            Assert.ThrowsAsync<Exception>(() => response);

        }
        [Fact]
        public void Test_DeleteChargingStationById_ShouldReturnSuccess_WhenRecordDeletedFromDatabase()
        {
            var station = new ChargingStation()
            {
                StationIdentifier = 1,
                StationName = "Station1",
                GroupIdentifier = 1

            };
            bool isDeleted = true;
            _mockChargingStationRepository.Setup(p => p.DeleteChargingStationById(It.IsAny<int>())).ReturnsAsync(isDeleted);
            var response = _chargingStationBusiness.DeleteChargingStationById(5);
            Assert.NotNull(response);
            Assert.True(response.Result);
        }

        [Fact]
        public void Test_DeleteChargingStationById_ShouldReturnDataNotFoundException_WhenRecordNotDeletedFromDatabase()
        {
            _mockChargingStationRepository.Setup(p => p.DeleteChargingStationById(It.IsAny<int>())).ThrowsAsync(new DataNotFoundException());
            var response = _chargingStationBusiness.DeleteChargingStationById(5);
            Assert.NotNull(response);
            Assert.ThrowsAsync<DataNotFoundException>(() => response);
        }
        [Fact]
        public void Test_DeleteChargingStationById_ShouldReturnException_WhenRecordNotDeletedFromDatabase()
        {
            _mockChargingStationRepository.Setup(p => p.DeleteChargingStationById(It.IsAny<int>())).ThrowsAsync(new Exception());
            var response = _chargingStationBusiness.DeleteChargingStationById(5);
            Assert.ThrowsAsync<Exception>(() => response);
        }

        [Fact]
        public void Test_UpdateChargingStation_ShouldReturnSuccess_WhenRecordIsUpdated()
        {
            var updatedStation = new ChargingStation()
            {
                StationIdentifier = 1,
                StationName = "Station1",
                GroupIdentifier = 1

            };
            var originalStation = new ChargingStation()
            {
                StationIdentifier = 1,
                StationName = "Station2",
                GroupIdentifier = 1

            };
            _mockChargingStationRepository.Setup(p => p.UpdateChargingStation(It.IsAny<ChargingStation>())).ReturnsAsync(updatedStation);
            var response = _chargingStationBusiness.UpdateChargingStation(originalStation);
            Assert.NotNull(response);
            Assert.Equal(response.Result.StationName, updatedStation.StationName);
            Assert.Equal(response.Result.StationIdentifier, updatedStation.StationIdentifier);
        }
        [Fact]
        public void Test_UpdateChargingStation_ShouldReturnException_WhenExceptionOccured()
        {
            var originalStation = new ChargingStation()
            {
                StationIdentifier = 1,
                StationName = "Station2",
                GroupIdentifier = 1

            };
            _mockChargingStationRepository.Setup(p => p.UpdateChargingStation(It.IsAny<ChargingStation>())).Throws(new Exception());
            var response = _chargingStationBusiness.UpdateChargingStation(originalStation);
            Assert.ThrowsAsync<Exception>(() => response);
        }

        [Fact]
        public void Test_UpdateChargingStation_ShouldReturnDataNotFoundException_WhenRecordNotFound()
        {
            var originalStation = new ChargingStation()
            {
                StationIdentifier = 1,
                StationName = "Station2",
                GroupIdentifier = 1

            };
            _mockChargingStationRepository.Setup(p => p.UpdateChargingStation(It.IsAny<ChargingStation>())).Throws(new DataNotFoundException());
            var response = _chargingStationBusiness.UpdateChargingStation(originalStation);
            Assert.ThrowsAsync<DataNotFoundException>(() => response);
        }
    }
}
