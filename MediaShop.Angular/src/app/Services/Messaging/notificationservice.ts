import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import { NotificationDto } from '../../Models/Messaging/notification';
import { environment } from '../../../environments/environment';

@Injectable()
export class NotificationService {
  constructor(private http: Http) { }
  Notify(title: string, message: string) {
    const notification = new NotificationDto();
    notification.Title = title;
    notification.Message = message;
    return this.http.post(environment.API_ENDPOINT + '/Notification/', notification).map(resp => resp.json());
  }
}
