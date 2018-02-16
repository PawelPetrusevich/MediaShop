import { Component, OnInit } from '@angular/core';
import { AccountService } from '../../../Services/User/AccountService';
import { PasswordRecovery } from '../../../Models/User/password-recovery';

@Component({
  selector: 'app-password-recovery',
  templateUrl: './password-recovery.component.html',
  styleUrls: ['./password-recovery.component.css']
})
export class PasswordRecoveryComponent implements OnInit {

  constructor(private accountService: AccountService) {

  }

  ngOnInit() {
  }

  recoveryPassword(token: string, email: string, password: string, confirmPassword: string) {
    let model = new PasswordRecovery();
    model.Email = email;
    model.Token = token;
    model.Password = password;
    model.ConfirmPassword = confirmPassword;
    console.log(model);
  }
}
