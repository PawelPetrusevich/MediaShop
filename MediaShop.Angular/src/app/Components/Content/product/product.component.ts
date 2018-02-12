import { Component, OnInit } from '@angular/core';
import { ProductService } from '../../../Services/product-service.service';
import { CompressedProductDto } from '../../../Models/Content/CompressedProductDto';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css'],
  providers: [ProductService],
})
export class ProductComponent implements OnInit {

  compressedProductList: CompressedProductDto[];

  constructor(private productService: ProductService) { }

  ngOnInit() {
    this.loadCompressedProductList();
  }

  loadCompressedProductList() {
    return this.productService.getListProduct().subscribe((response: CompressedProductDto[]) => this.compressedProductList = response);
  }
}
