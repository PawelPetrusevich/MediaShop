import { Component, OnInit } from '@angular/core';
import { AccountService } from '../../../Services/User/AccountService';
import { PasswordRecovery } from '../../../Models/User/password-recovery';
import { ActivatedRoute } from '@angular/router'

@Component({
  selector: 'app-password-recovery',
  templateUrl: './password-recovery.component.html',
  styleUrls: ['./password-recovery.component.css']
})
export class PasswordRecoveryComponent implements OnInit {
  model: PasswordRecovery;
  constructor(private accountService: AccountService, private route: ActivatedRoute) {
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

  recoveryPassword() {
    this.accountService.recoveryPassword(this.model).subscribe(resp => {
      console.log(resp);
    });
  }
}
