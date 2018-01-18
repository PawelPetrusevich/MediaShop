using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaShop.Common.Dto.Product
{
    public class FindDto
    {
        public string Name { get; set; } //price

        public string Op { get; set; } //= or >=

        public dynamic Value { get; set; } //100 
    }
}
