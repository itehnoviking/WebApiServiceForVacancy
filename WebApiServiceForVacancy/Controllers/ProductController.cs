using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Net;
using AutoMapper;
using WebApiServiceForVacancy.Core.DTOs;
using WebApiServiceForVacancy.Core.Interfaces.Services;
using WebApiServiceForVacancy.Models.Requests;
using WebApiServiceForVacancy.Models.Responses;

namespace WebApiServiceForVacancy.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }


        [HttpPost]
        [ProducesResponseType(typeof(ResponseMessage), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseMessage), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseMessage), 500)]
        public async Task<IActionResult> CreateNew(CreateNewProductRequest request)
        {
            try
            {
                if (request == null || !ModelState.IsValid)
                {
                    return BadRequest(new ResponseMessage { Message = "Request is null or invalid" });
                }

                await _productService.CreateAsync(_mapper.Map<CreateNewProductDto>(request));

                return Ok(new ResponseMessage { Message = "Product created" });
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, ex.Message);
                return StatusCode(500, new ResponseMessage { Message = ex.Message });
            }
        }

        [HttpPatch("{id}/changeAvailability")]
        [ProducesResponseType(typeof(ResponseMessage), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseMessage), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseMessage), 500)]
        public async Task<IActionResult> СhangeAvailability(uint id, [FromBody] СhangeAvailabilityProductRequest request)
        {
            try
            {
                if (id == null || !ModelState.IsValid)
                {
                    return BadRequest(new ResponseMessage { Message = "Id or request is null or invalid" });
                }
                var product = await _productService.GetByIdAsync(id);
                
                if (product == null)
                {
                    return BadRequest(new ResponseMessage { Message = $"Product not found" });
                }

                if (product.IsAvailable.Equals(request.IsAvailable))
                {
                    return Ok(new ResponseMessage { Message = "Product availability has already changed" });
                }

                product.IsAvailable = request.IsAvailable;
                await _productService.EditAsync(product);

                return Ok(new ResponseMessage { Message = "Product availability changed" });
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, ex.Message);
                return StatusCode(500, new ResponseMessage { Message = ex.Message });
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProductDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseMessage), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseMessage), 500)]
        public async Task<IActionResult> GetAllProductsAvailableForOrder()
        {
            try
            {
                var products = await _productService.GetAllProductsAvailableForOrderAsync();

                if (products == null || !products.Any())
                {
                    return BadRequest(new ResponseMessage { Message = "No content" });
                }

                return Ok(products);

            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, ex.Message);
                return StatusCode(500, new ResponseMessage { Message = ex.Message });
            }
        }


    }
}
