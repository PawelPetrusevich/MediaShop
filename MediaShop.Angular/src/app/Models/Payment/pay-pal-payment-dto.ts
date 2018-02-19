import { ItemDto } from './item-dto';

export class PayPalPaymentDto {
  Currency: string;
  Total: number;
  Items: ItemDto[];
}
