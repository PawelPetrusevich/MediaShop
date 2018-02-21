import { Component, OnInit, OnChanges } from '@angular/core';
import { ProductService } from '../../../Services/product-service.service';
import { CompressedProductDto } from '../../../Models/Content/CompressedProductDto';
import { ContentType } from '../../../Models/Content/ContentType';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements OnInit, OnChanges {

  constructor(private productService: ProductService) { }
  compressedProductList: CompressedProductDto[];
  filtredProduct: CompressedProductDto[] = [];


  ngOnChanges() {
  }

  ngOnInit() {
    this.productService.getListProduct().subscribe((resp: CompressedProductDto[]) => {
      this.compressedProductList = resp;
      this.filter(0);
    });
  }

  filter(filterType) {
    if (filterType === 0) {
      this.filtredProduct = this.compressedProductList;
    } else {
      this.filtredProduct = this.compressedProductList.filter(product => {
        return product.ProductType === filterType;
      });
    }
  }

  sort(sortBy: number) {
    if (sortBy === 0) {
      this.filtredProduct.sort(this.SortByPriceAscending);
    } else {
      this.filtredProduct.sort(this.SortByPriceDescending);
    }
  }

  SortByPriceAscending(p1: CompressedProductDto, p2: CompressedProductDto) {
    if (p1.ProductPrice > p2.ProductPrice) { return 1; } else if (p1.ProductPrice === p2.ProductPrice) { return 0; } else { return -1; }
  }

  SortByPriceDescending(p1: CompressedProductDto, p2: CompressedProductDto) {
    if (p1.ProductPrice > p2.ProductPrice) { return -1; } else if (p1.ProductPrice === p2.ProductPrice) { return 0; } else { return 1; }
  }

}
