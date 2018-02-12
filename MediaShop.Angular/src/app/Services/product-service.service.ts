import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Http, Headers, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/toPromise';

import { CompressedProductDto } from '../Models/Content/CompressedProductDto';

@Injectable()
export class ProductService {

  constructor(private http: HttpClient ) { }

  compressedProductList: CompressedProductDto[];
  private webApiUrl = 'http://localhost:51289/api/product/';

  getListProduct() {
    return this.http.get(this.webApiUrl + 'GetListOnSale');
  }
}
