namespace POS.API.Helpers.Utils;

public class Utils
{
    public class Respuesta
    {
        public object _meta { get; set; }
        public object Items { get; set; }
        public object _links { get; set; }

        public static Respuesta Success(object data, object meta)
        {
            Respuesta res = new Respuesta
            {
                Items = data,
                _meta = meta,
                _links = null
            };
            return res;
        }
    }

    public class Meta
    {
        public int totalCount { get; set; }
        public int pageCount { get; set; }
        public int currentPage { get; set; }
        public int perPage { get; set; }
    }

    public class Links
    {
        public string self { get; set; }
        public string first { get; set; }
        public string last { get; set; }
        public string prev { get; set; }
    }

    public class Pages
    {
        public int inicio { get; set; }
        public int final { get; set; }
    }
    
    public class Responsex
    {
        public string mensaje { get; set; }
        public object data { get; set; }
        public string status { get; set; }

        public static Responsex Success(string sms, object data, string status = "INFO")
        {
            return new Responsex
            {
                mensaje = sms,
                data = data,
                status = status
            };
        }
    }
}