import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import { Cart } from '../Models/Cart/cart';
import { ContentCartDto } from '../Models/Cart/content-cart-dto';
import { HttpParams, HttpClient, HttpHeaders } from '@angular/common/http';
import { ProductDto } from '../Models/Content/ProductDto';
import { ProductInfoDto } from '../Models/Content/ProductInfoDto';
import { AppSettings } from '../Settings/AppSettings';
import { environment } from '../../environments/environment';

@Injectable()
export class Cartservice {
  constructor(private http: HttpClient) {}

  get(): Observable<Cart> {
    return this.http
      .get <Cart> (environment.API_ENDPOINT  + 'api/cart/getcartasync');
  }

  deleteById(id: number): Observable<number> {
    const params = new HttpParams().set('id', id.toString());
    return this.http
      .delete<number>(environment.API_ENDPOINT + 'api/cart/deletecontentbyidasync', {params});
  }

  clearCart(): Observable<Cart> {
    const params = new HttpParams().set('userId', localStorage.getItem('userId'));
    return this.http
      .delete <Cart> (environment.API_ENDPOINT + 'api/cart/clearcartasync', {params});
  }

  addContent(id: number): Observable<ContentCartDto> {
    const params: HttpParams = new HttpParams();
    return this.http
    .post<ContentCartDto>(environment.API_ENDPOINT + 'api/cart/addasync/' + id, params);
  }
}
