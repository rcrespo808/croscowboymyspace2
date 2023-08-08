using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using POS.API.Helpers;
using POS.Data;
using POS.Data.Dto;
using POS.MediatR.Beneficios.Command;
using System;
using System.Threading.Tasks;

namespace POS.API.Controllers.BeneficiosUsuarios;
[Route("api/[controller]")]
[ApiController]
public class BeneficiosController : BaseController
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly Guid SociosId = Guid.Parse("08db51d6-0c5d-4c33-87d0-17b0f3cd5032");

    public BeneficiosController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    /// <summary>
    /// Obtener Beneficios Usuarios.
    /// </summary>
    /// <param name="resource"></param>
    /// <returns></returns>
    [HttpGet("usuarios")]
    [ClaimCheck("READ_BENF_USER")]
    public async Task<IActionResult> GetBeneficiosUsuarios([FromQuery] BeneficiosResource resource)
    {
        var getBeneficiosUsuariosCommand = new GetAllBeneficiosCommand { Resource = resource, IgnoreBeneficioCategoriaId = SociosId };
        var result = await _mediator.Send(getBeneficiosUsuariosCommand);
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
    /// Obtener Beneficios Socios.
    /// </summary>
    /// <param name="resource"></param>
    /// <returns></returns>
    [HttpGet("socios")]
    [ClaimCheck("READ_BENF_SOC")]
    public async Task<IActionResult> GetBeneficiosSocios([FromQuery] BeneficiosResource resource)
    {
        resource.BeneficioCategoriaId = SociosId;
        var getBeneficiosUsuariosCommand = new GetAllBeneficiosCommand { Resource = resource };
        var result = await _mediator.Send(getBeneficiosUsuariosCommand);
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
    /// Obtener Beneficio.
    /// </summary>
    /// <param name="Id"></param>
    /// <returns></returns>
    [HttpGet("{Id}")]
    [ClaimCheck("READ_BENF_SOC,READ_BENF_USER")]
    public async Task<IActionResult> GetBeneficio(Guid Id)
    {
        var getBeneficioCommand = new GetBeneficioCommand { Id = Id };
        var result = await _mediator.Send(getBeneficioCommand);
        return Ok(result);
    }

    /// <summary>
    /// Add Beneficio Usuario
    /// </summary>
    /// <param name="addBeneficioCommand"></param>
    /// <returns></returns>
    [HttpPost("usuarios")]
    [ClaimCheck("CREATE_BENF_USER")]
    public async Task<IActionResult> AddBeneficioUsuario(AddBeneficioCommand addBeneficioCommand)
    {
        var result = await _mediator.Send(addBeneficioCommand);
        return ReturnFormattedResponse(result);
    }

    /// <summary>
    /// Add Beneficio Socio
    /// </summary>
    /// <param name="beneficioSocio"></param>
    /// <returns></returns>
    [HttpPost("socios")]
    [ClaimCheck("CREATE_BENF_SOC")]
    public async Task<IActionResult> AddBeneficioSocio(BeneficioSocioDto beneficioSocio)
    {
        var addBeneficioCommand = _mapper.Map<AddBeneficioCommand>(beneficioSocio);
        addBeneficioCommand.BeneficioCategoriaId = SociosId;
        var result = await _mediator.Send(addBeneficioCommand);
        return ReturnFormattedResponse(result);
    }

    /// <summary>
    /// Update Beneficios Usuarios.
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="updateBeneficiosCommand"></param>
    /// <returns></returns>
    [HttpPut("usuarios/{Id}")]
    [ClaimCheck("UPDATE_BENF_USER")]
    public async Task<IActionResult> UpdateBeneficioUsuario(Guid Id, UpdateBeneficioCommand updateBeneficiosCommand)
    {
        updateBeneficiosCommand.Id = Id;
        var result = await _mediator.Send(updateBeneficiosCommand);
        return ReturnFormattedResponse(result);
    }

    /// <summary>
    /// Update Beneficios Socios.
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="beneficioSocio"></param>
    /// <returns></returns>
    [HttpPut("socios/{Id}")]
    [ClaimCheck("UPDATE_BENF_SOC")]
    public async Task<IActionResult> UpdateBeneficioSocio(Guid Id, BeneficioSocioDto beneficioSocio)
    {
        var updateBeneficiosCommand = _mapper.Map<UpdateBeneficioCommand>(beneficioSocio);
        updateBeneficiosCommand.Id = Id;
        updateBeneficiosCommand.BeneficioCategoriaId = SociosId;
        var result = await _mediator.Send(updateBeneficiosCommand);
        return ReturnFormattedResponse(result);
    }

    /// <summary>
    /// Delete Beneficio.
    /// </summary>
    /// <param name="Id"></param>
    /// <returns></returns>
    [HttpDelete("{Id}")]
    [ClaimCheck("DELETE_BENF_SOC,DELETE_BENF_USER")]
    public async Task<IActionResult> DeleteBeneficio(Guid Id)
    {
        var deleteBeneficioCommand = new DeleteBeneficioCommand { Id = Id };
        var result = await _mediator.Send(deleteBeneficioCommand);
        return ReturnFormattedResponse(result);
    }
}
