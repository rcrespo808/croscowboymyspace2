using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POS.Data;
using POS.Data.Dto;
using POS.Data.Dto.Empresas;
using POS.MediatR.Empresas.Command;
using POS.MediatR.Empresas.Commands;
using POS.Repository.Empresas;
using System;
using System.Threading.Tasks;

namespace POS.API.Controllers.Empresas
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmpresasController : BaseController
    {
        private readonly IMediator _mediator;

        public EmpresasController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get Empresas.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetEmpresas")]
        [Produces("application/json", "application/xml", Type = typeof(EmpresasDto))]
        public async Task<IActionResult> GetEmpresas(Guid id)
        {
            var getEmpresasCommand = new GetEmpresasQuery { Id = id };
            var result = await _mediator.Send(getEmpresasCommand);
            return Ok(result);
        }

        /// <summary>
        /// Get All Inventory.
        /// </summary>
        /// <param name="publicidadResource"></param>
        /// <returns></returns>
        [HttpGet]
        [Produces("application/json", "application/xml", Type = typeof(EmpresasList))]
        public async Task<IActionResult> GetAllEmpresas([FromQuery] EmpresasResource publicidadResource)
        {
            var getAllEmpresasCommand = new GetAllEmpresasCommand
            {
                Resource = publicidadResource
            };
            var result = await _mediator.Send(getAllEmpresasCommand);

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
        /// Add Empresas
        /// </summary>
        /// <param name="addEmpresasCommand"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json", "application/xml", Type = typeof(EmpresasDto))]
        public async Task<IActionResult> AddEmpresas(AddEmpresasCommand addEmpresasCommand)
        {
            var result = await _mediator.Send(addEmpresasCommand);
            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Update Empresas.
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="updateEmpresasCommand"></param>
        /// <returns></returns>
        [HttpPut("{Id}")]
        [Produces("application/json", "application/xml", Type = typeof(CountryDto))]
        public async Task<IActionResult> UpdateEmpresas(Guid Id, UpdateEmpresasCommand updateEmpresasCommand)
        {
            updateEmpresasCommand.Id = Id;
            var result = await _mediator.Send(updateEmpresasCommand);
            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Delete Empresas.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteEmpresas(Guid Id)
        {
            var deleteEmpresasCommand = new DeleteEmpresasCommand { Id = Id };
            var result = await _mediator.Send(deleteEmpresasCommand);
            return ReturnFormattedResponse(result);
        }
    }
}
