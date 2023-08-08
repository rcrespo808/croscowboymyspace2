using MediatR;
using POS.Data.Dto.Nosotros;
using System;

namespace POS.MediatR.CommandAndQuery
{
    public class GetNosotrosQuery : IRequest<NosotrosDto>
    {
        public Guid Id { get; set; }
    }
}
