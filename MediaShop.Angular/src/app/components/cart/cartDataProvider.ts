import { Injectable } from '@angular/core';
import { Cart } from '../../Models/Cart/cart';

@Injectable()
export class CartDataProvider {

    public storageCart: Cart = new Cart();
    public storageUrl: string;

    public constructor() { }
}
