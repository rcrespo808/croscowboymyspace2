using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POS.API.Helpers;
using POS.Data;
using POS.Data.Dto;
using POS.Data.Dto.Nosotros;
using POS.Helper;
using POS.MediatR.CommandAndQuery;
using POS.MediatR.Country.Commands;
using POS.MediatR.Eventos.Command;
using POS.MediatR.Inventory.Command;
using POS.MediatR.Nosotros.Command;
using POS.MediatR.Nosotros.Commands;
using POS.Repository;
using System;
using System.Threading.Tasks;

namespace POS.API.Controllers.Nosotros
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NosotrosController : BaseController
    {
        private readonly IMediator _mediator;

        public NosotrosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get Nosotros.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetNosotros")]
        [ClaimCheck("READ_NOSO")]
        [Produces("application/json", "application/xml", Type = typeof(NosotrosDto))]
        public async Task<IActionResult> GetNosotros(Guid id)
        {
            var getNosotrosCommand = new GetNosotrosQuery { Id = id };
            var result = await _mediator.Send(getNosotrosCommand);
            return Ok(result);
        }

        /// <summary>
        /// Get All Inventory.
        /// </summary>
        /// <param name="nosotrosResource"></param>
        /// <returns></returns>
        [HttpGet]
        [ClaimCheck("READ_NOSO")]
        [Produces("application/json", "application/xml", Type = typeof(NosotrosList))]
        public async Task<IActionResult> GetAllNosotros([FromQuery] NosotrosResource nosotrosResource)
        {
            var getAllNosotrosCommand = new GetAllNosotrosCommand
            {
                Resource = nosotrosResource
            };
            var result = await _mediator.Send(getAllNosotrosCommand);

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
        /// Add Inventory
        /// </summary>
        /// <param name="addNosotrosCommand"></param>
        /// <returns></returns>
        [HttpPost]
        [ClaimCheck("CREATE_NOSO")]
        [Produces("application/json", "application/xml", Type = typeof(NosotrosDto))]
        public async Task<IActionResult> AddNosotros(AddNosotrosCommand addNosotrosCommand)
        {
            var result = await _mediator.Send(addNosotrosCommand);
            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Update Nosotros.
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="updateNosotrosCommand"></param>
        /// <returns></returns>
        [HttpPut("{Id}")]
        [ClaimCheck("UPDATE_NOSO")]
        [Produces("application/json", "application/xml", Type = typeof(CountryDto))]
        public async Task<IActionResult> UpdateNosotros(Guid Id, UpdateNosotrosCommand updateNosotrosCommand)
        {
            updateNosotrosCommand.Id = Id;
            var result = await _mediator.Send(updateNosotrosCommand);
            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Delete Nosotros.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("{Id}")]
        [ClaimCheck("DELETE_NOSO")]
        public async Task<IActionResult> DeleteNosotros(Guid Id)
        {
            var deleteNosotrosCommand = new DeleteNosotrosCommand { Id = Id };
            var result = await _mediator.Send(deleteNosotrosCommand);
            return ReturnFormattedResponse(result);
        }
    }
}
