import { Component, OnInit } from '@angular/core';
import { Cart } from '../../Models/Cart/cart';
import { Cartservice } from '../../services/cartservice';
import { ContentCartDto } from '../../Models/Cart/content-cart-dto';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {
  cart: Cart = new Cart();
  isLoaded = false;
  showError = false;
  errorMessage: string;
  id: number;

  constructor(private cartService: Cartservice) {
    const item: ContentCartDto = new ContentCartDto();
    item.ContentId = 1;
    item.ContentName = 'name';
    item.DescriptionItem = 'Descr';
    item.PriceItem = 100;
    const item2: ContentCartDto = new ContentCartDto();
    item2.ContentId = 2;
    item2.ContentName = 'name2';
    item2.DescriptionItem = 'Descr2';
    item2.PriceItem = 20;
    const items: ContentCartDto[] = [ item, item2 ];
    this.cart.ContentCartCollection = items;
  }

  ngOnInit() {
  }

  getCart(cart: Cart) {
    this.cartService.get().subscribe(resp => {
      this.cart = resp;
      this.isLoaded = true;
    }, (err: HttpErrorResponse) => {
      if (err.status === 404) {
        this.showError = true;
      }
      this.errorMessage = err.statusText;
    }
    );
  }
  delete(element: ContentCartDto) {
    this.showError = false;
    this.cartService.delete(element).subscribe(resp => {
      const index = this.cart.ContentCartCollection.indexOf(resp, 0);
      if (index > -1) {
        this.cart.ContentCartCollection.splice(index, 1);
      }
    }, (err: HttpErrorResponse) => {
      if (err.status === 404) {
        this.showError = true;
      }
      this.errorMessage = err.statusText;
    }
  );
  }
  clearCart() {
    this.showError = false;
    this.cartService.clearCart(this.cart).subscribe(resp => {
      this.cart = resp;
      this.isLoaded = true;
    }, (err: HttpErrorResponse) => {
      if (err.status === 404) {
        this.showError = true;
      }
      this.errorMessage = err.statusText;
    }
  );
  }

}
