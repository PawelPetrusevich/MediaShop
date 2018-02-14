import { ContentCartDto } from './content-cart-dto';

export class Cart {
  ContentCartDtoCollection: ContentCartDto[];
  PriceAllItemsCollection: number;
  CountItemsInCollection: number;
}
