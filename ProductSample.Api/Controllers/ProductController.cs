using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Product.Application.Dto.Request.Base;
using Product.Application.Dto.Request.Product;
using Product.Application.Dto.Response.Product;
using Product.Application.Dto.Response.Public;
using ProductSample.Api.Configuration.UrlHelper;
using System.Net;

namespace ProductSample.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [ApiExplorerSettings(GroupName ="Site-v1")]
    [AllowAnonymous]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Insert product
        /// </summary>
        [HttpPost(Routes.Product.Insert)]
        [ProducesResponseType(typeof(ApiBaseResult) , (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiBaseResult), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Get(InsertProductRequest request)
        {
            var result = await _mediator.Send(request);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Error);
            }
            return Ok();
        }

        /// <summary>
        /// Upload image before insert product
        /// </summary>
        [HttpPost(Routes.Product.Upload)]
        [ProducesResponseType(typeof(ApiBaseResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiBaseResult), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UploadFile([FromForm] UploadRequest request)
        {
            var result = await _mediator.Send(request);
            if (!result.Success)
            {
                return BadRequest(result.Error);
            }
            return Ok();
        }

        /// <summary>
        /// Update product price and inventory
        /// </summary>
        [HttpPost(Routes.Product.Update)]
        [ProducesResponseType(typeof(ApiBaseResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiBaseResult), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Update(UpdateProductRequest request) 
        {
            var result = await _mediator.Send(request);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Error);
            }
            return Ok();
        }

        /// <summary>
        /// Update discount
        /// </summary>
        [HttpPost(Routes.Product.DisCount)]
        [ProducesResponseType(typeof(ApiBaseResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ApiBaseResult), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Discount(UpdateProductDiscountRequest request)
        {
            var result = await _mediator.Send(request);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Error);
            }
            return Ok();
        }

        /// <summary>
        /// get all product group
        /// </summary>
        [HttpGet(Routes.Product.GetAll)]
        [ProducesResponseType(typeof(ApiResult<List<ProductGroupResponse>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _mediator.Send(new GetAllProductsRequest()));
        }

    }

}
