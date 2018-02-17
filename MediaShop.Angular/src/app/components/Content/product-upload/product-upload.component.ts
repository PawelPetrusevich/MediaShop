import { Component, OnInit } from '@angular/core';
import { UploadProductModel } from '../../../Models/Content/UploadProductModel';
import { ProductService } from '../../../Services/product-service.service';

@Component({
  selector: 'app-product-upload',
  templateUrl: './product-upload.component.html',
  styleUrls: ['./product-upload.component.css']
})
export class ProductUploadComponent implements OnInit {
  uploadProduct: UploadProductModel = new UploadProductModel();
  constructor(private productService: ProductService) {}

  ngOnInit() {}

  handleFileSelect(evt) {
    const files = evt.target.files;
    const file = files[0];
    if (files && file) {
      const read = new FileReader();
      read.onload = this._handleReaderLoaded.bind(this);
      read.readAsBinaryString(file);
    }
  }

  _handleReaderLoaded(readEvt) {
    const binaryString = readEvt.target.result;
    this.uploadProduct.UploadProduct = btoa(binaryString);
  }

  AddProduct(name: string, description: string) {
    this.uploadProduct.ProductName = name;
    this.uploadProduct.Description = description;
    this.uploadProduct.ProductPrice = 150;
    this.productService.uploadProduct(this.uploadProduct);
  }
}
