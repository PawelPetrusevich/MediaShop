import { Component, OnInit } from '@angular/core';
import { ProductService } from '../../../Services/product-service.service';
import { CompressedProductDto } from '../../../Models/Content/CompressedProductDto';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements OnInit {

  constructor(private productService: ProductService) { }
  compressedProductList: CompressedProductDto[];

  ngOnInit() {
    this.productService.getListProduct().subscribe((resp: CompressedProductDto[]) => this.compressedProductList = resp);
  }

}
