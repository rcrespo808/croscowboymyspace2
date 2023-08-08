using POS.Helper;
using MediatR;
using System;

namespace POS.MediatR.SalesOrder.Commands
{
    public class AppoveSalesOrderCommand : IRequest<ServiceResponse<bool>>
    {
        public Guid Id { get; set; }
    }
}
