import { Component, OnInit } from '@angular/core';
import { ProductService } from '../../../Services/product-service.service';
import { CompressedProductDto } from '../../../Models/Content/CompressedProductDto';
import { ProductSearchModel } from '../../../Models/Content/ProductSearchModel';

@Component({
  selector: 'app-product-filter',
  templateUrl: './product-filter.component.html',
  styleUrls: ['./product-filter.component.css']
})
export class ProductFilterComponent implements OnInit {

  compressedProductList: CompressedProductDto[];
  conditionList: ProductSearchModel[] = new  Array<ProductSearchModel>();
  hiddenDiv: Boolean = true;

  constructor(private productService: ProductService) {}

  ngOnInit() {
   this.AddCondition();
  }

  FindProduct() {
    this.productService.searchProduct(this.conditionList).subscribe((resp: CompressedProductDto[]) => {
      this.compressedProductList = resp;
      if (this.compressedProductList === undefined || this.compressedProductList.length === 0) {
        this.hiddenDiv = false;
      } else {
        this.hiddenDiv = true;
      }
    });


  }

  AddCondition() {
    let condition: ProductSearchModel;
    condition = new ProductSearchModel();
    condition.LeftValue = '';
    condition.RightValue = '';
    condition.Operand = '=';

    this.conditionList.push(condition);
  }

  DeleteCondition(condition ) {

    if (this.conditionList === undefined || this.conditionList.length <= 1) {
      return;
    }

    this.conditionList.forEach( (item, index) => {
      if (item.LeftValue === condition.LeftValue
        && item.Operand === condition.Operand
        && item.RightValue === condition.RightValue) {
          this.conditionList.splice(index, 1);
        }
    });
  }

}
