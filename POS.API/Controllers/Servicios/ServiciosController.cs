using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using POS.API.Helpers;
using POS.Data;
using POS.Data.Dto.Servicio;
using POS.Helper.Enum;
using POS.MediatR.Servicios.Command;
using System;
using System.Threading.Tasks;

namespace POS.API.Controllers.Servicios;
[Route("api/[controller]")]
[ApiController]
public class ServiciosController : BaseController
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public ServiciosController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    /// <summary>
    /// Obtener Servicios Cainco.
    /// </summary>
    /// <param name="resource"></param>
    /// <returns></returns>
    [HttpGet("cainco")]
    [ClaimCheck("READ_SERVICIO_CAINCO")]
    public async Task<IActionResult> GetServiciosCainco([FromQuery] ServiciosResource resource)
    {
        var getServiciosCaincoCommand = new GetAllServiciosCommand { Resource = resource, TipoServicio = TipoServicio.CAINCO };
        var result = await _mediator.Send(getServiciosCaincoCommand);
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
    /// Obtener Servicios Externos.
    /// </summary>
    /// <param name="resource"></param>
    /// <returns></returns>
    [HttpGet("externos")]
    [ClaimCheck("READ_SERVICIO_EXTERNO")]
    public async Task<IActionResult> GetServiciosExternos([FromQuery] ServiciosExternosResource resource)
    {
        var getServiciosExternoCommand = new GetAllServiciosExternosCommand { Resource = resource, TipoServicio = TipoServicio.EXTERNO };
        var result = await _mediator.Send(getServiciosExternoCommand);
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
    /// Obtener Servicio.
    /// </summary>
    /// <param name="Id"></param>
    /// <returns></returns>
    [HttpGet("{Id}")]
    public async Task<IActionResult> GetServicio(Guid Id)
    {
        var getServicioCommand = new GetServicioCommand { Id = Id };
        var result = await _mediator.Send(getServicioCommand);
        return Ok(result);
    }

    /// <summary>
    /// Add Servicio Usuario
    /// </summary>
    /// <param name="servicioCainco"></param>
    /// <returns></returns>
    [HttpPost("cainco")]
    [ClaimCheck("CREATE_SERVICIO_CAINCO")]
    public async Task<IActionResult> AddServicioCainco(ServicioCaincoDto servicioCainco)
    {
        var addServicioCommand = _mapper.Map<AddServicioCommand>(servicioCainco);
        addServicioCommand.TipoServicio = TipoServicio.CAINCO;
        var result = await _mediator.Send(addServicioCommand);
        return ReturnFormattedResponse(result);
    }

    /// <summary>
    /// Add Servicio Externo
    /// </summary>
    /// <param name="servicioExterno"></param>
    /// <returns></returns>
    [HttpPost("externos")]
    [ClaimCheck("CREATE_SERVICIO_EXTERNO")]
    public async Task<IActionResult> AddServicioExterno(ServicioExternoDto servicioExterno)
    {
        var addServicioCommand = _mapper.Map<AddServicioCommand>(servicioExterno);
        addServicioCommand.TipoServicio = TipoServicio.EXTERNO;
        addServicioCommand.ServicioCategoriaId = Guid.Empty;
        var result = await _mediator.Send(addServicioCommand);
        return ReturnFormattedResponse(result);
    }

    /// <summary>
    /// Update Servicios Cainco.
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="servicioCainco"></param>
    /// <returns></returns>
    [HttpPut("cainco/{Id}")]
    [ClaimCheck("UPDATE_SERVICIO_CAINCO")]
    public async Task<IActionResult> UpdateServicioCainco(Guid Id, ServicioCaincoDtoU servicioCainco)
    {
        var updateServiciosCommand = _mapper.Map<UpdateServicioCommand>(servicioCainco);
        updateServiciosCommand.Id = Id;
        updateServiciosCommand.TipoServicio = TipoServicio.CAINCO;
        var result = await _mediator.Send(updateServiciosCommand);
        return ReturnFormattedResponse(result);
    }

    /// <summary>
    /// Update Servicios Externos.
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="servicioExterno"></param>
    /// <returns></returns>
    [HttpPut("externos/{Id}")]
    [ClaimCheck("UPDATE_SERVICIO_EXTERNO")]
    public async Task<IActionResult> UpdateServicioExterno(Guid Id, ServicioExternoDtoU servicioExterno)
    {
        var updateServiciosCommand = _mapper.Map<UpdateServicioCommand>(servicioExterno);
        updateServiciosCommand.Id = Id;
        updateServiciosCommand.TipoServicio = TipoServicio.EXTERNO;
        updateServiciosCommand.ServicioCategoriaId = Guid.Empty;
        var result = await _mediator.Send(updateServiciosCommand);
        return ReturnFormattedResponse(result);
    }

    /// <summary>
    /// Delete Servicio.
    /// </summary>
    /// <param name="Id"></param>
    /// <returns></returns>
    [HttpDelete("{Id}")]
    // Ajustar ClaimCheck para varios claims
    //[ClaimCheck("DELETE_SERVICIO_EXTERNO")]
    public async Task<IActionResult> DeleteServicio(Guid Id)
    {
        var deleteServicioCommand = new DeleteServicioCommand { Id = Id };
        var result = await _mediator.Send(deleteServicioCommand);
        return ReturnFormattedResponse(result);
    }
}
