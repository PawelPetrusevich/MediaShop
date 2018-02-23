import { Component } from '@angular/core';
import { SignalRConnection } from 'ng2-signalr';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  private _connection: SignalRConnection;

  constructor(private route: ActivatedRoute) {
  }

  title = 'app';
  ngOnInit() {
   // this._connection = this.route.snapshot.data['connection'];
  }
}
