import { Component, OnInit } from '@angular/core';
import { ProductService } from '../../../Services/product-service.service';
import { ActivatedRoute, Params, Router, ActivatedRouteSnapshot } from '@angular/router';
import {Subscription} from 'rxjs/Subscription';
import { ProductInfoDto } from '../../../Models/Content/ProductInfoDto';
import { Cartservice } from '../../../services/cartservice';
import { ProductDto } from '../../../Models/Content/ProductDto';
import { ContentType } from '../../../Models/Content/ContentType';
import { OriginalProductDTO } from '../../../Models/Content/OriginalProductDto';
import * as FileSaver from 'file-saver';
import { NotificationsService } from 'angular2-notifications';
import { HttpErrorResponse } from '@angular/common/http';
import { ContentCartDto } from '../../../Models/Cart/content-cart-dto';
import { AccountService } from '../../../Services/User/AccountService';

@Component({
  selector: 'app-product-info',
  templateUrl: './product-info.component.html',
  styleUrls: ['./product-info.component.css']
})
export class ProductInfoComponent implements OnInit {

  productInfo: ProductInfoDto = new ProductInfoDto;
  id: number;
  private subscrition: Subscription;
  type: ContentType;
  ErrorMessage: String = '';



  constructor(private productService: ProductService,
     private activatedRouter: ActivatedRoute,
     private router: Router,
     private accauntService: AccountService,
      private cartService: Cartservice,
    private notificationService: NotificationsService) {
    this.subscrition = activatedRouter.params.subscribe(data => this.id = data['id']);

   }

  ngOnInit() {
    this.activatedRouter.params.forEach((params: Params) => {
    const id = +params['id'];
    this.productService.getProductById(id).subscribe(result =>  {
      this.productInfo = result;
      this.type = this.productInfo.ProductType;
    }
    );
    });
  }

  get Base64Image(): string {
    return 'data:image/jpg;base64,' + this.productInfo.Content;
  }

  get Base64Video(): string {
    return 'data:video/mp4;base64,' + this.productInfo.Content;
  }

  get Base64Audio(): string {
    return 'data:audio/mp3;base64,' + this.productInfo.Content;
  }

  ToCart() {
    this.cartService.addContent(this.productInfo.Id).subscribe(
      data => this.ShowOkInCart(data as ContentCartDto),
      error => this.ShowError(error as HttpErrorResponse)
    );
  }


  Download() {
    console.log('to servise');
    this.productService.downloadProduct(this.productInfo.Id).subscribe((response) => {
      switch (this.productInfo.ProductType) {
        case ContentType.Image:
        this.SaveImage(response);
        break;

        case ContentType.Music:
        this.SaveAudio(response);
        break;

        case ContentType.Video:
        this.SaveVideo(response);
        break;
      }
    },
    error => this.ShowError(error as HttpErrorResponse)
  );
  }

  SaveImage(downloadingFile: OriginalProductDTO) {
      const contentType = 'image/jpg';
      const base64str = downloadingFile.Content;
      const binary = atob(base64str);
      const len = binary.length;
      const buffer = new ArrayBuffer(len);
      const view = new Uint8Array(buffer);
      for (let i = 0; i < len; i++) {
        view[i] = binary.charCodeAt(i);
      }
      const blob = new Blob([view], { type: contentType });
      const fileName = downloadingFile.ProductName + '.jpg';
      FileSaver.saveAs(blob, fileName);
  }

  SaveAudio(downloadingFile: OriginalProductDTO) {
      const contentType = 'audio/mp3';
      const base64str = downloadingFile.Content;
      const binary = atob(base64str);
      const len = binary.length;
      const buffer = new ArrayBuffer(len);
      const view = new Uint8Array(buffer);
      for (let i = 0; i < len; i++) {
        view[i] = binary.charCodeAt(i);
      }
      const blob = new Blob([view], { type: contentType });
      const fileName = downloadingFile.ProductName + '.mp3';
      FileSaver.saveAs(blob, fileName);
    }

    SaveVideo(downloadingFile: OriginalProductDTO) {
        const contentType = 'video/*';
        const base64str = downloadingFile.Content;
        const binary = atob(base64str);
        const len = binary.length;
        const buffer = new ArrayBuffer(len);
        const view = new Uint8Array(buffer);
        for (let i = 0; i < len; i++) {
          view[i] = binary.charCodeAt(i);
        }
        const blob = new Blob([view], { type: contentType });
        const fileName = downloadingFile.ProductName + '.avi';
        FileSaver.saveAs(blob, fileName);
      }

      ShowError(error: HttpErrorResponse) {
        this.notificationService.error (
          'OperationError',
          error.error.Message,
          {
            timeOut: 5000,
            showProgressBar: true,
            clickToClose: true
          }
        );
      }

      ShowOkInCart(data: ContentCartDto) {
        //Ok messame send SignalR
      }


}
