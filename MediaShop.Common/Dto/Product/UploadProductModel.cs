using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using FluentValidation.Attributes;
using MediaShop.Common.Dto.Product.ProductValidators;
using MediaShop.Common.Enums;

namespace MediaShop.Common.Dto.Product
{
    /// <summary>
    /// Модель для   загрузки модели
    /// </summary>
    [Validator(typeof(UploadModelValidator))]
    public class UploadProductModel
    {
        /// <summary>
        /// Gets or sets the ProductName.
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// Gets or sets the ProductPrice.
        /// </summary>
        public decimal ProductPrice { get; set; }

        /// <summary>
        /// Gets or sets the Description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Загружаемый файл
        /// </summary>
        public string UploadProduct { get; set; }
    }
}
