import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { SignalR, SignalRConnection, ISignalRConnection } from 'ng2-signalr';
import { Injectable } from '@angular/core';

@Injectable()
export class SignalRconnectionResolver implements Resolve<SignalRConnection> {

    constructor(private _signalR: SignalR)  { }
    
    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): any {
        console.log('ConnectionResolver. Resolving...');
        return this._signalR.connect();
    }
}
