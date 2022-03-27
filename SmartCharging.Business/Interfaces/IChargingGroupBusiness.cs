﻿//using SmartCharging.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SmartCharging.Business
{
    public interface IChargingGroupBusiness
    {
        Task<IEnumerable<ChargingGroup>> GetChargingGroups();
        Task<ChargingGroup> GetChargingGroupById(int groupId);
        Task<ChargingGroup> CreateChargingGroup(ChargingGroup chargingGroup);
        Task<ChargingGroup> UpdateChargingGroup(ChargingGroup chargingGroup);
        Task<bool> DeleteChargingGroupById(int id);
    }
}
