﻿using MediatR;
using POS.Helper;
using System;

namespace POS.MediatR.City.Commands
{
    public class DeleteEventosCommand: IRequest<ServiceResponse<bool>>
    {
        public Guid Id { get; set; }
    }
}
