using POS.Data.Resources;
using POS.MediatR.CommandAndQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;
using System.Threading.Tasks;
using POS.API.Helpers;
using POS.MediatR.Supplier.Commands;
using SAPB1.Integrations;
using SAPB1.Sap_Objects;
using System.Collections.Generic;
using Microsoft.VisualBasic;
using POS.Data;
using System.Linq;

namespace POS.API.Controllers
{
    /// <summary>
    /// Supplier Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    [AllowAnonymous]
    public class SupplierController : BaseController
    {
        public readonly IMediator _mediator;
        private readonly ILogger<SupplierController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="SupplierController"/> class.
        /// </summary>
        /// <param name="mediator">The mediator.</param>
        public SupplierController(IMediator mediator,
             ILogger<SupplierController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Get All Suppliers
        /// </summary>
        /// <param name="supplierResource"></param>
        /// <returns></returns>

        [HttpGet(Name = "GetSuppliers")]
        [ClaimCheck("READ_SOCIO")]
        public async Task<IActionResult> GetSuppliers([FromQuery] SupplierResource supplierResource)
        {
            var getAllSupplierQuery = new GetAllSupplierQuery
            {
                SupplierResource = supplierResource
            };
            var result = await _mediator.Send(getAllSupplierQuery);

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
        /// Get Supplier by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetSupplier")]
        [ClaimCheck("READ_SOCIO")]
        public async Task<IActionResult> GetSupplier(Guid id)
        {
            var getSupplierQuery = new GetSupplierQuery
            {
                Id = id
            };

            var result = await _mediator.Send(getSupplierQuery);
            if (result.StatusCode != 200)
            {
                _logger.LogError(result.StatusCode,
                                JsonSerializer.Serialize(result), "");
                return StatusCode(result.StatusCode, result);
            }
            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Add Supplier
        /// </summary>
        /// <param name="addSupplierCommand"></param>
        /// <returns></returns>
        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> AddSupplier([FromBody] AddSupplierCommand addSupplierCommand)
        {
            var result = await _mediator.Send(addSupplierCommand);
            if (result.StatusCode != 200)
            {
                _logger.LogError(result.StatusCode,
                                JsonSerializer.Serialize(result), "");
                return StatusCode(result.StatusCode, result);
            }

            try
            {
                Integration integration = new Integration();
                var bp = new BusinessPartners();
                bp.CardCode = result.Data.Id.ToString();
                bp.CardName = addSupplierCommand.SupplierName;
                bp.CardType = "C";
                bp.MailAddress = addSupplierCommand.Email;
                bp.Phone1 = addSupplierCommand.PhoneNo;
                bp.Fax = addSupplierCommand.Fax;
                //bp.Cellular = addSupplierCommand.MobileNo;
                bp.Website = addSupplierCommand.Website;
                bp.Notes = addSupplierCommand.Description;

                object value = await integration.CreateBusinessParthner(bp);
            }
            catch (Exception)
            {

            }


            //Integration integration = new Integration();

            //List<BusinessPartners> bpList = await integration.GetBusinessParthnerList();

            //foreach (var bp in bpList)
            //{
            //    //("CardCode, CardName, CardType, ContactPerson, U_correos_facturacion_elec, Fax, Cellular, Phone1, Website, Notes")
            //    //AddSupplierCommand supplierCommand2 = new AddSupplierCommand();

            //    supplierCommand2.CardCode = bp.CardCode;
            //    supplierCommand2.SupplierName = bp.CardName;
            //    supplierCommand2.ContactPerson = (string)bp.ContactPerson;

            //    supplierCommand2.ContactPerson = (string)bp.ContactPerson;
            //    supplierCommand2.Email = (string)bp.U_correos_facturacion_elec;
            //    supplierCommand2.Fax= (string)bp.Fax;
            //    supplierCommand2.MobileNo = (string)bp.Cellular;
            //    supplierCommand2.PhoneNo = (string)bp.Phone1;
            //    supplierCommand2.Website = (string)bp.Website;
            //    supplierCommand2.Description = (string)bp.Notes;

            //    var resultBP = await _mediator.Send(supplierCommand2);

            //}

            //return Ok();

            return CreatedAtRoute("GetSupplier",
                 new { id = result.Data.Id },
                 result.Data);
        }
        /// <summary>
        /// Update Supplier By Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateSupplierCommand"></param>
        /// <returns></returns>
        [HttpPut("{id}"), DisableRequestSizeLimit]
        [ClaimCheck("UPDATE_SOCIO")]
        public async Task<IActionResult> UpdateSupplier(Guid id, [FromBody] UpdateSupplierCommand updateSupplierCommand)
        {
            updateSupplierCommand.Id = id;
            var result = await _mediator.Send(updateSupplierCommand);
            if (result.StatusCode != 200)
            {
                _logger.LogError(result.StatusCode,
                                JsonSerializer.Serialize(result), "");
                return StatusCode(result.StatusCode, result);
            }

            //try
            //{
            //    var bp = new BusinessPartners();
            //    bp.CardCode = updateSupplierCommand.CardCode;
            //    bp.CardName = updateSupplierCommand.SupplierName;
            //    bp.CardType = "C";
            //    bp.ContactPerson = updateSupplierCommand.ContactPerson;
            //    bp.MailAddress = updateSupplierCommand.Email;
            //    bp.Phone1 = updateSupplierCommand.PhoneNo;
            //    bp.Fax = updateSupplierCommand.Fax;
            //    bp.Cellular = updateSupplierCommand.MobileNo;
            //    bp.Website = updateSupplierCommand.Website;
            //    bp.Notes = updateSupplierCommand.Description;
            //    Integration integration = new Integration();
            //    integration.UpdateBusinessParthner(bp);

            //}
            //catch (Exception)
            //{

            //}

            return NoContent();
        }
        /// <summary>
        /// Delete Supplier By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ClaimCheck("DELETE_SOCIO")]
        public async Task<IActionResult> DeleteSupplier(Guid id)
        {
            var deleteSupplierCommand = new DeleteSupplierCommand { Id = id };
            var result = await _mediator.Send(deleteSupplierCommand);
            if (result.StatusCode != 200)
            {
                _logger.LogError(result.StatusCode,
                                JsonSerializer.Serialize(result.Errors), "");
                return StatusCode(result.StatusCode, result.Errors);
            }
            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Get Latest Register Suppliers
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetNewSupplier")]
        [ClaimCheck("READ_SOCIO")]
        public async Task<IActionResult> GetNewSupplier()
        {
            var result = await _mediator.Send(new GetNewSupplierQuery { });
            return Ok(result);
        }

        /// <summary>
        /// Get Supplier Payment.
        /// </summary>
        /// <param name="supplierResource"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetSupplierPayment")]
        //[ClaimCheck("SUPP_VIEW_SUPPLIERS")]
        public async Task<IActionResult> GetSupplierPayment([FromQuery] SupplierResource supplierResource)
        {
            var result = await _mediator.Send(new GetSupplierPaymentsQuery
            {
                SupplierResource = supplierResource
            });

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
        /// Get All Suppliers Interes
        /// </summary>
        /// <param name="supplierResource"></param>
        /// <returns></returns>

        [HttpGet]
        [Route("GetSupplierInterest")]
        public async Task<IActionResult> GetInterest([FromQuery] SupplierResource supplierResource)
        {
            var getAllSupplierQuery = new GetAllSupplierQuery
            {
                SupplierResource = supplierResource
            };
            var result = await _mediator.Send(getAllSupplierQuery);

            var paginationMetadata = new
            {
                totalCount = result.TotalCount,
                pageSize = result.PageSize,
                skip = result.Skip,
                totalPages = result.TotalPages
            };
            Response.Headers.Add("X-Pagination",
                Newtonsoft.Json.JsonConvert.SerializeObject(paginationMetadata));

            return Ok(result.Take(3));
        }


        /// <summary>
        /// Get All Suppliers Publicity
        /// </summary>
        /// <param name="supplierResource"></param>
        /// <returns></returns>

        [HttpGet]
        [Route("GetSupplierPublicity")]
        [ClaimCheck("READ_PUB_SOC")]
        public async Task<IActionResult> GetPublicity([FromQuery] SupplierResource supplierResource)
        {
            var getAllSupplierQuery = new GetAllSupplierQuery
            {
                SupplierResource = supplierResource
            };
            var result = await _mediator.Send(getAllSupplierQuery);

            var paginationMetadata = new
            {
                totalCount = result.TotalCount,
                pageSize = result.PageSize,
                skip = result.Skip,
                totalPages = result.TotalPages
            };
            Response.Headers.Add("X-Pagination",
                Newtonsoft.Json.JsonConvert.SerializeObject(paginationMetadata));

            return Ok(result.Take(1));
        }

    }
}
