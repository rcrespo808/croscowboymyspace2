using MediatR;
using POS.Data.Dto;
using System;

namespace POS.MediatR.BuzonSugerencias.Command
{
    public class GetBuzonSugerenciasQuery : IRequest<BuzonSugerenciasDto>
    {
        public Guid Id { get; set; }
    }
}
