using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POS.Data;
using POS.Data.Dto;
using POS.Data.Resources;
using POS.MediatR.Facturas.Command;
using POS.MediatR.Facturas.Commands;
using POS.Repository.Facturas;
using System;
using System.Threading.Tasks;

namespace POS.API.Controllers.Facturas
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FacturasController : BaseController
    {
        private readonly IMediator _mediator;

        public FacturasController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get Facturas.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetFacturas")]
        [Produces("application/json", "application/xml", Type = typeof(FacturasDto))]
        public async Task<IActionResult> GetFacturas(Guid id)
        {
            var getFacturasCommand = new GetFacturasQuery { Id = id };
            var result = await _mediator.Send(getFacturasCommand);
            return Ok(result);
        }

        /// <summary>
        /// Get All Facturas.
        /// </summary>
        /// <param name="facturasResource"></param>
        /// <returns></returns>
        [HttpGet]
        [Produces("application/json", "application/xml", Type = typeof(FacturasList))]
        public async Task<IActionResult> GetAllFacturas([FromQuery] FacturasResource facturasResource)
        {
            var getAllFacturasCommand = new GetAllFacturasCommand
            {
                Resource = facturasResource
            };
            var result = await _mediator.Send(getAllFacturasCommand);

            var paginationMetadata = new
            {
                totalCount = result.TotalCount,
                pageSize = result.PageSize,
                skip = result.Skip,
                totalPages = result.TotalPages
            };
            Response.Headers.Add("X-Pagination",
                Newtonsoft.Json.JsonConvert.SerializeObject(paginationMetadata));
            return Ok(result);
        }

        /// <summary>
        /// Add Facturas
        /// </summary>
        /// <param name="addFacturasCommand"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json", "application/xml", Type = typeof(FacturasDto))]
        public async Task<IActionResult> AddFacturas(AddFacturasCommand addFacturasCommand)
        {
            var result = await _mediator.Send(addFacturasCommand);
            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Update Facturas.
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="updateFacturasCommand"></param>
        /// <returns></returns>
        [HttpPut("{Id}")]
        [Produces("application/json", "application/xml", Type = typeof(CountryDto))]
        public async Task<IActionResult> UpdateFacturas(Guid Id, UpdateFacturasCommand updateFacturasCommand)
        {
            updateFacturasCommand.Id = Id;
            var result = await _mediator.Send(updateFacturasCommand);
            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Delete Facturas.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteFacturas(Guid Id)
        {
            var deleteFacturasCommand = new DeleteFacturasCommand { Id = Id };
            var result = await _mediator.Send(deleteFacturasCommand);
            return ReturnFormattedResponse(result);
        }
    }
}
