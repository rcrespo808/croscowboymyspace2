using POS.Helper.Enum;
using System;
using System.ComponentModel.DataAnnotations;

namespace POS.Data;

public class ServiciosCategorias
{
    [Key]
    public Guid Id { get; set; }

    public string Nombre { get; set; }

    public string UrlImage { get; set; }
}