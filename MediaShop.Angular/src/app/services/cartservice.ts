import { Injectable } from '@angular/core';
import { Http, Response, RequestOptions, Headers } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import { Cart } from '../Models/Cart/cart';
import { ContentCartDto } from '../Models/Cart/content-cart-dto';

@Injectable()
export class Cartservice {
    constructor(private http: Http){}

        get(id: number): Observable<Cart>{
            var options = new RequestOptions();
            options.headers = new Headers();
            options.headers.append('id', id.toString());
            return this.http.get('http://localhost:51289/api/cart/getcartasync',options)
            .map(resp=>resp.json())
            .catch(err => Observable.throw(err));
        }

        delete(contentCart: ContentCartDto): Observable<ContentCartDto>{
            var options = new RequestOptions();
            options.body = contentCart;
            return this.http.delete('http://localhost:51289/api/cart/deletecontentasync', options)
            .map(resp=>resp.json())
            .catch(err=>Observable.throw(err));
        }

        clearCart(cart: Cart): Observable<Cart>{
            var options = new RequestOptions();
            options.body = cart;
            return this.http.delete('http://localhost:51289/api/cart/clearcartasync', options)
            .map(resp=>resp.json())
            .catch(err=>Observable.throw(err));
        }
}
