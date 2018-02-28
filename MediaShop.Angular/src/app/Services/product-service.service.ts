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
import { environment } from '../../environments/environment';

@Injectable()
export class ProductService {
  constructor(private http: HttpClient) {
  }


  compressedProductList: CompressedProductDto[];

  getListProduct() {
    return this.http.get(environment.API_ENDPOINT + 'api/product/GetListOnSale');
  }

  uploadProduct(uploadProduct: UploadProductModel) {
    return this.http.post(environment.API_ENDPOINT + 'api/product/addAsync', uploadProduct);
  }

  getProductById(ID: number) {
    return this.http.get<ProductInfoDto>(environment.API_ENDPOINT + 'api/product/getById/' + ID);
  }

  searchProduct(conditionList: ProductSearchModel[]) {
    return this.http.post<CompressedProductDto[]>(environment.API_ENDPOINT + 'api/product/FindAsync', conditionList);
  }

  getListPurshasedProducts() {
    return this.http.get(environment.API_ENDPOINT + 'api/product/GetPurshasedProductsAsync');
  }

  downloadProduct(ID: number) {
    return this.http.get<OriginalProductDTO>(environment.API_ENDPOINT + 'api/product/GetOriginalPurshasedProductAsync/' + ID);
  }

  deleteProduct(id: number) {
    return this.http.delete<ProductDto>(environment.API_ENDPOINT + 'api/product/deleteAsync/' + id);
  }

  getUploadProductList() {
    return this.http.get<CompressedProductDto[]>(environment.API_ENDPOINT + 'api/product/GetUploadProductAsync');
  }
}
