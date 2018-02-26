import { Component, OnInit } from '@angular/core';
import { ProductService } from '../../../Services/product-service.service';
import { CompressedProductDto } from '../../../Models/Content/CompressedProductDto';
import { Router } from '@angular/router';
import { ContentType } from '../../../Models/Content/ContentType';


@Component({
  selector: 'app-product-list-uploaded',
  templateUrl: './product-list-uploaded.component.html',
  styleUrls: ['./product-list-uploaded.component.css']
})
export class ProductListUploadedComponent implements OnInit {

  constructor(private productService: ProductService, private router: Router) { }
  productListUploaded: CompressedProductDto[];
  ContentType = ContentType;

  ngOnInit() {
  }

  ToInfo(id: number) {
  this.router.navigate(['product-info', id]);
  }
}
