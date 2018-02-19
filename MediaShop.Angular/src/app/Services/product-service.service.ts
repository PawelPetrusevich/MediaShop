import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Http, Headers, Response, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/toPromise';

import { CompressedProductDto } from '../Models/Content/CompressedProductDto';
import { UploadProductModel } from '../Models/Content/UploadProductModel';
import { ProductDto } from '../Models/Content/ProductDto';
import { ProductSearchModel } from '../Models/Content/ProductSearchModel';
import { OriginalProductDTO } from '../Models/Content/OriginalProductDto';
import { ProductInfoDto } from '../Models/Content/ProductInfoDto';

@Injectable()
export class ProductService {

  constructor(private http: HttpClient ) { }

  compressedProductList: CompressedProductDto[];
  private webApiUrl = 'http://localhost:51289/api/product/';

  getListProduct() {
    return this.http.get(this.webApiUrl + 'GetListOnSale');
  }

  uploadProduct(uploadProduct: UploadProductModel) {
    const header = new HttpHeaders().set('Content-Type', 'application/json');
    return this.http.post(this.webApiUrl + 'add', uploadProduct, {headers: header});
  }

  getProductById(ID: number) {
    return this.http.get<ProductInfoDto>(this.webApiUrl + 'getById/' + ID);
  }

  searchProduct(productSearchModel: ProductSearchModel) {
    return this.http.post<ProductDto>(this.webApiUrl + 'Find', productSearchModel);
  }

  getListPurshasedProducts(UserID: number) {
    return this.http.get<ProductDto>(this.webApiUrl + 'GetPurshasedProducts');
  }

  downloadProduct(ID: number, UserID: Number) {
    return this.http.get<OriginalProductDTO>(this.webApiUrl + 'GetOriginalPurshasedProduct');
  }
}
