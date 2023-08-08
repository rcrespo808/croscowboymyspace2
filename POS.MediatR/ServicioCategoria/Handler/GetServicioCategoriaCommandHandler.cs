using MediatR;
using Microsoft.EntityFrameworkCore;
using POS.Data;
using POS.Helper;
using POS.MediatR.ServicioCategoria.Command;
using POS.Repository;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace POS.MediatR.Servicios.Command
{
    public class GetServicioCategoriaCommandHandler : IRequestHandler<GetServicioCategoriaCommand, ServiciosCategorias>
    {
        private readonly IServiciosCategoriasRepository _servicioCategoriaRepository;
        private readonly PathHelper _pathHelper;
        private readonly IPropertyMappingService _propertyMappingService;

        public GetServicioCategoriaCommandHandler(
            IServiciosCategoriasRepository servicioCategoriaRepository,
            PathHelper pathHelper,
            IPropertyMappingService propertyMappingService)
        {
            _servicioCategoriaRepository = servicioCategoriaRepository;
            _pathHelper = pathHelper;
            _propertyMappingService = propertyMappingService;
        }

        public async Task<ServiciosCategorias> Handle(GetServicioCategoriaCommand request, CancellationToken cancellationToken)
        {
            var servicioCategoria = await _servicioCategoriaRepository
                .All
                .Where(b => b.Id == request.Id)
                .Select(b => (new ServiciosCategorias
                {
                    Id = b.Id,
                    Nombre = b.Nombre,
                    UrlImage = !string.IsNullOrWhiteSpace(b.UrlImage) ? Path.Combine(_pathHelper.ServicioCategoriaImagePath, b.UrlImage) : "",
                })
                ).FirstOrDefaultAsync();
            return servicioCategoria;
        }
    }
}
