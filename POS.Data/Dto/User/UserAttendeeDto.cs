using System;
using System.Collections.Generic;

namespace POS.Data.Dto
{
    public class UserAttendeeDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string ProfilePhoto { get; set; }
        public string Provider { get; set; }

        public Guid EventosId { get; set; }

        public EstadoCuentaDto EstadoCuenta { get; set; }
    }
}
