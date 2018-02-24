import { Component, OnInit } from '@angular/core';
import { SignalR } from 'ng2-signalr';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent {

  constructor(private signalR: SignalR) { }

  ngOnInit() {
    this.signalR.connect();
  }
}
