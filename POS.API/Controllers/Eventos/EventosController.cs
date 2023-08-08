using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POS.API.Helpers;
using POS.API.Helpers.Utils;
using POS.Common.UnitOfWork;
using POS.Data;
using POS.Data.Dto;
using POS.Data.Resources;
using POS.Domain;
using POS.MediatR.Eventos.Command;
using POS.MediatR.Nosotros.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace POS.API.Controllers.Eventos
{
    [Route("api/eventos")]
    [ApiController]
    public class EventosController : BaseController
    {
        public POSDbContext _context;
        private readonly IUnitOfWork<POSDbContext> _uow;
        private readonly IMediator _mediator;
        public readonly UserInfoToken _userInfo;

        public EventosController(POSDbContext context, IUnitOfWork<POSDbContext> uow, IMediator mediator, UserInfoToken userInfo)
        {
            _context = context;
            _uow = uow;
            _mediator = mediator;
            _userInfo = userInfo;
        }

        [HttpGet]
        [ClaimCheck("READ_EVENTO")]
        public JsonResult actionIndex(int page = 1, string search = "")
        {
            try
            {
                string userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                int perPage = 20;
                page--;
                Utils.Meta meta = new Utils.Meta();
                Utils.Links links = new Utils.Links();
                Utils.Respuesta respuesta = new Utils.Respuesta();
                string filter = string.Empty;
                if (search != "")
                {
                    filter = $" WHERE Titulo LIKE '%{search}%' OR  Descripcion LIKE '%{search}%'";
                }

                var query = _context.Eventos.FromSqlRaw($"SELECT * FROM eventosView {filter}").ToArray();
                if (query.Count() == 0)
                {
                    respuesta._meta = meta;
                    respuesta._links = links;
                    respuesta.Items = null;
                    return new JsonResult(respuesta);
                }

                int totalPages = query.Count();
                int pageCount = 0;
                double x = (double)(totalPages / (decimal)perPage);
                if ((x % 10) > 0)
                {
                    pageCount = (int)(Math.Truncate(x) + 1);
                }
                else
                {
                    pageCount = (int)Math.Truncate(x);
                }

                if (page >= pageCount)
                {
                    page = pageCount - 1;
                }

                meta.totalCount = totalPages;
                meta.perPage = perPage;
                meta.pageCount = pageCount;
                meta.currentPage = page;
                List<Utils.Pages> paginador = new List<Utils.Pages>();
                int p = 0;
                for (int i = 0; i < meta.pageCount; i++)
                {
                    paginador.Add(new Utils.Pages { inicio = p, final = perPage });
                    p = p + perPage;
                }

                links.first = $"?page=1&search={search}";
                links.last = $"?page={meta.pageCount}&search={search}";
                links.self = $"?page={meta.currentPage + 1}&search={search}";
                links.prev = $"?page={((meta.currentPage <= 0) ? 1 : meta.currentPage)}&search={search}";
                int inicio = paginador[meta.currentPage].inicio;
                int final = paginador[meta.currentPage].final;

                var eventosArr = new List<Data.Entities.Lookups.Eventos>();
                foreach (Data.Entities.Lookups.Eventos q in query.Skip(inicio).Take(final))
                {
                    q.Asistencia = this._context.Asistencia.Where(data => data.UsersId == new Guid(userId) && data.EventosId == q.Id).ToList();
                    eventosArr.Add(q);
                }

                respuesta._meta = meta;
                respuesta._links = links;
                respuesta.Items = eventosArr;
                return new JsonResult(respuesta);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// Get All Eventos
        /// </summary>
        /// <param name="eventosResource"></param>
        /// <returns></returns>
        [HttpGet("all")]
        [ClaimCheck("READ_EVENTO")]
        public async Task<IActionResult> GetAllEventos([FromQuery] EventosResource eventosResource)
        {
            var getAllEventosCommand = new GetAllEventosCommand
            {
                Resource = eventosResource
            };
            var result = await _mediator.Send(getAllEventosCommand);

            var paginationMetadata = new
            {
                totalCount = result.TotalCount,
                pageSize = result.PageSize,
                skip = result.Skip,
                totalPages = result.TotalPages
            };
            Response.Headers.Add("X-Pagination",
                Newtonsoft.Json.JsonConvert.SerializeObject(paginationMetadata));
            return Ok(new { Items = result });
        }

        /// <summary>
        /// Get Evento
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:Guid}")]
        [ClaimCheck("READ_EVENTO")]
        public async Task<IActionResult> GetEvento(Guid id)
        {
            var getEventosCommand = new GetEventosQuery { Id = id };
            var result = await _mediator.Send(getEventosCommand);
            return Ok(result);
        }

        /// <summary>
        /// Create Evento
        /// </summary>
        /// <param name="addEventosCommand"></param>
        /// <returns></returns>
        [HttpPost]
        [ClaimCheck("CREATE_EVENTO")]
        public async Task<IActionResult> CreateEvento(AddEventosCommand addEventosCommand)
        {
            var result = await _mediator.Send(addEventosCommand);
            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Update Evento
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="updateEventosCommand"></param>
        /// <returns></returns>
        [HttpPut("{id:Guid}")]
        [ClaimCheck("UPDATE_EVENTO")]
        public async Task<IActionResult> UpdateEvento(Guid Id, UpdateEventosCommand updateEventosCommand)
        {
            updateEventosCommand.Id = Id;
            var result = await _mediator.Send(updateEventosCommand);
            return ReturnFormattedResponse(result);
        }

        [HttpDelete("{id:Guid}")]
        [ClaimCheck("READ_EVENTO")]
        public JsonResult actionDelete(Guid id)
        {
            try
            {
                var eventos = new Data.Entities.Lookups.Eventos { Id = id };
                _context.Entry(eventos).State = EntityState.Deleted;
                _context.SaveChanges();
                return new JsonResult(1);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpGet("document/{id:Guid}")]
        [ClaimCheck("READ_EVENTO")]
        public JsonResult actionDocument(Guid id)
        {
            try
            {
                Data.Entities.Lookups.Eventos query = _context.Eventos.Where(data => data.Id == id).First();
                return new JsonResult(query.Adjunto);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpGet("expocitores/{id:Guid}")]
        [ClaimCheck("READ_EVENTO")]
        public JsonResult actionExpo(Guid id)
        {
            try
            {
                List<Data.Expositoriesview> query = _context.Expositoriesview.Where(data => data.evento == id).ToList();
                return new JsonResult(query);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// Get Asistentes de Evento por Id
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="EstadoCuenta"></param>
        /// <returns></returns>
        [HttpGet("{Id:Guid}/asistencia")]
        [ClaimCheck("READ_ASISTENTES")]
        public async Task<IActionResult> GetEventoAsistencias(Guid Id, [FromQuery] bool EstadoCuenta = false)
        {
            var getEventoAsistenciaCommand = new GetEventoAsistenciaQuery { Id = Id, EstadoCuenta = EstadoCuenta };
            var result = await _mediator.Send(getEventoAsistenciaCommand);
            return Ok(result);
        }

        [HttpGet("asistencia/{EventId:Guid}")]
        [ClaimCheck("READ_ASISTENTES")]
        public JsonResult actionAsistenciaCliente(Guid EventId)
        {
            Guid userId = Guid.Parse(_userInfo.Id);
            Asistencia query = _context.Asistencia.Where(data => data.UsersId == userId && data.EventosId == EventId).FirstOrDefault();
            string sms = (query is not null) ? "Cliente suscrito" : "Cliente sin suscripción";
            Utils.Responsex resp = Utils.Responsex.Success(sms, query, "OK");
            return new JsonResult(resp);
        }

        /// <summary>
        /// Save a user in an Event
        /// </summary>
        /// <param name="addEventoAsistenciaCommand"></param>
        /// <returns></returns>
        [HttpPost("asistencia")]
        [ClaimCheck("CREATE_ASISTENTES")]
        public async Task<IActionResult> AddEventoAsistencia(AddEventoAsistenciaCommand addEventoAsistenciaCommand)
        {
            var result = await _mediator.Send(addEventoAsistenciaCommand);
            return Ok(result);
        }
    }
}