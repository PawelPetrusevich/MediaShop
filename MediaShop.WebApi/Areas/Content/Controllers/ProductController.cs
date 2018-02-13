using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using MediaShop.Common.Dto;
using MediaShop.Common.Dto.Product;
using MediaShop.Common.Interfaces.Services;
using MediaShop.Common.Models.Content;
using MediaShop.WebApi.Areas.Content.Controllers;
using MediaShop.WebApi.Areas.Content.Controllers.Filters;
using MediaShop.WebApi.Properties;
using Swashbuckle.Swagger.Annotations;

namespace MediaShop.WebApi.Areas.Content.Controllers
{
    [LoggingFilter]
    [System.Web.Http.RoutePrefix("api/product")]
    public class ProductController : ApiController
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("add")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.OK, "Successful add product ", typeof(UploadProductModel))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Content upload error or unknown product type", typeof(string))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Other errors")]
        public IHttpActionResult AddProduct([FromBody] UploadProductModel data)
        {
            if (data == null || !ModelState.IsValid)
            {
                return BadRequest(Resources.ContentUploadError);
            }

            try
            {
                return Ok(_productService.UploadProducts(data));
            }
            catch (InvalidOperationException)
            {
                return BadRequest(Resources.ContentUploadError);
            }
            catch (ArgumentException)
            {
                return BadRequest(Resources.UnknowProductType);
            }
            catch (Exception e)
            {
                return InternalServerError();
            }
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("addAsync")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.OK, "Successful add product ", typeof(UploadProductModel))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Content upload error or unknown product type", typeof(string))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Other errors")]
        public async Task<IHttpActionResult> AddProductAsync([FromBody] UploadProductModel data)
        {
            if (data == null || !ModelState.IsValid)
            {
                return BadRequest(Resources.ContentUploadError);
            }

            try
            {
                return Ok(await _productService.UploadProductsAsync(data));
            }
            catch (InvalidOperationException)
            {
                return BadRequest(Resources.ContentUploadError);
            }
            catch (ArgumentException)
            {
                return BadRequest(Resources.UnknowProductType);
            }
            catch (Exception e)
            {
                return InternalServerError();
            }
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("Find")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.OK, "Return list of products ", typeof(List<ProductDto>))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Input error ", typeof(string))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Other errors")]
        public IHttpActionResult FindProducts([FromBody] List<ProductSearchModel> conditionsList)
        {
            if (conditionsList == null || !ModelState.IsValid)
            {
                var sb = new StringBuilder();
                foreach (var value in ModelState.Values)
                {
                    foreach (var error in value.Errors)
                    {
                        sb.AppendFormat("{0} ! ", error.ErrorMessage);
                    }
                }

                if (sb.Length != 0)
                {
                    return BadRequest(sb.ToString());
                }
                else
                {
                    return BadRequest(Resources.EmptyConditionList);
                }
            }

            if (conditionsList.Count <= 0)
            {
                return BadRequest(Resources.EmptyConditionList);
            }
                
            try
            {
                return Ok(_productService.Find(conditionsList));
            }
            catch (InvalidOperationException)
            {
                return BadRequest(Resources.GetWithNullId);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
                
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("FindAsync")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.OK, "Return list of products ", typeof(List<ProductDto>))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Input error ", typeof(string))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Other errors")]
        public async Task<IHttpActionResult> FindProductsAsync([FromBody] List<ProductSearchModel> conditionsList)
        {
            if (conditionsList == null || !ModelState.IsValid)
            {
                var sb = new StringBuilder();
                foreach (var value in ModelState.Values)
                {
                    foreach (var error in value.Errors)
                    {
                        sb.AppendFormat("{0} ! ", error.ErrorMessage);
                    }
                }

                if (sb.Length != 0)
                {
                    return BadRequest(sb.ToString());
                }
                else
                {
                    return BadRequest(Resources.EmptyConditionList);
                }
            }

            if (conditionsList.Count <= 0)
            {
                return BadRequest(Resources.EmptyConditionList);
            }

            try
            {
                return Ok(await _productService.FindAsync(conditionsList));
            }
            catch (InvalidOperationException)
            {
                return BadRequest(Resources.GetWithNullId);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [HttpDelete]
        [Route("delete/{id}")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.OK, "Successful delete by id ", typeof(ProductDto))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Error delete by id", typeof(string))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Other errors")]
        public IHttpActionResult DeleteProduct(long id)
        {
            if (id <= 0)
            {
                return BadRequest(Resources.GetWithNullId);
            }

            try
            {
                return Ok(_productService.SoftDeleteById(id));
            }
            catch (InvalidOperationException)
            {
                return BadRequest(Resources.DeleteWithNullId);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [HttpDelete]
        [Route("deleteAsync/{id}")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.OK, "Successful delete by id ", typeof(ProductDto))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Error delete by id", typeof(string))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Other errors")]
        public async Task<IHttpActionResult> DeleteProductAsync(long id)
        {
            if (id <= 0)
            {
                return BadRequest(Resources.GetWithNullId);
            }

            try
            {
                return Ok(await _productService.SoftDeleteByIdAsync(id));
            }
            catch (InvalidOperationException)
            {
                return BadRequest(Resources.DeleteWithNullId);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("GetPurshasedProducts")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.OK, "Successful get purshased products", typeof(List<CompressedProductDTO>))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Error get purshased product", typeof(string))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Other errors")]
        public IHttpActionResult GetListPurshasedProducts([FromUri] long userId)
        {
            if (userId <= 0)
            {
                return BadRequest(Resources.GetWithNullId);
            }

            try
            {
                return Ok(_productService.GetListPurshasedProducts(userId));
            }
            catch (InvalidOperationException)
            {
                return BadRequest(Resources.ContentDownloadError);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("GetPurshasedProductsAsync")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.OK, "Successful get purshased products", typeof(List<CompressedProductDTO>))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Error get purshased product", typeof(string))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Other errors")]
        public async Task<IHttpActionResult> GetListPurshasedProductsAsync([FromUri] long userId)
        {
            if (userId <= 0)
            {
                return BadRequest(Resources.GetWithNullId);
            }

            try
            {
                return Ok(await _productService.GetListPurshasedProductsAsync(userId));
            }
            catch (InvalidOperationException)
            {
                return BadRequest(Resources.ContentDownloadError);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("GetListOnSale")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.OK, "Successful Get compressed products on sale", typeof(List<CompressedProductDTO>))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Error get products on sale", typeof(string))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Other errors")]
        public IHttpActionResult GetListOnSale()
        {
            try
            {
                return Ok(_productService.GetListOnSale());
            }
            catch (InvalidOperationException)
            {
                return BadRequest(Resources.ContentDownloadError);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("GetListOnSaleAsync")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.OK, "Successful Get compressed products on sale", typeof(List<CompressedProductDTO>))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Error get products on sale", typeof(string))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Other errors")]
        public async Task<IHttpActionResult> GetListOnSaleAsync()
        {
            try
            {
                return Ok(await _productService.GetListOnSaleAsync());
            }
            catch (InvalidOperationException)
            {
                return BadRequest(Resources.ContentDownloadError);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("GetOriginalPurshasedProduct")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.OK, "Successful Get Original Purshased Product", typeof(OriginalProductDTO))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Error get original purshased product", typeof(string))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Other errors")]
        public IHttpActionResult GetOriginalPurshasedProduct([FromUri] long userId, long productId)
        {
            if (userId <= 0 || productId <= 0)
            {
                return BadRequest(Resources.GetWithNullId);
            }

            try
            {
                return Ok(_productService.GetOriginalPurshasedProduct(userId, productId));
            }
            catch (InvalidOperationException)
            {
                return BadRequest(Resources.ContentDownloadError);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("GetOriginalPurshasedProductAsync")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.OK, "Successful Get Original Purshased Product", typeof(OriginalProductDTO))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Error get original purshased product", typeof(string))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Other errors")]
        public async Task<IHttpActionResult> GetOriginalPurshasedProductAsync([FromUri] long userId, long productId)
        {
            if (userId <= 0 || productId <= 0)
            {
                return BadRequest(Resources.GetWithNullId);
            }

            try
            {
                return Ok(await _productService.GetOriginalPurshasedProductAsync(userId, productId));
            }
            catch (InvalidOperationException)
            {
                return BadRequest(Resources.ContentDownloadError);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
    }
}
