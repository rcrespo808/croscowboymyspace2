using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Helper.Constants
{
    public static class Constants
    {
        public const string SERVICIO_CAINCO = "SERVICIOS_CAINCO";
        
        public const string SERVICIO_EXTERNO = "SERVICIOS_EXTERNOS";
        
        public const string BENEFICIO_SOCIO = "BENEFICIOS_SOCIOS";
        
        public const string BENEFICIO_USUARIO = "BENEFICIOS_USUARIOS";

        public static Dictionary<string, Guid> Servicios = new Dictionary<string, Guid>()
        {
            {SERVICIO_CAINCO , Guid.Parse("f137e0e8-6561-41ad-86c7-9b908900800f") },
            {SERVICIO_EXTERNO , Guid.Parse("3fb0cf77-fa11-4262-8a9b-bd159e26e5bf") },
            {BENEFICIO_SOCIO , Guid.Parse("3b38f232-3036-4420-843c-c13b62030af1") },
            {BENEFICIO_USUARIO , Guid.Parse("1a3b09a5-e98c-4880-8663-dd7b6377bde3") },
        };
    }
}
