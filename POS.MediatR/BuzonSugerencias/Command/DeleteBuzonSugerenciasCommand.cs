using MediatR;
using POS.Helper;
using System;

namespace POS.MediatR.BuzonSugerencias.Command
{
    public class DeleteBuzonSugerenciasCommand : IRequest<ServiceResponse<bool>>
    {
        public Guid Id { get; set; }
    }
}
