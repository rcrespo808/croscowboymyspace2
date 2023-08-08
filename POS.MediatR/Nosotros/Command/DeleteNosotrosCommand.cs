using MediatR;
using POS.Helper;
using System;
namespace POS.MediatR.Nosotros.Command
{
    public class DeleteNosotrosCommand : IRequest<ServiceResponse<bool>>
    {
        public Guid Id { get; set; }
    }
}
