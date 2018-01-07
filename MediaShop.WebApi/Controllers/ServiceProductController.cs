using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using MediaShop.Common.Dto;
using MediaShop.Common.Models;
using MediaShop.Common.Models.Content;

namespace MediaShop.WebApi.Controllers
{
    [RoutePrefix("api/products")]
    public class ServiceProductController : ApiController
    {
        [HttpGet]
        [Route("all")]
        public IHttpActionResult GetAllProducts()
        {
            //возвращаем список продуктов со свойствами, определенными ProductDto
            return this.Ok(new List<ProductDto>());
        }

        [HttpGet]
        [Route("id")]
        public IHttpActionResult GetProductsById(int id)
        {
            //возвращаем модельку ProductDto по Id продукта
            return this.Ok(new ProductDto());
        }

        [HttpGet]
        [Route("typeid")]
        public IHttpActionResult GetProductsByTypeId(int typeId) //следует пересмотреть тип int в TypeId (byte к примеру)
        {
            //возвращаем список ProductDto по TypeId продукта
            return this.Ok(new List<ProductDto>());
        }

        //заполняем модель Product, возвращаем ProductDto и 201
        [HttpPost]
        public IHttpActionResult Post([FromBody]Product product)
        {
            return this.Content<ProductDto>(HttpStatusCode.Created, new ProductDto());
        }

        //вносим изменения в модель Product
        [HttpPut]
        public IHttpActionResult Put([FromBody] Product product)
        {
            return this.Ok(new Product());
        }

        //удаление по Id продукта
        [HttpDelete]
        public IHttpActionResult Delete([FromBody] int id)
        {
            return this.Ok();
        }
    }
}
