using MediatR;
using POS.Data;
using System.Collections.Generic;

namespace POS.MediatR.ServicioCategoria.Command
{
    public class GetAllServiciosCategoriasCommand : IRequest<List<ServiciosCategorias>>
    {
    }
}
