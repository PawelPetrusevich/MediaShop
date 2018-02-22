import { Component, OnInit } from '@angular/core';
import { ProductService } from '../../../Services/product-service.service';
import { CompressedProductDto } from '../../../Models/Content/CompressedProductDto';
import { OriginalProductDTO } from '../../../Models/Content/OriginalProductDto';

@Component({
  selector: 'app-product-download',
  templateUrl: './product-download.component.html',
  styleUrls: ['./product-download.component.css']
})
export class ProductDownloadComponent implements OnInit {

  constructor(private productService: ProductService) { }
  purshasedProductList: CompressedProductDto[];

  ngOnInit() {
   this.productService.getListPurshasedProducts()
    .subscribe((resp: CompressedProductDto[]) => this.purshasedProductList = resp);
  }
  onDownloading(id: number) {
    this.productService.downloadProduct(id);
  }
}
