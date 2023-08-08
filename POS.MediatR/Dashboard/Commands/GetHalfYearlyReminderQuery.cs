﻿using POS.Data.Dto;
using MediatR;
using System.Collections.Generic;

namespace POS.MediatR.CommandAndQuery
{
    public class GetHalfYearlyReminderQuery : IRequest<List<CalenderReminderDto>>
    {
        public int Month { get; set; }
        public int Year { get; set; }
    }
}
