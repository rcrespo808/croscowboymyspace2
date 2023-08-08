using System;
using System.ComponentModel.DataAnnotations;

namespace POS.Data;

public class BeneficiosUsuarios
{
    [Key]
    public Guid Id { get; set; }
    [Required(ErrorMessage= "La Nombre socio no puede ser nulo.")]
    
    public string Nombre { get; set; } 
    
    [Required(ErrorMessage= "La cupos cupos no puede ser nulo.")]
    public bool TieneCupos { get; set; }

    [Required(ErrorMessage = "La cupos cupos no puede ser nulo.")]
    public int Cupos { get; set; }

    [Required(ErrorMessage= "La costo socio no puede ser nulo.")]
    public decimal Costo { get; set; } 
    
    public decimal Descuento { get; set; } 
    
    public string Descripcion { get; set; }

    public string Color { get; set; }

    [Required(ErrorMessage = "La Categoria socio no puede ser nulo.")]
    public Guid IdBeneficiosCategorias { get; set; }
}