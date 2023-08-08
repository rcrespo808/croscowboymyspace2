using MediatR;
using Microsoft.EntityFrameworkCore;
using POS.Helper;
using POS.Repository;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace POS.MediatR.Servicios.Command
{
    public class GetServicioCommandHandler : IRequestHandler<GetServicioCommand, Data.Servicios>
    {
        private readonly IServiciosRepository _serviciosRepository;
        private readonly PathHelper _pathHelper;
        private readonly IPropertyMappingService _propertyMappingService;

        public GetServicioCommandHandler(
            IServiciosRepository serviciosRepository,
            PathHelper pathHelper,
            IPropertyMappingService propertyMappingService)
        {
            _serviciosRepository = serviciosRepository;
            _pathHelper = pathHelper;
            _propertyMappingService = propertyMappingService;
        }

        public async Task<Data.Servicios> Handle(GetServicioCommand request, CancellationToken cancellationToken)
        {
            var collectionBeforePaging = _serviciosRepository
                .All
                .Where(b => b.Id == request.Id)
                .AsNoTracking();

            if(collectionBeforePaging.AsNoTracking().FirstOrDefault().ServicioCategoriaId != Guid.Empty)
            {
                collectionBeforePaging = collectionBeforePaging.Include(b => b.ServicioCategoria);
            }

            var servicio = await collectionBeforePaging.FirstOrDefaultAsync();
            servicio.UrlImage = FileManager.GetPathFile(servicio.UrlImage, _pathHelper.ServiciosImagePath);
            return servicio;
        }
    }
}
