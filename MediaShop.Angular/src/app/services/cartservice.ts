import { Injectable } from '@angular/core';
import { Http, Response, RequestOptions, Headers } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import { Cart } from '../Models/Cart/cart';
import { ContentCartDto } from '../Models/Cart/content-cart-dto';
import { HttpParams, HttpClient } from '@angular/common/http';
import { ProductDto } from '../Models/Content/ProductDto';
import { ProductInfoDto } from '../Models/Content/ProductInfoDto';
import { AppSettings } from '../Settings/AppSettings';

@Injectable()
export class Cartservice {
  constructor(private http: HttpClient) {}

  get(): Observable<Cart> {
    return this.http
      .get <Cart> (AppSettings.API_PUBLIC  + 'api/cart/getcartasync');
  }

  deleteById(id: number): Observable<number> {
    const params = new HttpParams().set('id', id.toString());
    return this.http
      .delete<number>(AppSettings.API_PUBLIC + 'api/cart/deletecontentbyidasync', {params});
  }

  clearCart(): Observable<Cart> {
    const params = new HttpParams().set('userId', localStorage.getItem('userId'));
    return this.http
      .delete <Cart> (AppSettings.API_PUBLIC + 'api/cart/clearcartasync', {params});
  }

  addContent(id: number): Observable<ContentCartDto> {
    const params = new HttpParams().set('contentId', id.toString());
    return this.http
      .post <ContentCartDto> (AppSettings.API_PUBLIC + 'api/cart/addasync', {params});
  }
}
