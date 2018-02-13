import { ContentCartDto } from './content-cart-dto';

export class Cart {
  ContentCartCollection: [ContentCartDto];
  PriceAllItemsColection: number;
  CountItemsInCollection: number;
}
