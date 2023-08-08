using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS.Data
{
    public class Publicidad : BaseEntity
    {
        public Guid Id { get; set; }
        
        public string Nombre { get; set; }

        public string Link { get; set; }

        public string UrlBanner { get; set; }
    }
}
