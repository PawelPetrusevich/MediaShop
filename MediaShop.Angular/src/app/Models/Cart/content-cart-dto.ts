import { StateCartContent } from './state-cart-content.enum';

  export class ContentCartDto {
    Id: number;
    ContentId: number;
    ContentName: string;
    CreatorName: string;
    DescriotionItem: string;
    PriceItem: number;
    StateContent: StateCartContent;
    CreatorId: number;
  }
