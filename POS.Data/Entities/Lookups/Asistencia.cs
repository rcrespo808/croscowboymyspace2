using POS.Data.Entities.Lookups;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace POS.Data;

public class Asistencia // : BaseEntity
{
    [Key]
    public int Id { get; set; }

    public Guid UsersId { get; set; }

    public Guid EventosId { get; set; }

    public Guid? EstadoCuentaId { get; set; }

    public Eventos Evento { get; set; }

    public User User { get; set; }

    public EstadoCuenta EstadoCuenta { get; set; }
}