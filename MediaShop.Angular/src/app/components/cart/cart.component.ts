import { Component, OnInit } from '@angular/core';
import { Cart } from '../../Models/Cart/cart';
import { Cartservice } from '../../services/cartservice';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {
  cart: Cart = new Cart();
  id:number = 1;
  

  constructor(private cartService: Cartservice) { }

  ngOnInit() {
  }

  getCart(cart: Cart): void {
    this.cartService.get(this.id).subscribe(resp=>{
      this.cart = resp;
    }
    );
  }

}
