using System;
using System.ComponentModel.DataAnnotations;

namespace POS.Data;

public class Beneficios
{
    [Key]
    public Guid Id { get; set; }

    public string Nombre { get; set; }

    public bool TieneCupos { get; set; }

    public int Cupos { get; set; }

    public decimal Costo { get; set; }

    public decimal Descuento { get; set; }

    public string Color { get; set; }

    public string Descripcion { get; set; }

    public Guid? BeneficioCategoriaId { get; set; }

    public BeneficiosCategorias BeneficioCategoria { get; set; }
}