using Microsoft.EntityFrameworkCore;
using POS.Data;
using POS.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POS.Repository
{
    public class ServiciosList : List<Servicios>
    {
        private readonly PathHelper _pathHelper;
        public ServiciosList(PathHelper pathHelper)
        {
            _pathHelper = pathHelper;
        }

        public int Skip { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }

        public ServiciosList(List<Servicios> items, int count, int skip, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            Skip = skip;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            AddRange(items);
        }

        public async Task<ServiciosList> Create(IQueryable<Data.Servicios> source, int skip, int pageSize)
        {
            var count = await GetCount(source);
            var dtoList = await GetDtos(source, skip, pageSize);
            var dtoPageList = new ServiciosList(dtoList, count, skip, pageSize);
            return dtoPageList;
        }

        public async Task<ServiciosList> CreateExternos(IQueryable<Data.Servicios> source, int skip, int pageSize)
        {
            var count = await GetCount(source);
            var dtoList = await GetDtosExternos(source, skip, pageSize);
            var dtoPageList = new ServiciosList(dtoList, count, skip, pageSize);
            return dtoPageList;
        }

        public async Task<int> GetCount(IQueryable<Data.Servicios> source)
        {
            return await source.AsNoTracking().CountAsync();
        }

        public async Task<List<Servicios>> GetDtos(IQueryable<Data.Servicios> source, int skip, int pageSize)
        {

            if (pageSize == 0)
            {
                var entities = await source
                                        .AsNoTracking()
                                        .Select(b => new Servicios
                                        {
                                            Id = b.Id,
                                            TipoServicio = b.TipoServicio,
                                            Nombre = b.Nombre,
                                            Celular = b.Celular,
                                            UrlImage = FileManager.GetPathFile(b.UrlImage, _pathHelper.ServiciosImagePath),
                                            UrlWhatsapp = b.UrlWhatsapp,
                                            UrlWeb = b.UrlWeb,
                                            TipoPago = b.TipoPago,
                                            Costo = b.Costo,
                                            CostoSocio = b.CostoSocio,
                                            Descripcion = b.Descripcion,
                                            ServicioCategoriaId = b.ServicioCategoriaId,
                                            ServicioCategoria = b.ServicioCategoria
                                        })
                                        .ToListAsync();
                return entities;
            }
            else
            {
                var entities = await source
                                       .Skip(skip)
                                       .Take(pageSize)
                                       .AsNoTracking()
                                       .Select(b => new Servicios
                                       {
                                           Id = b.Id,
                                           TipoServicio = b.TipoServicio,
                                           Nombre = b.Nombre,
                                           Celular = b.Celular,
                                           UrlImage = FileManager.GetPathFile(b.UrlImage, _pathHelper.ServiciosImagePath),
                                           UrlWhatsapp = b.UrlWhatsapp,
                                           UrlWeb = b.UrlWeb,
                                           TipoPago = b.TipoPago,
                                           Costo = b.Costo,
                                           CostoSocio = b.CostoSocio,
                                           Descripcion = b.Descripcion,
                                           ServicioCategoriaId = b.ServicioCategoriaId,
                                           ServicioCategoria = b.ServicioCategoria
                                       })
                                       .ToListAsync();
                return entities;
            }

        }

        public async Task<List<Servicios>> GetDtosExternos(IQueryable<Data.Servicios> source, int skip, int pageSize)
        {

            if (pageSize == 0)
            {
                var entities = await source
                                        .AsNoTracking()
                                        .Select(b => new Servicios
                                        {
                                            Id = b.Id,
                                            TipoServicio = b.TipoServicio,
                                            Nombre = b.Nombre,
                                            Celular = b.Celular,
                                            UrlImage = FileManager.GetPathFile(b.UrlImage, _pathHelper.ServiciosImagePath),
                                            UrlWhatsapp = b.UrlWhatsapp,
                                            UrlWeb = b.UrlWeb,
                                            TipoPago = b.TipoPago,
                                            Costo = b.Costo,
                                            CostoSocio = b.CostoSocio,
                                            Descripcion = b.Descripcion,
                                        })
                                        .ToListAsync();
                return entities;
            }
            else
            {
                var entities = await source
                                       .Skip(skip)
                                       .Take(pageSize)
                                       .AsNoTracking()
                                       .Select(b => new Servicios
                                       {
                                           Id = b.Id,
                                           TipoServicio = b.TipoServicio,
                                           Nombre = b.Nombre,
                                           Celular = b.Celular,
                                           UrlImage = FileManager.GetPathFile(b.UrlImage, _pathHelper.ServiciosImagePath),
                                           UrlWhatsapp = b.UrlWhatsapp,
                                           UrlWeb = b.UrlWeb,
                                           TipoPago = b.TipoPago,
                                           Costo = b.Costo,
                                           CostoSocio = b.CostoSocio,
                                           Descripcion = b.Descripcion
                                       })
                                       .ToListAsync();
                return entities;
            }

        }
    }
}
