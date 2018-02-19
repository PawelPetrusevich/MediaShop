import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import { Notification} from '../../Models/Messaging/notification';

@Injectable()
export class NotificationService {
  static url = 'http://localhost:51289/api';
  constructor(private http: Http) { }
  Notify(title: string, message: string) {
    const notification = new Notification();
    notification.Title = title;
    notification.Message = message;
    return this.http.post(NotificationService.url + '/Notification/', notification).map(resp => resp.json());
  }
}
