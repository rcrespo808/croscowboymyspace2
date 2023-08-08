using POS.Data.Dto;
using MediatR;
using System.Collections.Generic;
using System;
using System.ComponentModel;
using System.Reflection;
using System.Linq;

namespace POS.MediatR.CommandAndQuery
{
    public class GetAllProductCategoriesQuery : IRequest<List<ProductCategoryDto>>
    {
        public Guid? Id { get; set; }

        public string ProductCategoryCainco { get; set; }
    }
}
