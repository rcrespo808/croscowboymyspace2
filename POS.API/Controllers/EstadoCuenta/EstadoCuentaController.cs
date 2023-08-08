using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POS.Data;
using POS.Data.Dto;
using POS.Data.Resources;
using POS.MediatR.EstadoCuenta.Command;
using POS.MediatR.EstadoCuenta.Commands;
using POS.Repository.EstadoCuenta;
using System;
using System.Threading.Tasks;

namespace POS.API.Controllers.EstadoCuenta
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EstadoCuentaController : BaseController
    {
        private readonly IMediator _mediator;

        public EstadoCuentaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get EstadoCuenta.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetEstadoCuenta")]
        [Produces("application/json", "application/xml", Type = typeof(EstadoCuentaDto))]
        public async Task<IActionResult> GetEstadoCuenta(Guid id)
        {
            var getEstadoCuentaCommand = new GetEstadoCuentaQuery { Id = id };
            var result = await _mediator.Send(getEstadoCuentaCommand);
            return Ok(result);
        }

        /// <summary>
        /// Get All Estado Cuenta.
        /// </summary>
        /// <param name="estadoCuentaResource"></param>
        /// <returns></returns>
        [HttpGet]
        [Produces("application/json", "application/xml", Type = typeof(EstadoCuentaList))]
        public async Task<IActionResult> GetAllEstadoCuenta([FromQuery] EstadoCuentaResource estadoCuentaResource)
        {
            var getAllEstadoCuentaCommand = new GetAllEstadoCuentaCommand
            {
                Resource = estadoCuentaResource
            };
            var result = await _mediator.Send(getAllEstadoCuentaCommand);

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
        /// Add EstadoCuenta
        /// </summary>
        /// <param name="addEstadoCuentaCommand"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json", "application/xml", Type = typeof(EstadoCuentaDto))]
        public async Task<IActionResult> AddEstadoCuenta(AddEstadoCuentaCommand addEstadoCuentaCommand)
        {
            var result = await _mediator.Send(addEstadoCuentaCommand);
            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Update EstadoCuenta.
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="updateEstadoCuentaCommand"></param>
        /// <returns></returns>
        [HttpPut("{Id}")]
        [Produces("application/json", "application/xml", Type = typeof(CountryDto))]
        public async Task<IActionResult> UpdateEstadoCuenta(Guid Id, UpdateEstadoCuentaCommand updateEstadoCuentaCommand)
        {
            updateEstadoCuentaCommand.Id = Id;
            var result = await _mediator.Send(updateEstadoCuentaCommand);
            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Delete EstadoCuenta.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteEstadoCuenta(Guid Id)
        {
            var deleteEstadoCuentaCommand = new DeleteEstadoCuentaCommand { Id = Id };
            var result = await _mediator.Send(deleteEstadoCuentaCommand);
            return ReturnFormattedResponse(result);
        }
    }
}
