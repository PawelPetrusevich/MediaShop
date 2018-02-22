import { Component, OnInit } from '@angular/core';
import { ProductService } from '../../../Services/product-service.service';
import { ActivatedRoute, Params, Router } from '@angular/router';
import {Subscription} from 'rxjs/Subscription';
import { ProductInfoDto } from '../../../Models/Content/ProductInfoDto';
import { Cartservice } from '../../../services/cartservice';
import { ProductDto } from '../../../Models/Content/ProductDto';
import { ContentType } from '../../../Models/Content/ContentType';

@Component({
  selector: 'app-product-info',
  templateUrl: './product-info.component.html',
  styleUrls: ['./product-info.component.css']
})
export class ProductInfoComponent implements OnInit {

  productInfo: ProductInfoDto = new ProductInfoDto;
  id: number;
  private subscrition: Subscription;
  type: ContentType;


  constructor(private productService: ProductService, private activatedRouter: ActivatedRoute, private cartService: Cartservice) {
    this.subscrition = activatedRouter.params.subscribe(data => this.id = data['id']);
   }

  ngOnInit() {
    this.activatedRouter.params.forEach((params: Params) => {
    const id = +params['id'];
    this.productService.getProductById(id).subscribe(result =>  {
      this.productInfo = result;
      this.type = this.productInfo.ProductType;
    }
    );
    });
  }

  get Base64Image(): string {
    return 'data:image/jpg;base64,' + this.productInfo.Content;
  }

  get Base64Video(): string {
    return 'data:video/mp4;base64,' + this.productInfo.Content;
  }

  get Base64Audio(): string {
    return 'data:audio/mp3;base64,' + this.productInfo.Content;
  }

  ToCart() {
    console.log('run methods');
    this.cartService.addContent(this.productInfo.Id).subscribe();
  }

  DeleteProduct() {
    console.log('run methods');
    this.productService.deleteProduct(this.productInfo.Id).subscribe();
  }

}
