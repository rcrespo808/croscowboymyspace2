using MediatR;
using Microsoft.EntityFrameworkCore;
using POS.Data;
using POS.Helper;
using POS.MediatR.ServicioCategoria.Command;
using POS.Repository;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace POS.MediatR.Servicios.Command
{
    public class GetAllServiciosCategoriasCommandHandler : IRequestHandler<GetAllServiciosCategoriasCommand, List<ServiciosCategorias>>
    {
        private readonly IServiciosCategoriasRepository _servicioCategoriaRepository;
        private readonly PathHelper _pathHelper;
        private readonly IPropertyMappingService _propertyMappingService;

        public GetAllServiciosCategoriasCommandHandler(
            IServiciosCategoriasRepository servicioCategoriaRepository,
            PathHelper pathHelper,
            IPropertyMappingService propertyMappingService)
        {
            _servicioCategoriaRepository = servicioCategoriaRepository;
            _pathHelper = pathHelper;
            _propertyMappingService = propertyMappingService;
        }

        public async Task<List<ServiciosCategorias>> Handle(GetAllServiciosCategoriasCommand request, CancellationToken cancellationToken)
        {
            var serviciosCategorias = await _servicioCategoriaRepository
                .All
                .Select(b => (new ServiciosCategorias
                {
                    Id = b.Id,
                    Nombre = b.Nombre,
                    UrlImage = !string.IsNullOrWhiteSpace(b.UrlImage) ? Path.Combine(_pathHelper.ServicioCategoriaImagePath, b.UrlImage) : "",
                })
                ).ToListAsync();
            return serviciosCategorias;
        }
    }
}
