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
    public class GroupBusinessTests
    {

        public Mock<IChargingGroupRepository> _mockChargingGroupRepository;
        public IChargingGroupBusiness _chargingGroupBusiness;
        public Mock<ILogger<ChargingStationBusiness> > _logger;

        public GroupBusinessTests()
        {
            _mockChargingGroupRepository = new Mock<IChargingGroupRepository>();
            _logger = new Mock<ILogger<ChargingStationBusiness>>();
            _chargingGroupBusiness =new ChargingGroupBusiness(_mockChargingGroupRepository.Object, _logger.Object);
        }
        [Fact]
        public void Test_CreateGroup_ShouldReturnSuccess_WhenCreatedInDatabase()
        {
            var group = new ChargingGroup()
            {
                GroupName="Group1",
                GroupIdentifier=1,
                CapacityInAmps=100
            };
            _mockChargingGroupRepository.Setup(p => p.CreateChargingGroup(It.IsAny<ChargingGroup>())).ReturnsAsync(group);
            var response=  _chargingGroupBusiness.CreateChargingGroup(group);
            Assert.NotNull(response);
            Assert.Equal(group.GroupName,response?.Result?.GroupName);
        }

        [Fact]
        public void Test_CreateGroup_ShouldReturnException_WhenNotCreatedInDatabase()
        {
            var group = new ChargingGroup()
            {
                GroupName = "Group1",
                GroupIdentifier = 1,
                CapacityInAmps = 100
            };
            _mockChargingGroupRepository.Setup(p => p.CreateChargingGroup(It.IsAny<ChargingGroup>())).Throws(new Exception());
            var response = _chargingGroupBusiness.CreateChargingGroup(group);
            Assert.ThrowsAsync<Exception>(() => response);
        }

        [Fact]
        public void Test_GetChargingGroupById_ShouldReturnSuccess_WhenRecordFoundInDatabase()
        {
            var group = new ChargingGroup()
            {
                GroupName = "Group1",
                GroupIdentifier = 1,
                CapacityInAmps = 100
            };
            _mockChargingGroupRepository.Setup(p => p.GetChargingGroupById(It.IsAny<int>())).ReturnsAsync(group);
            var response = _chargingGroupBusiness.GetChargingGroupById(5);
            Assert.NotNull(response);
            Assert.Equal(group.GroupName, response?.Result?.GroupName);
        }
        [Fact]
        public  void Test_GetChargingGroupById_ShouldReturnException_WhenNotFoundInDatabase()
        {
            _mockChargingGroupRepository.Setup(p => p.GetChargingGroupById(It.IsAny<int>())).Throws(new Exception());
             var response = _chargingGroupBusiness.GetChargingGroupById(5);
             Assert.ThrowsAsync< Exception>(()=> response);
           
        }
        [Fact]
        public void Test_DeleteChargingGroupById_ShouldReturnSuccess_WhenRecordDeletedFromDatabase()
        {
            var group = new ChargingGroup()
            {
                GroupName = "Group1",
                GroupIdentifier = 1,
                CapacityInAmps = 100
            };
            bool isDeleted = true;
            _mockChargingGroupRepository.Setup(p => p.DeleteChargingGroupById(It.IsAny<int>())).ReturnsAsync(isDeleted);
            var response = _chargingGroupBusiness.DeleteChargingGroupById(5);
            Assert.NotNull(response);
            Assert.True(response.Result);
        }

        [Fact]
        public void Test_DeleteChargingGroupById_ShouldReturnDataNotFoundException_WhenRecordNotDeletedFromDatabase()
        {
            _mockChargingGroupRepository.Setup(p => p.DeleteChargingGroupById(It.IsAny<int>())).ThrowsAsync(new DataNotFoundException());
            var response = _chargingGroupBusiness.DeleteChargingGroupById(5);
            Assert.NotNull(response);
            Assert.ThrowsAsync<DataNotFoundException>(()=>response);
        }
        [Fact]
        public void Test_DeleteChargingGroupById_ShouldReturnException_WhenRecordNotDeletedFromDatabase()
        {
            _mockChargingGroupRepository.Setup(p => p.DeleteChargingGroupById(It.IsAny<int>())).ThrowsAsync(new Exception());
            var response = _chargingGroupBusiness.DeleteChargingGroupById(5);
            Assert.ThrowsAsync<Exception>(() => response);
        }

        [Fact]
        public void Test_UpdateChargingGroup_ShouldReturnSuccess_WhenRecordIsUpdated()
        {
            var updatedGroup = new ChargingGroup()
            {
                GroupName = "Group1",
                GroupIdentifier = 1,
                CapacityInAmps = 100
            };
            var originalGroup = new ChargingGroup()
            {
                GroupName = "Group2",
                GroupIdentifier = 2,
                CapacityInAmps = 50
            };
            _mockChargingGroupRepository.Setup(p => p.UpdateChargingGroup(It.IsAny<ChargingGroup>())).ReturnsAsync(updatedGroup);
            var response = _chargingGroupBusiness.UpdateChargingGroup(originalGroup);
            Assert.NotNull(response);
            Assert.Equal(response.Result.GroupName, updatedGroup.GroupName);
            Assert.Equal(response.Result.GroupIdentifier, updatedGroup.GroupIdentifier);
            Assert.Equal(response.Result.CapacityInAmps, updatedGroup.CapacityInAmps);
        }
        [Fact]
        public void Test_UpdateChargingGroup_ShouldReturnException_WhenExceptionOccured()
        {
            var originalGroup = new ChargingGroup()
            {
                GroupName = "Group2",
                GroupIdentifier = 2,
                CapacityInAmps = 50
            };
            _mockChargingGroupRepository.Setup(p => p.UpdateChargingGroup(It.IsAny<ChargingGroup>())).Throws(new Exception());
            var response = _chargingGroupBusiness.UpdateChargingGroup(originalGroup);
            Assert.ThrowsAsync<Exception>(()=>response);
        }

        [Fact]
        public void Test_UpdateChargingGroup_ShouldReturnDataNotFoundException_WhenRecordNotFound()
        {
            var originalGroup = new ChargingGroup()
            {
                GroupName = "Group2",
                GroupIdentifier = 2,
                CapacityInAmps = 50
            };
            _mockChargingGroupRepository.Setup(p => p.UpdateChargingGroup(It.IsAny<ChargingGroup>())).Throws(new DataNotFoundException());
            var response = _chargingGroupBusiness.UpdateChargingGroup(originalGroup);
            Assert.ThrowsAsync<DataNotFoundException>(() => response);
        }
    }
}
