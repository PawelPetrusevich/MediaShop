import { Component, OnInit } from '@angular/core';
import { AccountService } from '../../../Services/User/AccountService';
import { PasswordRecovery } from '../../../Models/User/password-recovery';
import { ActivatedRoute, Router } from '@angular/router';
import { NgForm } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-password-recovery',
  templateUrl: './password-recovery.component.html',
  styleUrls: ['./password-recovery.component.css']
})
export class PasswordRecoveryComponent implements OnInit {
  model: PasswordRecovery;
  showMessage: boolean;
  message: string;
  constructor(private accountService: AccountService, private route: ActivatedRoute, private router: Router) {
    this.model = new PasswordRecovery();
  }

  ngOnInit() {
    this.route
      .queryParams
      .subscribe(params => {
        this.model.Email = params['email'];
        this.model.Token = params['token'];
      });
  }

  recoveryPassword(f: NgForm) {
    if (this.model.ConfirmPassword != this.model.Password || f.invalid)
      return;
    this.accountService.recoveryPassword(this.model).subscribe(resp => {
      this.router.navigate(['login']);
    },
      (err: HttpErrorResponse) => {
        this.showMessage = true;
        this.message = err.error.ExceptionMessage;
      });
  }
}
