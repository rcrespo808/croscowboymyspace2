using MediatR;
using POS.Data;
using POS.Repository.Empresas;

namespace POS.MediatR.Empresas.Command
{
    public class GetAllEmpresasCommand : IRequest<EmpresasList>
    {
        public EmpresasResource Resource { get; set; }
    }
}
