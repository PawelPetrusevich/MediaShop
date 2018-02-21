import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AccountService } from '../../../Services/User/AccountService';
import { HttpErrorResponse } from '@angular/common/http';


@Component({
  selector: 'app-confirm',
  templateUrl: './confirm.component.html',
  styleUrls: ['./confirm.component.css']
})
export class ConfirmComponent implements OnInit {

  private email: string;
  private token: string;
  result: string;
  showError: boolean = false;
  constructor(private accountService: AccountService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.route
      .queryParams
      .subscribe(params => {
        this.email = params['email'];
        this.token = params['token'];
      });
    if (this.email && this.token) {
      this.accountService.confirm(this.email, this.token).subscribe(resp => {
        this.result = 'Welcome to Media shop!';
      }, (err: HttpErrorResponse) => {
        this.showError = true;
        this.result = "Oops! There seems to be a problem.";
      });
    }
  }

}
