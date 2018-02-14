import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Http, Headers, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/toPromise';

import { CompressedProductDto } from '../Models/Content/CompressedProductDto';
import { UploadProductModel } from '../Models/Content/UploadProductModel';
import { ProductDto } from '../Models/Content/ProductDto';

@Injectable()
export class ProductService {

  constructor(private http: HttpClient ) { }

  compressedProductList: CompressedProductDto[];
  private webApiUrl = 'http://localhost:51289/api/product/';

  getListProduct() {
    return this.http.get<CompressedProductDto[]>(this.webApiUrl + 'GetListOnSale');
  }

  uploadProduct(uploadProduct: UploadProductModel) {
    return this.http.post(this.webApiUrl + 'add', uploadProduct);
  }

  getProductById(ID: number) {
    return this.http.get<ProductDto>(this.webApiUrl + 'getById/' + ID);
  }
}
