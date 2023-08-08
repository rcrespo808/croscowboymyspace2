using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Data.Dto.Nosotros
{
    public class NosotrosDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int Location { get; set; }

        public string ImageUrl { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
