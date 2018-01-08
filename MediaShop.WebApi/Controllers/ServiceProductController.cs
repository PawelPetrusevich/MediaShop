using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using MediaShop.Common.Dto;
using MediaShop.Common.Interfaces.Services;
using MediaShop.Common.Models;
using MediaShop.Common.Models.Content;
using Ninject.Infrastructure.Language;

namespace MediaShop.WebApi.Controllers
{
    [RoutePrefix("api/products")]
    public class ServiceProductController : ApiController
    {
        private IProductService service;

        public ServiceProductController(IProductService _service)
        {
            this.service = _service;
        }

        [HttpGet]
        [Route("all")]
        public IHttpActionResult GetAllProducts()
        {
            var result = this.service.Products().ToList();

            return this.Ok(result);
        }

        [HttpGet]
        [Route("id")]
        public IHttpActionResult GetProductsById(int id)
        {
            var result = this.service.Get(id);

            return this.Ok(result);
        }

        [HttpGet]
        [Route("typeid")]
        public IHttpActionResult GetProductsByTypeId(int typeId) //следует пересмотреть тип int в TypeId (byte к примеру)
        {
            var result = this.service.Find(x => x.ProductTypeId == typeId).Single();
            
            return this.Ok(result);
        }

        //заполняем модель Product, возвращаем ProductDto и 201
        [HttpPost]
        public IHttpActionResult Post([FromBody]Product product)
        {
            var result = this.service.Add(product);

            return this.Content<ProductDto>(HttpStatusCode.Created, result);
        }

        //вносим изменения в модель Product
        [HttpPut]
        public IHttpActionResult Put([FromBody] Product product)
        {
            var result = this.service.Update(product);

            return this.Ok(result);
        }

        //удаление по Id продукта
        [HttpDelete]
        public IHttpActionResult Delete([FromBody] int id)
        {
            var result = this.service.Delete(id);

            return this.Ok(result);
        }
    }
}
