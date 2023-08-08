using System;

namespace POS.Data.Dto;

public class PublicidadDto
{
    public Guid Id { get; set; }

    public string Nombre { get; set; }

    public string Link { get; set; }

    public string UrlBanner { get; set; }

    public DateTime CreatedDate { get; set; }
}
