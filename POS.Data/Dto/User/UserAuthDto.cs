﻿using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace POS.Data.Dto
{
    public class UserAuthDto
    {
        public UserAuthDto()
        {
            BearerToken = string.Empty;
        }
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string BearerToken { get; set; }
        public bool IsAuthenticated { get; set; }
        public string ProfilePhoto { get; set; }
        public List<AppClaimDto> Claims { get; set; }
        public string SessionId { get; set; }
        public string Version { get; set; }
        public int SessionTimeout { get; set; }
        public string OdataMetadata { get; set; }
        public Guid? CustomerId { get; set; }
        public Guid? SupplierId { get; set; }

    }
}
