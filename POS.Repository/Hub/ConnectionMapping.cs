using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using POS.Data.Dto;

namespace POS.Repository
{
    public class ConnectionMappingRepository : IConnectionMappingRepository
    {
        private bool _schedulerStatus = false;
        private bool _emailSchedulerStatus = false;

        private ConcurrentDictionary<string, UserInfoToken> _onlineUser { get; set; } = new ConcurrentDictionary<string, UserInfoToken>();
        public bool AddUpdate(UserInfoToken tempUserInfo, string connectionId)
        {
            var userAlreadyExists = _onlineUser.ContainsKey(tempUserInfo.Id);

            var userInfo = new UserInfoToken
            {
                Id = tempUserInfo.Id,
                ConnectionId = connectionId,
                Email = tempUserInfo.Email
            };

            _onlineUser.AddOrUpdate(tempUserInfo.Id, userInfo, (key, value) => userInfo);

            return userAlreadyExists;
        }
        public void Remove(UserInfoToken tempUserInfo)
        {
            UserInfoToken userInfo;
            _onlineUser.TryRemove(tempUserInfo.Id, out userInfo);
        }
        public IEnumerable<UserInfoToken> GetAllUsersExceptThis(UserInfoToken tempUserInfo)
        {
            return _onlineUser.Values.Where(item => item.Id != tempUserInfo.Id);
        }
        public UserInfoToken GetUserInfo(UserInfoToken tempUserInfo)
        {
            UserInfoToken user;
            _onlineUser.TryGetValue(tempUserInfo.Id, out user);
            return user;
        }

        public UserInfoToken GetUserInfoById(Guid userId)
        {
            UserInfoToken user;
            _onlineUser.TryGetValue(userId.ToString(), out user);
            return user;
        }
        public UserInfoToken GetUserInfoByName(string id)
        {
            UserInfoToken user;
            _onlineUser.TryGetValue(id, out user);
            return user;
        }
        public UserInfoToken GetUserInfoByConnectionId(string connectionId)
        {
            foreach (var onlineUser in _onlineUser)
            {
                var user = onlineUser.Value;
                if (user.ConnectionId == connectionId)
                {
                    return user;
                }
            }
            return null;
        }

        public void SetSchedulerServiceStatus(bool status)
        {
            _schedulerStatus = status;
        }

        public bool GetSchedulerServiceStatus()
        {
            return _schedulerStatus;
        }

        public void SetEmailSchedulerStatus(bool status)
        {
            _emailSchedulerStatus = status;
        }

        public bool GetEmailSchedulerStatus()
        {
            return _emailSchedulerStatus;
        }
    }
}
