using MediatR;
using POS.Helper;
using System;

namespace POS.MediatR.Empresas.Command
{
    public class DeleteEmpresasCommand : IRequest<ServiceResponse<bool>>
    {
        public Guid Id { get; set; }
    }
}
