using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using AutoMapper;
using MediaShop.Common.Dto;
using MediaShop.Common.Dto.Product;
using MediaShop.Common.Interfaces.Services;
using MediaShop.Common.Models.Content;
using MediaShop.Common.Models.User;
using MediaShop.WebApi.Areas.Content.Controllers;
using MediaShop.WebApi.Filters;
using MediaShop.WebApi.Properties;
using Swashbuckle.Swagger.Annotations;

namespace MediaShop.WebApi.Areas.Content.Controllers
{
    [LoggingFilter]
    [EnableCors("*", "*", "*")]
    [System.Web.Http.RoutePrefix("api/product")]
    [ProductExeptionFilter]
    public class ProductController : ApiController
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [Route("getById/{id}")]
        [AllowAnonymous]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.OK, " ", typeof(string))]
        [SwaggerResponse(HttpStatusCode.BadRequest, " ", typeof(string))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, " ")]
        public IHttpActionResult GetProductById([FromUri]long id)
        {
            if (id == null || id <= 0)
            {
                return BadRequest(Resources.IncorrectId);
            }

            try
            {
                return Ok(_productService.GetById(id));
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(Resources.NotFoundProduct);
            }
            catch (Exception e)
            {
                return InternalServerError();
            }
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("add")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.OK, "Successful add product ", typeof(UploadProductModel))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Content upload error or unknown product type", typeof(string))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Other errors")]
        [MediaAuthorizationFilter(Permission = Permissions.See)]
        public IHttpActionResult AddProduct([FromBody] UploadProductModel data)
        {
            if (data == null || !ModelState.IsValid)
            {
                return BadRequest(Resources.ContentUploadError);
            }

            try
            {
                var user = HttpContext.Current.User.Identity as ClaimsIdentity;
                var claimId = user.Claims.FirstOrDefault(x => x.Type == Resources.ClaimTypeId);
                var creatorId = long.Parse(claimId.Value);
                return Ok(_productService.UploadProducts(data, creatorId));
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
        [MediaAuthorizationFilter(Permission = Permissions.See)]
        public async Task<IHttpActionResult> AddProductAsync([FromBody] UploadProductModel data)
        {
            if (data == null || !ModelState.IsValid)
            {
                return BadRequest(Resources.ContentUploadError);
            }

            try
            {
                var user = HttpContext.Current.User.Identity as ClaimsIdentity;
                var claimID = user.Claims.FirstOrDefault(x => x.Type == Resources.ClaimTypeId);
                var userId = long.Parse(claimID.Value);
                return Ok(await _productService.UploadProductsAsync(data, userId));
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
        [AllowAnonymous]
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
        [AllowAnonymous]
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
            catch (ArgumentException)
            {
                return BadRequest(Resources.ErrorFindService);
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
        [MediaAuthorizationFilter(Permission = Permissions.See)]
        public IHttpActionResult DeleteProduct(long id)
        {
            if (id <= 0)
            {
                return BadRequest(Resources.GetWithNullId);
            }

            var user = HttpContext.Current.User.Identity as ClaimsIdentity;
            var claimId = user.Claims.FirstOrDefault(x => x.Type == Resources.ClaimTypeId);
            var createrId = long.Parse(claimId.Value);

            try
            {
                return Ok(_productService.SoftDeleteById(id, createrId));
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
        [MediaAuthorizationFilter(Permission = Permissions.See)]
        public async Task<IHttpActionResult> DeleteProductAsync(long id)
        {
            if (id <= 0)
            {
                return BadRequest(Resources.GetWithNullId);
            }

            var user = HttpContext.Current.User.Identity as ClaimsIdentity;
            var claimId = user.Claims.FirstOrDefault(x => x.Type == Resources.ClaimTypeId);
            var createrId = long.Parse(claimId.Value);

            try
            {
                return Ok(await _productService.SoftDeleteByIdAsync(id, createrId));
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
        [MediaAuthorizationFilter(Permission = Permissions.See)]
        public IHttpActionResult GetListPurshasedProducts()
        {
            try
            {
                var user = HttpContext.Current.User.Identity as ClaimsIdentity;
                var idClaim = user.Claims.FirstOrDefault(x => x.Type == Resources.ClaimTypeId);
                var idUser = long.Parse(idClaim.Value);

                return Ok(_productService.GetListPurshasedProducts(idUser));
            }
            catch (NullReferenceException)
            {
                return BadRequest(Resources.NullTokenData);
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
        [MediaAuthorizationFilter(Permission = Permissions.See)]
        public async Task<IHttpActionResult> GetListPurshasedProductsAsync()
        {
            try
            {
                var user = HttpContext.Current.User.Identity as ClaimsIdentity;
                var idClaim = user.Claims.FirstOrDefault(x => x.Type == Resources.ClaimTypeId);
                var idUser = long.Parse(idClaim.Value);

                return Ok(await _productService.GetListPurshasedProductsAsync(idUser));
            }
            catch (NullReferenceException)
            {
                return BadRequest(Resources.NullTokenData);
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
        [AllowAnonymous]
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
            catch (Exception e)
            {
                return InternalServerError();
            }
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("GetListOnSaleAsync")]
        [AllowAnonymous]
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
        [System.Web.Http.Route("GetOriginalPurshasedProduct/{productId}")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.OK, "Successful Get Original Purshased Product", typeof(OriginalProductDTO))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Error get original purshased product", typeof(string))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Other errors")]
        [MediaAuthorizationFilter(Permission = Permissions.See)]
        public IHttpActionResult GetOriginalPurshasedProduct([FromUri]long productId)
        {
            if (productId <= 0)
            {
                return BadRequest(Resources.GetWithNullId);
            }

            try
            {
                var user = HttpContext.Current.User.Identity as ClaimsIdentity;
                var idClaim = user.Claims.FirstOrDefault(x => x.Type == Resources.ClaimTypeId);
                var idUser = long.Parse(idClaim.Value);

                return Ok(_productService.GetOriginalPurshasedProduct(idUser, productId));
            }
            catch (NullReferenceException)
            {
                return BadRequest(Resources.NullTokenData);
            }
            catch (InvalidOperationException)
            {
                return BadRequest(Resources.ContentDownloadError);
            }
            catch (ArgumentNullException)
            {
                return BadRequest(Resources.ErrorDownload);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("GetOriginalPurshasedProductAsync/{productId}")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.OK, "Successful Get Original Purshased Product", typeof(OriginalProductDTO))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Error get original purshased product", typeof(string))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Other errors")]
        [MediaAuthorizationFilter(Permission = Permissions.See)]
        public async Task<IHttpActionResult> GetOriginalPurshasedProductAsync([FromUri]long productId)
        {
            if (productId <= 0)
            {
                return BadRequest(Resources.GetWithNullId);
            }

            try
            {
                var user = HttpContext.Current.User.Identity as ClaimsIdentity;
                var idClaim = user.Claims.FirstOrDefault(x => x.Type == Resources.ClaimTypeId);
                var idUser = long.Parse(idClaim.Value);

                return Ok(await _productService.GetOriginalPurshasedProductAsync(idUser, productId));
            }
            catch (NullReferenceException)
            {
                return BadRequest(Resources.NullTokenData);
            }
            catch (InvalidOperationException)
            {
                return BadRequest(Resources.ContentDownloadError);
            }
            catch (ArgumentNullException)
            {
                return BadRequest(Resources.ErrorDownload);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("GetUploadProductAsync")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.OK, "Successful Get upload Product", typeof(CompressedProductDTO))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "Error get upload product", typeof(string))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Other errors")]
        [MediaAuthorizationFilter(Permission = Permissions.See)]
        public async Task<IHttpActionResult> GetUploadProductAsync()
        {
            try
            {
                var user = HttpContext.Current.User.Identity as ClaimsIdentity;
                var idClaim = user.Claims.FirstOrDefault(x => x.Type == Resources.ClaimTypeId);
                var idUser = long.Parse(idClaim.Value);

                return Ok(await _productService.GetUploadProductListAsync(idUser));
            }
            catch (NullReferenceException)
            {
                return BadRequest(Resources.NullTokenData);
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
