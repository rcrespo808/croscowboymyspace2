using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using POS.Common.UnitOfWork;
using POS.Data;
using POS.Domain;
using POS.Helper;
using POS.MediatR.Eventos.Command;
using POS.Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace POS.MediatR.Eventos.Handler
{
    internal class AddEventosCommandHandler : IRequestHandler<AddEventosCommand, ServiceResponse<bool>>
    {
        private readonly IEventosRepository _eventosRepository;
        private readonly IReminderRepository _reminderRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly PathHelper _pathHelper;
        public POSDbContext _context;
        private readonly IUnitOfWork<POSDbContext> _uow;

        public AddEventosCommandHandler(IEventosRepository eventosRepository,
            IMapper mapper,
            PathHelper pathHelper,
            IReminderRepository reminderRepository,
            IUserRepository userRepository,
            POSDbContext context,
            IUnitOfWork<POSDbContext> uow)
        {
            _reminderRepository = reminderRepository;
            _eventosRepository = eventosRepository;
            _mapper = mapper;
            _pathHelper = pathHelper;
            _userRepository = userRepository;
            _context = context;
            _uow = uow;
        }

        public async Task<ServiceResponse<bool>> Handle(AddEventosCommand request, CancellationToken cancellationToken)
        {
            var eventos = _mapper.Map<Data.Entities.Lookups.Eventos>(request);
            eventos.Id = Guid.NewGuid();
            if (eventos.Panelistas != null)
            {
                foreach (var pane in request.Panelistas)
                {
                    this._context.Add(new Expone
                    {
                        CustomerId = pane.Id,
                        EventoId = eventos.Id
                    });
                    _uow.Save();
                }
            }
            eventos.Panelistas = null;

            var reminder = new Reminder();
            reminder.Frequency = Data.Entities.Frequency.Daily;
            reminder.Subject = $"Evento - '{eventos.Titulo}'";
            reminder.Message = $"El evento {eventos.Titulo} esta cerca";
            reminder.StartDate = eventos.FechaInicial.AddDays(-5);
            reminder.EndDate = eventos.FechaInicial;
            reminder.DailyReminders = new List<DailyReminder>
            {
                new DailyReminder { IsActive = true, DayOfWeek = DayOfWeek.Sunday},
                new DailyReminder { IsActive = true, DayOfWeek = DayOfWeek.Monday},
                new DailyReminder { IsActive = true, DayOfWeek = DayOfWeek.Tuesday},
                new DailyReminder { IsActive = true, DayOfWeek = DayOfWeek.Wednesday},
                new DailyReminder { IsActive = true, DayOfWeek = DayOfWeek.Thursday},
                new DailyReminder { IsActive = true, DayOfWeek = DayOfWeek.Friday},
                new DailyReminder { IsActive = true, DayOfWeek = DayOfWeek.Saturday},
            };

            reminder.ReminderUsers = await _userRepository.All.Where(u => u.IsDeleted == false).Select(u => new ReminderUser
            {
                UserId = u.Id,
            }).ToListAsync();

            _reminderRepository.Add(reminder);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<bool>.Return500();
            }
            eventos.ReminderId = reminder.Id;

            _eventosRepository.Add(eventos);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<bool>.Return500();
            }

            await FileManager.SaveFile(eventos.Banner1, request.Banner1, _pathHelper.EventosBanners);
            await FileManager.SaveFile(eventos.Banner2, request.Banner2, _pathHelper.EventosBanners);
            await FileManager.SaveFile(eventos.Adjunto, request.Adjunto, _pathHelper.EventosDocuments);

            return ServiceResponse<bool>.ReturnSuccess();
        }
    }
}
