﻿using System;
using System.Collections.Generic;
using System.Net;
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
    [StopWatchFilter]
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
        [System.Web.Http.Route("Find")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.OK, " ", typeof(List<ProductDto>))]
        [SwaggerResponse(HttpStatusCode.BadRequest, " ", typeof(string))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Other errors")]
        public IHttpActionResult FindProducts([FromBody] List<ProductSearchModel> conditionsList)
        {
            if (conditionsList == null || !ModelState.IsValid)
            {
                return BadRequest(Resources.EmptyConditionList);
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

        [HttpDelete]
        [Route("delete/{id}")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.OK, " ", typeof(ProductDto))]
        [SwaggerResponse(HttpStatusCode.BadRequest, " ", typeof(string))]
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

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("GetPurshasedProducts")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.OK, "", typeof(List<CompressedProductDTO>))]
        [SwaggerResponse(HttpStatusCode.BadRequest, " ", typeof(string))]
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
        [System.Web.Http.Route("GetListOnSale")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.OK, "Get compressed products on sale", typeof(List<CompressedProductDTO>))]
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
        [System.Web.Http.Route("GetOriginalPurshasedProduct")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.OK, "", typeof(OriginalProductDTO))]
        [SwaggerResponse(HttpStatusCode.BadRequest, " ", typeof(string))]
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
    }
}
