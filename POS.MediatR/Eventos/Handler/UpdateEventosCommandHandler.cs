using Amazon.Runtime.Internal;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using POS.Common.UnitOfWork;
using POS.Data;
using POS.Data.Entities.Lookups;
using POS.Domain;
using POS.Helper;
using POS.MediatR.Eventos.Command;
using POS.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace POS.MediatR.Eventos.Handler
{
    public class UpdateEventosCommandHandler : IRequestHandler<UpdateEventosCommand, ServiceResponse<bool>>
    {
        private readonly IEventosRepository _eventosRepository;
        private readonly IReminderRepository _reminderRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork<POSDbContext> _uow;
        private readonly PathHelper _pathHelper;
        private readonly IMapper _mapper;
        public POSDbContext _context;
        private readonly ILogger<UpdateEventosCommandHandler> _logger;
        public UpdateEventosCommandHandler(
            IEventosRepository eventosRepository,
            IMapper mapper,
            IReminderRepository reminderRepository,
            IUserRepository userRepository,
            PathHelper pathHelper,
            IUnitOfWork<POSDbContext> uow,
            POSDbContext context,
            ILogger<UpdateEventosCommandHandler> logger)
        {
            _eventosRepository = eventosRepository;
            _reminderRepository = reminderRepository;
            _mapper = mapper;
            _pathHelper = pathHelper;
            _context = context;
            _userRepository = userRepository;
            _uow = uow;
            _logger = logger;
        }

        public async Task<ServiceResponse<bool>> Handle(UpdateEventosCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await _eventosRepository.FindBy(c => c.Id == request.Id).FirstOrDefaultAsync();

            var oldBanner1Url = entityExist.Banner1;
            var oldBanner2Url = entityExist.Banner2;
            var oldAdjuntoUrl = entityExist.Adjunto;

            //entityExist.HoraInicial = request.HoraInicial;
            //entityExist.FechaInicial = request.FechaInicial;
            //entityExist.HoraFinal = request.HoraFinal;
            //entityExist.FechaFinal = request.FechaFinal;
            //entityExist.CostoSocios = request.CostoSocios;
            //entityExist.CostoComun = request.CostoComun;
            //entityExist.Ubicacion = request.Ubicacion;
            //entityExist.Lat = request.Lat;
            //entityExist.Lng = request.Lng;
            //entityExist.Link = request.Link;
            //entityExist.EstadoLInk = request.EstadoLInk;
            //entityExist.Titulo = request.Titulo;
            //entityExist.Descripcion = request.Descripcion;
            //entityExist.Banner1 = FileManager.GetUpdateFile(request.IsBanner1Changed, entityExist.Banner1, request.Banner1, "png");
            //entityExist.Banner2 = FileManager.GetUpdateFile(request.IsBanner2Changed, entityExist.Banner2, request.Banner2, "png");
            //entityExist.Adjunto = FileManager.GetUpdateFile(request.IsAdjuntoChanged, entityExist.Adjunto, request.Adjunto, "pdf");

            entityExist = _mapper.Map(request, entityExist);

            var reminder = await _reminderRepository.All.Where(r => r.Id == entityExist.ReminderId).FirstOrDefaultAsync();
            if (reminder is not null)
            {
                reminder.Subject = $"Evento - '{entityExist.Titulo}'";
                reminder.Message = $"El evento {entityExist.Titulo} esta cerca";
                reminder.StartDate = entityExist.FechaInicial.AddDays(-5);
                reminder.EndDate = entityExist.FechaInicial;

                _reminderRepository.Update(reminder);
                if (await _uow.SaveAsync() <= 0)
                {
                    return ServiceResponse<bool>.Return500();
                }
            }
            else
            {
                var newReminder = new Reminder();
                newReminder.Frequency = Data.Entities.Frequency.Daily;
                newReminder.Subject = $"Evento - '{entityExist.Titulo}'";
                newReminder.Message = $"El evento {entityExist.Titulo} esta cerca";
                newReminder.StartDate = entityExist.FechaInicial.AddDays(-5);
                newReminder.EndDate = entityExist.FechaInicial;
                newReminder.DailyReminders = new List<DailyReminder>
                {
                    new DailyReminder { IsActive = true, DayOfWeek = DayOfWeek.Sunday},
                    new DailyReminder { IsActive = true, DayOfWeek = DayOfWeek.Monday},
                    new DailyReminder { IsActive = true, DayOfWeek = DayOfWeek.Tuesday},
                    new DailyReminder { IsActive = true, DayOfWeek = DayOfWeek.Wednesday},
                    new DailyReminder { IsActive = true, DayOfWeek = DayOfWeek.Thursday},
                    new DailyReminder { IsActive = true, DayOfWeek = DayOfWeek.Friday},
                    new DailyReminder { IsActive = true, DayOfWeek = DayOfWeek.Saturday},
                };

                newReminder.ReminderUsers = await _userRepository.All.Where(u => u.IsDeleted == false).Select(u => new ReminderUser
                {
                    UserId = u.Id,
                }).ToListAsync();
                _reminderRepository.Add(newReminder);
                if (await _uow.SaveAsync() <= 0)
                {
                    return ServiceResponse<bool>.Return500();
                }
                entityExist.ReminderId = newReminder.Id;
            }

            _eventosRepository.Update(entityExist);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<bool>.Return500();
            }

            // eliminacion de expocitores
            List<Expone> exponex = this._context.Expone.Where(x => x.EventoId == entityExist.Id).ToList();
            foreach (var entity in exponex)
                this._context.Expone.Remove(entity);
            this._context.SaveChanges();

            // nuevos expositoes
            if (request.Panelistas != null)
            {
                foreach (var pane in request.Panelistas)
                {
                    this._context.Add(new Expone
                    {
                        CustomerId = pane.Id,
                        EventoId = entityExist.Id
                    });
                    _uow.Save();
                }
            }
            this._context.SaveChanges();

            await FileManager.UpdateFile(request.IsBanner1Changed, _pathHelper.EventosBanners, entityExist.Banner1, request.Banner1, oldBanner1Url);
            await FileManager.UpdateFile(request.IsBanner2Changed, _pathHelper.EventosBanners, entityExist.Banner2, request.Banner2, oldBanner2Url);
            await FileManager.UpdateFile(request.IsAdjuntoChanged, _pathHelper.EventosDocuments, entityExist.Adjunto, request.Adjunto, oldAdjuntoUrl);
            return ServiceResponse<bool>.ReturnResultWith200(true);
        }
    }
}
