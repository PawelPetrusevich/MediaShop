import {Item} from './item';
export class PayPalPaymentDto {
  Currency: string;
  Total: number;
  Items: [Item];
}
