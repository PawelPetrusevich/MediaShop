import { Component, OnInit } from '@angular/core';
import { ProductService } from '../../../Services/product-service.service';
import { ActivatedRoute, Params, Router } from '@angular/router';
import {Subscription} from 'rxjs/Subscription';
import { ProductInfoDto } from '../../../Models/Content/ProductInfoDto';
import { Cartservice } from '../../../services/cartservice';
import { ProductDto } from '../../../Models/Content/ProductDto';

@Component({
  selector: 'app-product-info',
  templateUrl: './product-info.component.html',
  styleUrls: ['./product-info.component.css']
})
export class ProductInfoComponent implements OnInit {

  productInfo: ProductInfoDto = new ProductInfoDto;
  id: number;
  private subscrition: Subscription;
  constructor(private productService: ProductService, private activatedRouter: ActivatedRoute, private cartService: Cartservice) {
    this.subscrition = activatedRouter.params.subscribe(data => this.id = data['id']);
   }

  ngOnInit() {
    this.activatedRouter.params.forEach((params: Params) => {
    const id = +params['id'];
    this.productService.getProductById(id).subscribe(result => this.productInfo = result);
    });

  }

  get Base64Image(): string {
    return 'data:image/jpg;base64,' + this.productInfo.Content;
  }

  ToCart() {
    this.cartService.addContent(this.productInfo.Id);
  }

}
