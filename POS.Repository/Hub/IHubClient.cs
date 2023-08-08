﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using POS.Data.Dto;

namespace POS.Repository
{
    public interface IHubClient
    {
        Task UserLeft(string id);

        Task NewOnlineUser(UserInfoToken userInfo);

        Task Joined(UserInfoToken userInfo);

        Task OnlineUsers(IEnumerable<UserInfoToken> userInfo);

        Task Logout(UserInfoToken userInfo);

        Task ForceLogout(UserInfoToken userInfo);

        Task SendDM(string message, UserInfoToken userInfo);

        Task SendNotification(Guid userId);


    }
}
