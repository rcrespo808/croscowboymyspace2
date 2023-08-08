using System;
using System.Collections.Generic;
using System.Linq;
using MediatR;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POS.API.Helpers;
using POS.API.Helpers.Utils;
using POS.Data;
using POS.Domain;
using POS.MediatR.BeneficioCategoria.Command;

namespace POS.API.Controllers.BeneficiosCategorias;

[Route("api/[controller]")]
[ApiController]
public class BeneficiosCategoriasController : BaseController
{
    private readonly IMediator _mediator;
    private readonly Guid SociosId = Guid.Parse("08db51d6-0c5d-4c33-87d0-17b0f3cd5032");

    public BeneficiosCategoriasController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Obtener Beneficios Categorias.
    /// </summary>
    /// <param name="resource"></param>
    /// <returns></returns>
    [HttpGet]
    [ClaimCheck("READ_CATEGORIA")]
    public async Task<IActionResult> GetBeneficiosCategorias()
    {
        var getBeneficiosCategoriasCommand = new GetAllBeneficiosCategoriasCommand { };
        var result = await _mediator.Send(getBeneficiosCategoriasCommand);
        return Ok(result);
    }

    /// <summary>
    /// Obtener Beneficios Categorias Usuarios.
    /// </summary>
    /// <param name="resource"></param>
    /// <returns></returns>
    [HttpGet("usuarios")]
    [ClaimCheck("READ_BENF_USER,READ_BENF_SOC")]
    public async Task<IActionResult> GetBeneficiosCategoriasUsuarios()
    {
        var getBeneficiosCategoriasCommand = new GetAllBeneficiosCategoriasCommand { IgnoreBeneficioCategoriaId = SociosId };
        var result = await _mediator.Send(getBeneficiosCategoriasCommand);
        return Ok(result);
    }

    /// <summary>
    /// Obtener Beneficios Categorias By Id.
    /// </summary>
    /// <param name="Id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [ClaimCheck("READ_CATEGORIA")]
    public async Task<IActionResult> GetBeneficioCategoria(Guid Id)
    {
        var getBeneficioCategoriaCommand = new GetBeneficioCategoriaCommand { Id = Id };
        var result = await _mediator.Send(getBeneficioCategoriaCommand);
        return Ok(result);
    }

    /// <summary>
    /// Add BeneficioCategoria
    /// </summary>
    /// <param name="addBeneficioCategoriaCommand"></param>
    /// <returns></returns>
    [HttpPost]
    [ClaimCheck("CREATE_CATEGORIA")]
    public async Task<IActionResult> AddBeneficioCategoria(AddBeneficioCategoriaCommand addBeneficioCategoriaCommand)
    {
        var result = await _mediator.Send(addBeneficioCategoriaCommand);
        return ReturnFormattedResponse(result);
    }

    /// <summary>
    /// Update Beneficio Categoria.
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="updateBeneficioCategoriasCommand"></param>
    /// <returns></returns>
    [HttpPut("{Id}")]
    [ClaimCheck("UPDATE_CATEGORIA")]
    public async Task<IActionResult> UpdateBeneficioCategoria(Guid Id, UpdateBeneficioCategoriaCommand updateBeneficioCategoriasCommand)
    {
        updateBeneficioCategoriasCommand.Id = Id;
        var result = await _mediator.Send(updateBeneficioCategoriasCommand);
        return ReturnFormattedResponse(result);
    }

    /// <summary>
    /// Delete BeneficioCategoria.
    /// </summary>
    /// <param name="Id"></param>
    /// <returns></returns>
    [HttpDelete("{Id}")]
    [ClaimCheck("DELETE_CATEGORIA")]
    public async Task<IActionResult> DeleteBeneficioCategoria(Guid Id)
    {
        var deleteBeneficioCategoriaCommand = new DeleteBeneficioCategoriaCommand { Id = Id };
        var result = await _mediator.Send(deleteBeneficioCategoriaCommand);
        return ReturnFormattedResponse(result);
    }
}
