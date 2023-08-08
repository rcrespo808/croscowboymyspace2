using POS.Data.Entities.Lookups;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS.Data;

public class Expone
{
    [Key]
    public long Id { get; set; }

    public Guid EventoId { get; set; }

    [NotMapped]
    public Eventos Evento { get; set; }

    public Guid CustomerId { get; set; }

    public Customer Customer { get; set; }
}