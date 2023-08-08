using POS.Data.Dto;
using POS.MediatR.CommandAndQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using POS.MediatR.Unit.Commands;

namespace POS.API.Controllers.Unit
{
    [Route("api")]
    [ApiController]
    [Authorize]
    public class UnitController : BaseController
    {
        public IMediator _mediator { get; set; }

        public UnitController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get Unit.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Unit/{id}", Name = "GetUnit")]
        [Produces("application/json", "application/xml", Type = typeof(UnitDto))]
        public async Task<IActionResult> GetUnit(Guid id)
        {
            var getUnitCommand = new GetUnitCommand { Id = id };
            var result = await _mediator.Send(getUnitCommand);
            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Get Unit.
        /// </summary>
        /// <returns></returns>
        [HttpGet("Units")]
        [Produces("application/json", "application/xml", Type = typeof(List<UnitDto>))]
        public async Task<IActionResult> GetUnits()
        {
            var getAllUnitCommand = new GetAllUnitCommand { };
            var result = await _mediator.Send(getAllUnitCommand);
            return Ok(result);
        }

        /// <summary>
        /// Create Unit.
        /// </summary>
        /// <param name="addUnitCommand"></param>
        /// <returns></returns>
        [HttpPost("Unit")]
        [Produces("application/json", "application/xml", Type = typeof(UnitDto))]
        public async Task<IActionResult> AddUnit(AddUnitCommand addUnitCommand)
        {
            var response = await _mediator.Send(addUnitCommand);
            if (!response.Success)
            {
                return ReturnFormattedResponse(response);
            }
            return CreatedAtAction("GetUnit", new { id = response.Data.Id }, response.Data);
        }

        /// <summary>
        /// Update Unit.
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="updateUnitCommand"></param>
        /// <returns></returns>
        [HttpPut("Unit/{Id}")]
        [Produces("application/json", "application/xml", Type = typeof(UnitDto))]
        public async Task<IActionResult> UpdateUnit(Guid Id, UpdateUnitCommand updateUnitCommand)
        {
            updateUnitCommand.Id = Id;
            var result = await _mediator.Send(updateUnitCommand);
            return ReturnFormattedResponse(result);

        }

        /// <summary>
        /// Delete Unit.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("Unit/{Id}")]
        public async Task<IActionResult> DeleteUnit(Guid Id)
        {
            var deleteUnitCommand = new DeleteUnitCommand { Id = Id };
            var result = await _mediator.Send(deleteUnitCommand);
            return ReturnFormattedResponse(result);
        }
    }
}
