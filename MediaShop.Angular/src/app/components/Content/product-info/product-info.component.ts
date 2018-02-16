import { Component, OnInit } from '@angular/core';
import { ProductService } from '../../../Services/product-service.service';
import { ProductDto } from '../../../Models/Content/ProductDto';

@Component({
  selector: 'app-product-info',
  templateUrl: './product-info.component.html',
  styleUrls: ['./product-info.component.css']
})
export class ProductInfoComponent implements OnInit {

  productInfo: ProductDto;
  constructor(private productService: ProductService) { }

  ngOnInit() {
  }

  getProductInfo(id: number) {
    return this.productService.getProductById(id).subscribe((data: ProductDto) => this.productInfo = data);
  }

}
