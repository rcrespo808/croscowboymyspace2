using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using POS.Common.UnitOfWork;
using POS.Data;
using POS.Data.Dto;
using POS.Domain;
using POS.Helper;
using POS.MediatR.Eventos.Command;
using POS.Repository;
using POS.Repository.EstadoCuenta;
using SixLabors.ImageSharp.ColorSpaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace POS.MediatR.Eventos.Handler
{
    internal class AddEventoAsistenciaCommandHandler : IRequestHandler<AddEventoAsistenciaCommand, object>
    {
        private readonly IReminderRepository _reminderRepository;
        private readonly IUserRepository _userRepository;
        private readonly IAsistenciaRepository _asistenciaRepository;
        private readonly IEventosRepository _eventosRepository;
        private readonly IMapper _mapper;
        private readonly IEstadoCuentaRepository _estadoCuentaRepository;
        private readonly IUnitOfWork<POSDbContext> _uow;
        private readonly UserInfoToken _userInfoToken;

        public AddEventoAsistenciaCommandHandler(
            IAsistenciaRepository asistenciaRepository,
            IEventosRepository eventosRepository,
            IMapper mapper,
            IReminderRepository reminderRepository,
            IUserRepository userRepository,
            IUnitOfWork<POSDbContext> uow, IEstadoCuentaRepository estadoCuentaRepository,
            UserInfoToken userInfoToken)
        {
            _reminderRepository = reminderRepository;
            _userRepository = userRepository;
            _asistenciaRepository = asistenciaRepository;
            _eventosRepository = eventosRepository;
            _estadoCuentaRepository = estadoCuentaRepository;
            _mapper = mapper;
            _userInfoToken = userInfoToken;
            _uow = uow;
        }

        public async Task<object> Handle(AddEventoAsistenciaCommand request, CancellationToken cancellationToken)
        {
            var asistencia = new Asistencia();
            object response;

            asistencia.EventosId = request.EventosId;
            asistencia.UsersId = request.UsersId.HasValue ? request.UsersId.Value : Guid.Parse(_userInfoToken.Id);

            var existEntity = _asistenciaRepository.All.Where(c => c.EventosId == asistencia.EventosId && c.UsersId == asistencia.UsersId).FirstOrDefault();
            var eventoEntity = _eventosRepository.All.Where(e => e.Id == asistencia.EventosId).FirstOrDefault();
            if (existEntity is not null)
            {
                var estadoCuentaEntity = _estadoCuentaRepository.All.Where(e => e.Id == existEntity.EstadoCuentaId).FirstOrDefault();
                _estadoCuentaRepository.Delete(estadoCuentaEntity);
                _asistenciaRepository.Remove(existEntity);
                response = new
                {
                    mensaje = "Asistencia Eliminada",
                    data = existEntity,
                    status = "OK"
                };
            }
            else
            {
                var estadoCuenta = _estadoCuentaRepository.SaveAndReturn(new Data.EstadoCuenta
                {
                    Nombre = $"Pago por evento : {eventoEntity.Titulo}",
                    FechaCobro = DateTime.Now.AddDays(10),
                    Estado = Helper.Enum.EstadoCuenta.TipoEstado.NO_CUMPLIDO,
                    Monto = eventoEntity.CostoComun,
                    IdUsuario = asistencia.UsersId,
                });

                if (await _uow.SaveAsync() <= 0)
                {
                    return new
                    {
                        mensaje = "Error",
                        status = 500
                    };
                }

                asistencia.EstadoCuentaId = estadoCuenta.Id;
                _asistenciaRepository.Add(asistencia);
                response = new
                {
                    mensaje = "Asistencia Registrada",
                    data = asistencia,
                    status = "OK"
                };

            }


            if (await _uow.SaveAsync() <= 0)
            {
                return new
                {
                    mensaje = "Error",
                    status = 500
                };
            }

            return response;
            //var reminder = new Reminder();
            //reminder.Frequency = Data.Entities.Frequency.Daily;
            //reminder.Subject = "Recordatorio de evento";
            //reminder.Message = "El evento esta cerca";
            //reminder.StartDate = DateTime.Now;
            //reminder.EndDate = DateTime.Now;
            //reminder.DailyReminders = new List<DailyReminder>
            //{
            //    new DailyReminder { IsActive = true, DayOfWeek = DayOfWeek.Sunday},
            //    new DailyReminder { IsActive = true, DayOfWeek = DayOfWeek.Monday},
            //    new DailyReminder { IsActive = true, DayOfWeek = DayOfWeek.Tuesday},
            //    new DailyReminder { IsActive = true, DayOfWeek = DayOfWeek.Wednesday},
            //    new DailyReminder { IsActive = true, DayOfWeek = DayOfWeek.Thursday},
            //    new DailyReminder { IsActive = true, DayOfWeek = DayOfWeek.Friday},
            //    new DailyReminder { IsActive = true, DayOfWeek = DayOfWeek.Saturday},
            //};

            //reminder.ReminderUsers = await _userRepository.All.Select(u => new ReminderUser
            //{
            //    UserId = u.Id
            //}).ToListAsync();

            //_reminderRepository.Add(reminder);

        }
    }
}
