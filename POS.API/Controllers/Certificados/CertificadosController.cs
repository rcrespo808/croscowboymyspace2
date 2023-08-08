using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POS.API.Helpers;
using POS.Data.Dto;
using POS.Data.Resources;
using POS.MediatR.Certificados.Command;
using POS.MediatR.Certificados.Commands;
using POS.Repository.Certificados;
using System;
using System.Threading.Tasks;

namespace POS.API.Controllers.Certificados
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CertificadosController : BaseController
    {
        private readonly IMediator _mediator;

        public CertificadosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get Certificados.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetCertificados")]
        [ClaimCheck("READ_CERT")]
        [Produces("application/json", "application/xml", Type = typeof(CertificadosDto))]
        public async Task<IActionResult> GetCertificados(Guid id)
        {
            var getCertificadosCommand = new GetCertificadosQuery { Id = id };
            var result = await _mediator.Send(getCertificadosCommand);
            return Ok(result);
        }

        /// <summary>
        /// Get All Inventory.
        /// </summary>
        /// <param name="certificadosResource"></param>
        /// <returns></returns>
        [HttpGet]
        [ClaimCheck("READ_CERT")]
        [Produces("application/json", "application/xml", Type = typeof(CertificadosList))]
        public async Task<IActionResult> GetAllCertificados([FromQuery] CertificadosResource certificadosResource)
        {
            var getAllCertificadosCommand = new GetAllCertificadosCommand
            {
                Resource = certificadosResource
            };
            var result = await _mediator.Send(getAllCertificadosCommand);

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
        /// Add Certificados
        /// </summary>
        /// <param name="addCertificadosCommand"></param>
        /// <returns></returns>
        [HttpPost]
        [ClaimCheck("CREATE_CERT")]
        [Produces("application/json", "application/xml", Type = typeof(CertificadosDto))]
        public async Task<IActionResult> AddCertificados(AddCertificadosCommand addCertificadosCommand)
        {
            var result = await _mediator.Send(addCertificadosCommand);
            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Update Certificados.
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="updateCertificadosCommand"></param>
        /// <returns></returns>
        [HttpPut("{Id}")]
        [ClaimCheck("UPDATE_CERT")]
        [Produces("application/json", "application/xml", Type = typeof(CountryDto))]
        public async Task<IActionResult> UpdateCertificados(Guid Id, UpdateCertificadosCommand updateCertificadosCommand)
        {
            updateCertificadosCommand.Id = Id;
            var result = await _mediator.Send(updateCertificadosCommand);
            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Delete Certificados.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("{Id}")]
        [ClaimCheck("DELETE_CERT")]
        public async Task<IActionResult> DeleteCertificados(Guid Id)
        {
            var deleteCertificadosCommand = new DeleteCertificadosCommand { Id = Id };
            var result = await _mediator.Send(deleteCertificadosCommand);
            return ReturnFormattedResponse(result);
        }
    }
}
