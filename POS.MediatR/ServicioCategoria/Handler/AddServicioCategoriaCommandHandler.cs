using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using POS.Common.UnitOfWork;
using POS.Data;
using POS.Domain;
using POS.Helper;
using POS.MediatR.ServicioCategoria.Command;
using POS.Repository;
using System.Threading;
using System.Threading.Tasks;

namespace POS.MediatR.ServicioCategoria.Handler
{
    public class AddServicioCategoriaCommandHandler
        : IRequestHandler<AddServicioCategoriaCommand, ServiceResponse<ServiciosCategorias>>
    {

        private readonly IServiciosCategoriasRepository _servicioCategoriaRepository;
        private readonly PathHelper _pathHelper;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<POSDbContext> _uow;

        public AddServicioCategoriaCommandHandler(IServiciosCategoriasRepository servicioCategoriaRepository,
            IMapper mapper,
            PathHelper pathHelper,
            IUnitOfWork<POSDbContext> uow)
        {
            _servicioCategoriaRepository = servicioCategoriaRepository;
            _mapper = mapper;
            _pathHelper = pathHelper;
            _uow = uow;
        }
        public async Task<ServiceResponse<ServiciosCategorias>> Handle(AddServicioCategoriaCommand request, CancellationToken cancellationToken)
        {
            var existingServicioCategoria = await _servicioCategoriaRepository.All
                .FirstOrDefaultAsync(c => c.Nombre == request.Nombre);
            if (existingServicioCategoria != null)
            {
                return ServiceResponse<ServiciosCategorias>.Return409("Existe un Servicio Categoria con el mismo nombre");
            }

            var servicioCategoria = _mapper.Map<ServiciosCategorias>(request);
            _servicioCategoriaRepository.Add(servicioCategoria);

            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<ServiciosCategorias>.Return500();
            }

            await FileManager.SaveFile(servicioCategoria.UrlImage, request.UrlImageData, _pathHelper.ServicioCategoriaImagePath);
            servicioCategoria.UrlImage = FileManager.GetPathFile(servicioCategoria.UrlImage, _pathHelper.ServicioCategoriaImagePath);
            return ServiceResponse<ServiciosCategorias>.ReturnResultWith201(servicioCategoria);
        }
    }
}
