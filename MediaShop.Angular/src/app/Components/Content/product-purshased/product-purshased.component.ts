import { Component, OnInit, Input } from '@angular/core';
import { ProductService } from '../../../Services/product-service.service';
import { CompressedProductDto } from '../../../Models/Content/CompressedProductDto';
import { Router } from '@angular/router';
import { ContentType } from '../../../Models/Content/ContentType';


@Component({
  selector: 'app-product-purshased',
  templateUrl: './product-purshased.component.html',
  styleUrls: ['./product-purshased.component.css']
})
export class ProductPurshasedComponent implements OnInit {

  constructor(private productService: ProductService, private router: Router) { }
  productList: CompressedProductDto[];
  ContentType = ContentType;

  ngOnInit() {
   this.productService.getListPurshasedProducts()
    .subscribe((resp: CompressedProductDto[]) => {
      this.productList = resp;
    });
}

ToInfo(id: number) {
this.router.navigate(['product-info', id]);
}

}
