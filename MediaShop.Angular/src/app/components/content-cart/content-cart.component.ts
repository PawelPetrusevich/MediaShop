import { Component, OnInit, Input } from '@angular/core';
import { ContentCartDto } from '../../Models/Cart/content-cart-dto';

@Component({
  selector: 'app-content-cart',
  templateUrl: './content-cart.component.html',
  styleUrls: ['./content-cart.component.css']
})
export class ContentCartComponent implements OnInit {
  contentCart1: ContentCartDto;
  @Input()contentCart: ContentCartDto;

  constructor() {
  }

 // constructor(object: ContentCartDto) {
//    this.contentCart = object;
 // }

  ngOnInit() {
  }

}
