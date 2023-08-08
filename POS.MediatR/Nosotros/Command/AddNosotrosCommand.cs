using MediatR;
using POS.Data.Dto;
using POS.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.MediatR.Nosotros.Command
{
    public class AddNosotrosCommand : IRequest<ServiceResponse<bool>>
    {
        public string Description { get; set; }
        public int Location { get; set; }
        public string ImageUrl { get; set; }
    }
}
