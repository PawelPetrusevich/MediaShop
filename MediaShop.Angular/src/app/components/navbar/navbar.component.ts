import { Component, OnInit } from '@angular/core';
import { AccountService } from '../../Services/User/AccountService';


@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  constructor(private accountService: AccountService) { }

  ngOnInit() {
  }

}
