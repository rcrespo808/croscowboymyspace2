using MediatR;
using Microsoft.AspNetCore.Mvc;
using POS.MediatR.ServicioCategoria.Command;
using System;
using System.Threading.Tasks;

namespace POS.API.Controllers.ServiciosCategorias;

[Route("api/[controller]")]
[ApiController]
public class ServiciosCategoriasController : BaseController
{
    private readonly IMediator _mediator;

    public ServiciosCategoriasController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Obtener Servicios Categorias.
    /// </summary>
    /// <param name="resource"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetServiciosCategorias()
    {
        var getServiciosCategoriasCommand = new GetAllServiciosCategoriasCommand { };
        var result = await _mediator.Send(getServiciosCategoriasCommand);
        return Ok(result);
    }

    /// <summary>
    /// Obtener Servicios Categorias By Id.
    /// </summary>
    /// <param name="Id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetServicioCategoria(Guid Id)
    {
        var getServicioCategoriaCommand = new GetServicioCategoriaCommand { Id = Id };
        var result = await _mediator.Send(getServicioCategoriaCommand);
        return Ok(result);
    }

    /// <summary>
    /// Add ServicioCategoria
    /// </summary>
    /// <param name="addServicioCategoriaCommand"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> AddServicioCategoria(AddServicioCategoriaCommand addServicioCategoriaCommand)
    {
        var result = await _mediator.Send(addServicioCategoriaCommand);
        return ReturnFormattedResponse(result);
    }

    /// <summary>
    /// Update Servicio Categoria.
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="updateServicioCategoriasCommand"></param>
    /// <returns></returns>
    [HttpPut("{Id}")]
    public async Task<IActionResult> UpdateServicioCategoria(Guid Id, UpdateServicioCategoriaCommand updateServicioCategoriasCommand)
    {
        updateServicioCategoriasCommand.Id = Id;
        var result = await _mediator.Send(updateServicioCategoriasCommand);
        return ReturnFormattedResponse(result);
    }

    /// <summary>
    /// Delete ServicioCategoria.
    /// </summary>
    /// <param name="Id"></param>
    /// <returns></returns>
    [HttpDelete("{Id}")]
    public async Task<IActionResult> DeleteServicioCategoria(Guid Id)
    {
        var deleteServicioCategoriaCommand = new DeleteServicioCategoriaCommand { Id = Id };
        var result = await _mediator.Send(deleteServicioCategoriaCommand);
        return ReturnFormattedResponse(result);
    }
}
