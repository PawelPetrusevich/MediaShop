import { Component, OnInit } from '@angular/core';
import { UploadProductModel } from '../../../Models/Content/UploadProductModel';
import { ProductService } from '../../../Services/product-service.service';
import { HttpErrorResponse } from '@angular/common/http';
import { NotificationsService } from 'angular2-notifications';
import { ProductDto } from '../../../Models/Content/ProductDto';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-product-upload',
  templateUrl: './product-upload.component.html',
  styleUrls: ['./product-upload.component.css'],
  providers: [ProductService]
})
export class ProductUploadComponent implements OnInit {
  uploadProduct: UploadProductModel = new UploadProductModel();


  constructor(
    private productService: ProductService,
    private notivicationsService: NotificationsService
  ) {}

  ngOnInit() {}

  handleFileSelect(evt) {
    const files = evt.target.files;
    const file = files[0];
    if (files && file) {
      const read = new FileReader();
      read.readAsBinaryString(file);
      read.onload = this._handleReaderLoaded.bind(this);
    }
  }

  _handleReaderLoaded(readEvt) {
    const binaryString = readEvt.target.result;
    this.uploadProduct.UploadProduct = btoa(binaryString);
  }

  AddProduct(form: NgForm) {
    this.productService.uploadProduct(this.uploadProduct).subscribe(
      data => {
        this.ShowUploadNotification(data as ProductDto);
      },
      error => this.ShowError(error as HttpErrorResponse)
    );
    this.uploadProduct.UploadProduct = null;
    form.reset();
  }

  ShowError(errorcode: HttpErrorResponse) {
      this.notivicationsService.error(
        'File Not Upload',
         errorcode.message,
          {
        timeOut: 5000,
        clickToClose: true
          });

  }
  ShowUploadNotification(data: ProductDto) {
    this.notivicationsService.success(
      'File Upload',
       data.ProductName + ' ' + data.ProductPrice.toString() + '$',
      {
      timeOut: 5000,
      clickToClose: true
      });

  }
}
