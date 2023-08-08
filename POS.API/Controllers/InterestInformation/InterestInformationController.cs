using MediatR;
using Microsoft.AspNetCore.Mvc;
using POS.API.Helpers;
using POS.Data.Resources;
using POS.Domain;
using POS.MediatR.Country.Commands;
using POS.MediatR.InterestInformation.Command;
using System;
using System.Threading.Tasks;

namespace POS.API.Controllers.InterestInformation;

[Route("api/informacioninteres")]
[ApiController]
public class InterestInformationController : BaseController
{
    public POSDbContext _context;
    private readonly IMediator _mediator;

    public InterestInformationController(IMediator mediator, POSDbContext context)
    {
        this._context = context;
        _mediator = mediator;
    }

    /// <summary>
    /// Get All Information of Interest.
    /// </summary>
    /// <param name="interestInformationResource"></param>
    /// <returns></returns>
    [HttpGet]
    [ClaimCheck("READ_COMP_NOR,READ_GEST_RESP,READ_COM_INFO")]
    public async Task<IActionResult> GetAllInterestInformation([FromQuery] InterestInformationResource interestInformationResource)
    {
        var getAllInterestInformationCommand = new GetAllInterestInformationCommand
        {
            InterestInformationResource = interestInformationResource
        };
        var result = await _mediator.Send(getAllInterestInformationCommand);

        var paginationMetadata = new
        {
            totalCount = result.TotalCount,
            pageSize = result.PageSize,
            skip = result.Skip,
            totalPages = result.TotalPages
        };
        Response.Headers.Add("X-Pagination",
            Newtonsoft.Json.JsonConvert.SerializeObject(paginationMetadata));
        return Ok(new { Items = result });
    }

    /// <summary>
    /// Get Information of Interest by Id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:Guid}")]
    [ClaimCheck("READ_COMP_NOR,READ_GEST_RESP,READ_COM_INFO")]
    public async Task<IActionResult> GetInterestInformationById(Guid id)
    {
        var getInterestInformationCommand = new GetInterestInformationCommand { Id = id };
        var result = await _mediator.Send(getInterestInformationCommand);
        return Ok(result.Data);
    }

    /// <summary>
    /// Create Information of Interest.
    /// </summary>
    /// <param name="addInterestInformationCommand"></param>
    /// <returns></returns>
    [HttpPost]
    [ClaimCheck("CREATE_COMP_NOR,CREATE_GEST_RESP,CREATE_COM_INFO")]
    public async Task<IActionResult> CreateInterestInformation(AddInterestInformationCommand addInterestInformationCommand)
    {
        var result = await _mediator.Send(addInterestInformationCommand);
        return ReturnFormattedResponse(result);
    }

    /// <summary>
    /// Update Information of Interest.
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="updateInterestInformationCommand"></param>
    /// <returns></returns>
    [HttpPut("{id:Guid}")]
    [ClaimCheck("UPDATE_COMP_NOR,UPDATE_GEST_RESP,UPDATE_COM_INFO")]
    public async Task<IActionResult> UpdateInterestInformation(Guid Id, UpdateInterestInformationCommand updateInterestInformationCommand)
    {
        updateInterestInformationCommand.Id = Id;
        var result = await _mediator.Send(updateInterestInformationCommand);
        return ReturnFormattedResponse(result);
    }

    /// <summary>
    /// Delete Information of Interest.
    /// </summary>
    /// <param name="Id"></param>
    /// <returns></returns>
    [HttpDelete("{id:Guid}")]
    [ClaimCheck("DELETE_COMP_NOR,DELETE_GEST_RESP,DELETE_COM_INFO")]
    public async Task<IActionResult> DeleteInterestInformation(Guid Id)
    {
        var deleteInterestInformationCommand = new DeleteInterestInformationCommand { Id = Id };
        var result = await _mediator.Send(deleteInterestInformationCommand);
        return ReturnFormattedResponse(result);
    }
}
