using System;
using System.ComponentModel.DataAnnotations;

namespace POS.Data;

public class BeneficiosCategorias
{
    [Key]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "La Nombre socio no puede ser nulo.")]
    public string Nombre { get; set; }

    [Required(ErrorMessage = "La Descuento socio no puede ser nulo.")]

    public bool Descuento { get; set; }

    public string UrlBanner { get; set; }
}