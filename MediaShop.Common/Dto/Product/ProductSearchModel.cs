﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaShop.Common.Dto.Product
{
    /// <summary>
    /// модель для передачи операндов и операций для дерева выражений
    /// </summary>
    public class ProductSearchModel
    {
        /// <summary>
        ///  Gets or setst Properties of product model
        /// </summary>
        public string LeftValue { get; set; }

        /// <summary>
        ///  Gets or setst = or >= or  something else
        /// </summary>
        public string Operand { get; set; }

        /// <summary>
        /// Gets or setst value to search
        /// </summary>
        public dynamic RightValue { get; set; }
    }
}
