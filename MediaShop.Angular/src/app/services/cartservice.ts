import { Injectable } from '@angular/core';
import { Http, Response, RequestOptions, Headers } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import { Cart } from '../Models/Cart/cart';
import { ContentCartDto } from '../Models/Cart/content-cart-dto';
import { ProductDto } from '../Models/Content/ProductDto';
import { HttpParams } from '@angular/common/http';
import { AppSettings } from '../Settings/AppSettings';

@Injectable()
export class Cartservice {
  constructor(private http: Http) {}

  get(): Observable<Cart> {
    return this.http
      .get(AppSettings.API_ENDPOINT  + 'api/cart/getcartasync')
      .map(resp => resp.json())
      .catch(err => Observable.throw(err));
  }

  delete(contentCart: ContentCartDto): Observable<ContentCartDto> {
    const options = new RequestOptions();
    options.body = contentCart;
    return this.http
      .delete(AppSettings.API_ENDPOINT + 'api/cart/deletecontentasync', options)
      .map(resp => resp.json())
      .catch(err => Observable.throw(err));
  }

  deleteById(id: number): Observable<number> {
    const options = new RequestOptions();
    options.body = id;
    return this.http
      .delete(AppSettings.API_ENDPOINT + 'api/cart/deletecontentbyidasync', options)
      .map(resp => resp.json())
      .catch(err => Observable.throw(err));
  }

  clearCart(cart: Cart): Observable<Cart> {
    const options = new RequestOptions();
    options.body = cart;
    return this.http
      .delete(AppSettings.API_ENDPOINT + 'api/cart/clearcartasync', options)
      .map(resp => resp.json())
      .catch(err => Observable.throw(err));
  }

  addContent(product: ProductDto): Observable<ContentCartDto> {
    const params = new HttpParams().set('contentId', product.Id.toString());
    return this.http
      .post(AppSettings.API_ENDPOINT + 'api/cart/addasync', {params})
      .map(resp => resp.json())
      .catch(err => Observable.throw(err));
  }
}
