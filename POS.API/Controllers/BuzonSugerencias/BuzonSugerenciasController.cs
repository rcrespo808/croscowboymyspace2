using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POS.API.Helpers;
using POS.Data.Dto;
using POS.Data.Resources;
using POS.MediatR.BuzonSugerencias.Command;
using POS.MediatR.BuzonSugerencias.Commands;
using POS.Repository.BuzonSugerencias;
using System;
using System.Threading.Tasks;

namespace POS.API.Controllers.BuzonSugerencias
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BuzonSugerenciasController : BaseController
    {
        private readonly IMediator _mediator;

        public BuzonSugerenciasController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get BuzonSugerencias.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetBuzonSugerencias")]
        [ClaimCheck("READ_SUG")]
        [Produces("application/json", "application/xml", Type = typeof(BuzonSugerenciasDto))]
        public async Task<IActionResult> GetBuzonSugerencias(Guid id)
        {
            var getBuzonSugerenciasCommand = new GetBuzonSugerenciasQuery { Id = id };
            var result = await _mediator.Send(getBuzonSugerenciasCommand);
            return Ok(result);
        }

        /// <summary>
        /// Get All Inventory.
        /// </summary>
        /// <param name="buzonSugerenciasResource"></param>
        /// <returns></returns>
        [HttpGet]
        [ClaimCheck("READ_SUG")]
        [Produces("application/json", "application/xml", Type = typeof(BuzonSugerenciasList))]
        public async Task<IActionResult> GetAllBuzonSugerencias([FromQuery] BuzonSugerenciasResource buzonSugerenciasResource)
        {
            var getAllBuzonSugerenciasCommand = new GetAllBuzonSugerenciasCommand
            {
                Resource = buzonSugerenciasResource
            };
            var result = await _mediator.Send(getAllBuzonSugerenciasCommand);

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
        /// Add BuzonSugerencias
        /// </summary>
        /// <param name="addBuzonSugerenciasCommand"></param>
        /// <returns></returns>
        [HttpPost]
        [ClaimCheck("CREATE_SUG")]
        [Produces("application/json", "application/xml", Type = typeof(BuzonSugerenciasDto))]
        public async Task<IActionResult> AddBuzonSugerencias(AddBuzonSugerenciasCommand addBuzonSugerenciasCommand)
        {
            var result = await _mediator.Send(addBuzonSugerenciasCommand);
            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Update BuzonSugerencias.
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="updateBuzonSugerenciasCommand"></param>
        /// <returns></returns>
        [HttpPut("{Id}")]
        [ClaimCheck("UPDATE_SUG")]
        [Produces("application/json", "application/xml", Type = typeof(CountryDto))]
        public async Task<IActionResult> UpdateBuzonSugerencias(Guid Id, UpdateBuzonSugerenciasCommand updateBuzonSugerenciasCommand)
        {
            updateBuzonSugerenciasCommand.Id = Id;
            var result = await _mediator.Send(updateBuzonSugerenciasCommand);
            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Delete BuzonSugerencias.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("{Id}")]
        [ClaimCheck("DELETE_SUG")]
        public async Task<IActionResult> DeleteBuzonSugerencias(Guid Id)
        {
            var deleteBuzonSugerenciasCommand = new DeleteBuzonSugerenciasCommand { Id = Id };
            var result = await _mediator.Send(deleteBuzonSugerenciasCommand);
            return ReturnFormattedResponse(result);
        }
    }
}
