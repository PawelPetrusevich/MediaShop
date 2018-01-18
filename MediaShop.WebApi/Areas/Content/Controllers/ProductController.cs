using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using AutoMapper;
using MediaShop.Common.Dto;
using MediaShop.Common.Dto.Product;
using MediaShop.Common.Interfaces.Services;
using MediaShop.Common.Models.Content;
using MediaShop.WebApi.Properties;
using Swashbuckle.Swagger.Annotations;

namespace MediaShop.WebApi.Areas.Content.Controllers
{
    [RoutePrefix("api/product")]
    public class ProductController : ApiController
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        [Route("add")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.OK, " ", typeof(UploadModel))]
        [SwaggerResponse(HttpStatusCode.BadRequest, " ", typeof(string))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, " ")]
        public IHttpActionResult AddProduct([FromBody] UploadModel data)
        {
            if (data == null)
            {
                return BadRequest(Resources.ContentUploadError);
            }

            try
            {
                return Ok(_productService.UploadProducts(data));
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(Resources.ContentUploadError);
            }
            catch (Exception exception)
            {
                return InternalServerError();
            }
        }

        [HttpPost]
        [Route("GetOriginal")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.OK, " ", typeof(ProductContentDTO))]
        [SwaggerResponse(HttpStatusCode.BadRequest, " ", typeof(string))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, " ")]
        public IHttpActionResult GetOriginalProduct([FromBody] long id)
        {
            if (id <= 0)
            {
                return BadRequest(Resources.GetWithNullId);
            }

            try
            {
                return Ok(_productService.GetOriginalProduct(id));
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(Resources.GetWithNullId);
            }
            catch (Exception exception)
            {
                return InternalServerError();
            }
        }

        [HttpPost]
        [Route("GetProtected")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.OK, " ", typeof(ProductContentDTO))]
        [SwaggerResponse(HttpStatusCode.BadRequest, " ", typeof(string))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, " ")]
        public IHttpActionResult GetProtectedProduct([FromBody] long id)
        {
            if (id <= 0)
            {
                return BadRequest(Resources.GetWithNullId);
            }

            try
            {
                return Ok(_productService.GetProtectedProduct(id));
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(Resources.GetWithNullId);
            }
            catch (Exception exception)
            {
                return InternalServerError();
            }
        }

        [HttpPost]
        [Route("GetCompressed")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.OK, " ", typeof(ProductContentDTO))]
        [SwaggerResponse(HttpStatusCode.BadRequest, " ", typeof(string))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, " ")]
        public IHttpActionResult GetCompressedProduct([FromBody] long id)
        {
            if (id <= 0)
            {
                return BadRequest(Resources.GetWithNullId);
            }

            try
            {
                return Ok(_productService.GetCompressedProduct(id));
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(Resources.GetWithNullId);
            }
            catch (Exception exception)
            {
                return InternalServerError();
            }
        }

        [HttpPost]
        [Route("Find")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.OK, " ", typeof(List<ProductDto>))]
        [SwaggerResponse(HttpStatusCode.BadRequest, " ", typeof(string))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, " ")]
        public IHttpActionResult FindProducts([FromBody] List<ProductSearchModel> conditionsList)
        {
            if (conditionsList.Count <= 0)
            {
                return BadRequest("Пустой список условий");
            }

            try
            {
                return Ok(_productService.Find(conditionsList));
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(Resources.GetWithNullId);
            }
            catch (Exception exception)
            {
                return InternalServerError();
            }
        }

        [HttpPut]
        public IHttpActionResult Put()
        {
            return this.Ok();
        }

        [HttpDelete]
        [Route("delete")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.OK, " ", typeof(ProductDto))]
        [SwaggerResponse(HttpStatusCode.BadRequest, " ", typeof(string))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, " ")]
        public IHttpActionResult DeleteProduct([FromBody] long id)
        {
            if (id <= 0)
            {
                return BadRequest(Resources.GetWithNullId);
            }

            try
            {
                return Ok(_productService.DeleteProduct(id));
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(Resources.DeleteWithNullId);
            }
            catch (Exception exception)
            {
                return InternalServerError();
            }
        }
    }
}
