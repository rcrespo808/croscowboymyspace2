using POS.Helper.Enum;
using System;
using System.ComponentModel.DataAnnotations;

namespace POS.Data;

public class Servicios
{
    [Key]
    public Guid Id { get; set; }
    
    public TipoServicio TipoServicio { get; set; }
   
    public string Nombre { get; set; }
    
    public string Celular { get; set; }

    public string UrlImage { get; set; }

    public string UrlWhatsapp { get; set; }

    public string UrlWeb { get; set; }

    public TipoPago TipoPago { get; set; }

    public decimal Costo { get; set; }

    public decimal CostoSocio { get; set; }
    
    public string Descripcion { get; set; }

    public Guid ServicioCategoriaId { get; set; }

    public ServiciosCategorias ServicioCategoria { get; set; }
}