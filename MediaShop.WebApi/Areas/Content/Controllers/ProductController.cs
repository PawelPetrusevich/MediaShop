﻿using System;
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
        [SwaggerResponse(HttpStatusCode.OK, " ", typeof(UploadProductModel))]
        [SwaggerResponse(HttpStatusCode.BadRequest, " ", typeof(string))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, " ")]
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

        [HttpPost]
        [Route("Find")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.OK, " ", typeof(List<ProductDto>))]
        [SwaggerResponse(HttpStatusCode.BadRequest, " ", typeof(string))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, " ")]
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
            catch (InvalidOperationException)
            {
                return BadRequest(Resources.DeleteWithNullId);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [HttpPost]
        [Route("download")]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.OK, "", typeof(DownloadProductDto))]
        [SwaggerResponse(HttpStatusCode.BadRequest, " ", typeof(string))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, " ")]
        public IHttpActionResult DownloadProduct([FromBody] long id)
        {
            if (id <= 0)
            {
                return BadRequest(Resources.GetWithNullId);
            }

            try
            {
                return Ok(_productService.DownloadProduct(id));
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