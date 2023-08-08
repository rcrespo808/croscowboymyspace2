using MediatR;
using POS.Data.Dto.Empresas;
using System;

namespace POS.MediatR.Empresas.Command
{
    public class GetEmpresasQuery : IRequest<EmpresasDto>
    {
        public Guid Id { get; set; }
    }
}
