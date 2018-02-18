import { Component, OnInit, Input} from '@angular/core';
import { ProductService } from '../../../Services/product-service.service';
import { CompressedProductDto } from '../../../Models/Content/CompressedProductDto';
import { Observable } from 'rxjs/observable';
import { ProductListComponent } from '../product-list/product-list.component';
import { Router } from '@angular/router';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css'],
  providers: [ProductService],
})
export class ProductComponent implements OnInit {

  @Input() compressedProduct: CompressedProductDto;
  constructor(private productService: ProductService, private router: Router) {
   }

  ngOnInit() {
  }

  get Base64Content(): string {
    return 'data:image/jpg;base64,' + this.compressedProduct.Content;
  }

  GoInfo() {
    this.router.navigate(['product-info', this.compressedProduct.Id]);
  }
}
