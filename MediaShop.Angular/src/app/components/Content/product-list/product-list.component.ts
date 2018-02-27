import { Component, OnInit, OnChanges } from '@angular/core';
import { ProductService } from '../../../Services/product-service.service';
import { CompressedProductDto } from '../../../Models/Content/CompressedProductDto';
import { ContentType } from '../../../Models/Content/ContentType';
import { ProductSearchModel } from '../../../Models/Content/ProductSearchModel';
import { HttpErrorResponse } from '@angular/common/http';
import { NotificationsService } from 'angular2-notifications';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements OnInit, OnChanges {

  constructor(private productService: ProductService,
    private notivicationsService: NotificationsService) { }
  compressedProductList: CompressedProductDto[];
  filtredProduct: CompressedProductDto[] = [];
  conditionList: ProductSearchModel[] = [];
  DivNotFoundHidden: Boolean = true;
  DivSearchFilterHidden: Boolean = true;
  TextSearchFilter: String = 'Search filter';
  CurrentFilter: number;
  CurrentSorting: number;
  LoadingMessage: string;

  ngOnChanges() {
  }

  ngOnInit() {
   this.GetAllProducts();
  }

  GetAllProducts() {
    this.LoadingMessage = 'Product list is loading ...';
    this.productService.getListProduct().subscribe((resp: CompressedProductDto[]) => {
      this.compressedProductList = resp;
      this.filter(0);
      this.LoadingMessage = '';
    });
  }

  ShowProductListIsEmpty() {
    return this.filtredProduct.length === 0 && this.LoadingMessage === '';
  }

  filter(filterType) {
    this.CurrentFilter = filterType;
    if (filterType === 0) {
      this.filtredProduct = this.compressedProductList;
    } else {
      this.filtredProduct = this.compressedProductList.filter(product => {
        return product.ProductType === filterType;
      });
    }
  }

  sort(sortBy: number) {
    this.CurrentSorting = sortBy;
    if (sortBy === 0) {
      this.filtredProduct.sort(this.SortByPriceAscending);
    } else {
      this.filtredProduct.sort(this.SortByPriceDescending);
    }
  }

  SortByPriceAscending(p1: CompressedProductDto, p2: CompressedProductDto) {
    if (p1.ProductPrice > p2.ProductPrice) { return 1; } else if (p1.ProductPrice === p2.ProductPrice) { return 0; } else { return -1; }
  }

  SortByPriceDescending(p1: CompressedProductDto, p2: CompressedProductDto) {
    if (p1.ProductPrice > p2.ProductPrice) { return -1; } else if (p1.ProductPrice === p2.ProductPrice) { return 0; } else { return 1; }
  }


  HideShowSearchFilter() {
    this.DivSearchFilterHidden = !this.DivSearchFilterHidden;
    if (this.DivSearchFilterHidden) {
      this.conditionList = [];
      this.TextSearchFilter = 'Search filter';
      this.GetAllProducts();
      this.DivNotFoundHidden = true;
    } else {
      this.AddCondition();
      this.TextSearchFilter = 'Cancel search filter';
    }
  }

  AddCondition() {
    let condition: ProductSearchModel;
    condition = new ProductSearchModel();
    condition.LeftValue = 'ProductPrice';
    condition.RightValue = '';
    condition.Operand = '=';

    this.conditionList.push(condition);
  }

  DeleteCondition(condition ) {

    if (this.conditionList === undefined || this.conditionList.length <= 1) {
      return;
    }

    this.conditionList.forEach( (item, index) => {
      if (item.LeftValue === condition.LeftValue
        && item.Operand === condition.Operand
        && item.RightValue === condition.RightValue) {
          this.conditionList.splice(index, 1);
        }
    });
  }

  FindProduct() {
    this.productService.searchProduct(this.conditionList).subscribe((resp: CompressedProductDto[]) => {
      this.compressedProductList = resp;
      this.filter(this.CurrentFilter);
      this.sort(this.CurrentSorting);
      if (this.compressedProductList === undefined || this.compressedProductList.length === 0) {
        this.ShowError('Nothing found!');
      }
    },
    (err: HttpErrorResponse) => {
      this.filtredProduct = [];
      this.ShowError(err.error.Message);
    });
  }

  ShowError(errorMessage: String) {
    this.notivicationsService.error(
      errorMessage,
      ' ',
        {
      timeOut: 5000,
      clickToClose: true
        });
    }
}
