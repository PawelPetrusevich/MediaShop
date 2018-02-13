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
  isLoaded = false;
  showError = false;

  constructor(private cartService: Cartservice) { }

  ngOnInit() {
  }

  getCart(cart: Cart) {
    this.cartService.get().subscribe(resp => {
      this.cart = resp;
      this.isLoaded = true;
    }
    );
  }

}
