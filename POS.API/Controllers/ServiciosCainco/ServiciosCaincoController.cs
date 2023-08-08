using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POS.API.Helpers;
using POS.Data.Dto;
using POS.Data.Resources;
using POS.MediatR.Product.Command;
using POS.Repository.Helper.ListGenerator;
using System;
using System.Threading.Tasks;

namespace POS.API.Controllers.ServiciosCainco
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ServiciosCaincoController : BaseController
    {
        private readonly IMediator _mediator;
        public ServiciosCaincoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get All ServiciosCainco.
        /// </summary>
        /// <param name="productResource"></param>
        /// <returns></returns>
        [HttpGet(Name = "GetServiciosCainco")]
        [ClaimCheck("PRO_VIEW_PRODUCTS")]
        public async Task<IActionResult> GetServiciosCainco([FromQuery] ProductResource productResource)
        {
            var getAllProductCommand = new GetAllProductCommand
            {
                ProductResource = productResource
            };
            var result = await _mediator.Send(getAllProductCommand);

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
        /// Get Product.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ClaimCheck("PRO_VIEW_PRODUCTS")]
        [Produces("application/json", "application/xml", Type = typeof(ProductDto))]
        public async Task<IActionResult> GetServiciosCainco(Guid id)
        {
            var getProductCommand = new GetProductCommand { Id = id };
            var result = await _mediator.Send(getProductCommand);
            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Add Product.
        /// </summary>
        /// <param name="addProductCommand"></param>
        /// <returns></returns>
        [HttpPost]
        [ClaimCheck("PRO_ADD_PRODUCT")]
        [DisableRequestSizeLimit]
        [Produces("application/json", "application/xml", Type = typeof(ProductDto))]
        public async Task<IActionResult> AddServiciosCainco(AddProductCommand addProductCommand)
        {
            var response = await _mediator.Send(addProductCommand);
            return ReturnFormattedResponse(response);
        }


        /// <summary>
        /// Update Product.
        /// </summary>
        /// <param name="updateProductCommand"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ClaimCheck("PRO_UPDATE_PRODUCT")]
        [DisableRequestSizeLimit]
        [Produces("application/json", "application/xml", Type = typeof(ProductDto))]
        public async Task<IActionResult> UpdateServiciosCainco(Guid id, UpdateProductCommand updateProductCommand)
        {
            updateProductCommand.Id = id;
            var response = await _mediator.Send(updateProductCommand);
            return ReturnFormattedResponse(response);
        }

        /// <summary>
        /// Delete Product.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ClaimCheck("PRO_DELETE_PRODUCT")]
        public async Task<IActionResult> DeleteServiciosCainco(Guid id)
        {
            var command = new DeleteProductCommand() { Id = id };
            var result = await _mediator.Send(command);
            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Get Products By Detail
        /// </summary>
        /// <returns></returns>
        [HttpGet("detail")]
        [ClaimCheck("PRO_VIEW_PRODUCTS")]
        [Produces("application/json", "application/xml", Type = typeof(ProductDto))]
        public async Task<IActionResult> GetServiciosCaincoDetail()
        {
            var getProductsDetail = new GetProductsDetailCommand();
            var response = await _mediator.Send(getProductsDetail);
            return ReturnFormattedResponse(response);
        }
    }
}
