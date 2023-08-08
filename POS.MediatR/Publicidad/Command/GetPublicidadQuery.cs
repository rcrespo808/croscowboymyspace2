using MediatR;
using POS.Data.Dto;
using System;

namespace POS.MediatR.CommandAndQuery
{
    public class GetPublicidadQuery : IRequest<PublicidadDto>
    {
        public Guid Id { get; set; }
    }
}
