import { Component, OnInit, Input } from '@angular/core';
import { ProductService } from '../../../Services/product-service.service';
import { CompressedProductDto } from '../../../Models/Content/CompressedProductDto';
import { Router } from '@angular/router';


@Component({
  selector: 'app-product-purshased',
  templateUrl: './product-purshased.component.html',
  styleUrls: ['./product-purshased.component.css']
})
export class ProductPurshasedComponent implements OnInit {

  @Input() purshasedProduct: CompressedProductDto;
  constructor(private productService: ProductService, private router: Router) { }
  purshasedProductList: CompressedProductDto[];

  ngOnInit() {
   this.productService.getListPurshasedProducts()
    .subscribe((resp: CompressedProductDto[]) => this.purshasedProductList = resp);
}

  get Base64Content(): string {
  return 'data:image/jpg;base64,' + this.purshasedProduct.Content;
  }

}
