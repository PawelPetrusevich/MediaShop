import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
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
import { catchError } from 'rxjs/operators';

@Injectable()
export class ProductService {
  constructor(private http: HttpClient) {
  }


  compressedProductList: CompressedProductDto[];
  /*private webApiUrl = 'http://localhost:51289/api/product/';*/
  private webApiUrl = 'http://demo.belpyro.net/api/product/';

  getListProduct() {
    return this.http.get(this.webApiUrl + 'GetListOnSale');
  }

  uploadProduct(uploadProduct: UploadProductModel) {
    return this.http.post(this.webApiUrl + 'addAsync', uploadProduct);
  }

  getProductById(ID: number) {
    return this.http.get<ProductInfoDto>(this.webApiUrl + 'getById/' + ID);
  }

  searchProduct(conditionList: ProductSearchModel[]) {
    return this.http.post<CompressedProductDto[]>(this.webApiUrl + 'FindAsync', conditionList);
  }

  getListPurshasedProducts() {
    return this.http.get(this.webApiUrl + 'GetPurshasedProductsAsync');
  }

  downloadProduct(ID: number) {
    return this.http.get<OriginalProductDTO>(this.webApiUrl + 'GetOriginalPurshasedProductAsync/' + ID);
  }

  deleteProduct(id: number) {
    return this.http.delete<ProductDto>(this.webApiUrl + 'deleteAsync/' + id);
  }
}
