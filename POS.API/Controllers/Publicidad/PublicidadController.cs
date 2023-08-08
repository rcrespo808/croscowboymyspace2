using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POS.API.Helpers;
using POS.Data;
using POS.Data.Dto;
using POS.MediatR.CommandAndQuery;
using POS.MediatR.Publicidad.Command;
using POS.MediatR.Publicidad.Commands;
using POS.Repository;
using System;
using System.Threading.Tasks;

namespace POS.API.Controllers.Publicidad
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PublicidadController : BaseController
    {
        private readonly IMediator _mediator;

        public PublicidadController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get Publicidad.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetPublicidad")]
        [ClaimCheck("READ_PUB")]
        [Produces("application/json", "application/xml", Type = typeof(PublicidadDto))]
        public async Task<IActionResult> GetPublicidad(Guid id)
        {
            var getPublicidadCommand = new GetPublicidadQuery { Id = id };
            var result = await _mediator.Send(getPublicidadCommand);
            return Ok(result);
        }

        /// <summary>
        /// Get All Inventory.
        /// </summary>
        /// <param name="publicidadResource"></param>
        /// <returns></returns>
        [HttpGet]
        [ClaimCheck("READ_PUB")]
        [Produces("application/json", "application/xml", Type = typeof(PublicidadList))]
        public async Task<IActionResult> GetAllPublicidad([FromQuery] PublicidadResource publicidadResource)
        {
            var getAllPublicidadCommand = new GetAllPublicidadCommand
            {
                Resource = publicidadResource
            };
            var result = await _mediator.Send(getAllPublicidadCommand);

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
        /// Add Publicidad
        /// </summary>
        /// <param name="addPublicidadCommand"></param>
        /// <returns></returns>
        [HttpPost]
        [ClaimCheck("CREATE_PUB")]
        [Produces("application/json", "application/xml", Type = typeof(PublicidadDto))]
        public async Task<IActionResult> AddPublicidad(AddPublicidadCommand addPublicidadCommand)
        {
            var result = await _mediator.Send(addPublicidadCommand);
            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Update Publicidad.
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="updatePublicidadCommand"></param>
        /// <returns></returns>
        [HttpPut("{Id}")]
        [ClaimCheck("UPDATE_PUB")]
        [Produces("application/json", "application/xml", Type = typeof(CountryDto))]
        public async Task<IActionResult> UpdatePublicidad(Guid Id, UpdatePublicidadCommand updatePublicidadCommand)
        {
            updatePublicidadCommand.Id = Id;
            var result = await _mediator.Send(updatePublicidadCommand);
            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Delete Publicidad.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("{Id}")]
        [ClaimCheck("DELETE_PUB")]
        public async Task<IActionResult> DeletePublicidad(Guid Id)
        {
            var deletePublicidadCommand = new DeletePublicidadCommand { Id = Id };
            var result = await _mediator.Send(deletePublicidadCommand);
            return ReturnFormattedResponse(result);
        }
    }
}
