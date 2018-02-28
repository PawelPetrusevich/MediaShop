import { ContentType } from '@angular/http/src/enums';

export class CompressedProductDto {
  Id: number;
  ProductName: string;
  Content: string;
  ProductPrice: number;
  ProductType: ContentType;
}
