using System;
using System.Collections.Generic;
using POS.Data.Dto;

namespace POS.Repository
{
    public interface IConnectionMappingRepository
    {
        bool AddUpdate(UserInfoToken tempUserInfo, string connectionId);
        void Remove(UserInfoToken tempUserInfo);
        IEnumerable<UserInfoToken> GetAllUsersExceptThis(UserInfoToken tempUserInfo);
        UserInfoToken GetUserInfo(UserInfoToken tempUserInfo);
        UserInfoToken GetUserInfoByName(string id);
        UserInfoToken GetUserInfoById(Guid userId);
        UserInfoToken GetUserInfoByConnectionId(string connectionId);
        void SetSchedulerServiceStatus(bool status);
        bool GetSchedulerServiceStatus();
        void SetEmailSchedulerStatus(bool status);
        bool GetEmailSchedulerStatus();
    }
}
