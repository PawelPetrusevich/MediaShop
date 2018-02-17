import { Component, OnInit } from '@angular/core';
import {AccountService} from '../../../Services/User/AccountService';

@Component({
  selector: 'app-logout',
  templateUrl: './logout.component.html',
  styleUrls: ['./logout.component.css']
})
export class LogoutComponent implements OnInit {

  constructor(private accountService : AccountService) { }

  logut(id : number): void {
    this.accountService.logout(id).subscribe(resp => {
      localStorage.removeItem('token');
      localStorage.setItem('isAuthorized', 'false');
    });
  }

  ngOnInit() {
  }

}
