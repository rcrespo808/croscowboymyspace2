using MediatR;
using POS.Data.Dto;
using POS.Helper;
using System;

namespace POS.MediatR.Nosotros.Commands
{
    public class UpdateNosotrosCommand : IRequest<ServiceResponse<bool>>
    {
        public Guid Id { get; set; }
        
        public string Title { get; set; }

        public string Description { get; set; }

        public int Location { get; set; }
        
        public string ImageUrl { get; set; }

        public bool IsImageChanged { get; set; }
    }
}
