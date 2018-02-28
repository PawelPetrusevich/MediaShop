import { Component, OnInit } from '@angular/core';
import { ProductService } from '../../../Services/product-service.service';
import { CompressedProductDto } from '../../../Models/Content/CompressedProductDto';
import { Router } from '@angular/router';
import { ContentType } from '../../../Models/Content/ContentType';
import { NotificationsService } from 'angular2-notifications';
import { HttpErrorResponse } from '@angular/common/http';
import { ProductDto } from '../../../Models/Content/ProductDto';


@Component({
  selector: 'app-product-list-uploaded',
  templateUrl: './product-list-uploaded.component.html',
  styleUrls: ['./product-list-uploaded.component.css']
})
export class ProductListUploadedComponent implements OnInit {

  constructor(private productService: ProductService,
    private router: Router,
    private notificationService: NotificationsService
  ) { }
  productListUploaded: CompressedProductDto[];
  ContentType = ContentType;

  ngOnInit() {
    this.productService.getUploadProductList().subscribe(
      data => this.productListUploaded = data as CompressedProductDto[]
    );
  }

  DeleteProduct(id: number) {
    this.productService.deleteProduct(id).subscribe(
      data => {
        this.ShowOkDelete(data as ProductDto);
        this.productService.getUploadProductList().subscribe(
          result => this.productListUploaded = result as CompressedProductDto[]
        );
      },
      error => this.ShowError(error as HttpErrorResponse)
    );
  }

  ShowOkDelete(data: ProductDto) {
    this.notificationService.success(
      'Deleted',
      data.ProductName + ' deleted',
      {
        timeOut: 5000,
        showProgressBar: true,
        clickToClose: true
      }
    );
  }

  ShowError(error: HttpErrorResponse) {
    this.notificationService.error (
      'OperationError',
      error.error.Message,
      {
        timeOut: 5000,
        showProgressBar: true,
        clickToClose: true
      }
    );
  }

  ToInfo(id: number) {
    this.router.navigate(['product-info', id]);
  }
}
