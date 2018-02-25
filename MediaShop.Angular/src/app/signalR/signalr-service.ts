import { SignalR, ISignalRConnection, SignalRConnection, BroadcastEventListener } from "ng2-signalr";
import { AccountService } from "../Services/User/AccountService";
import { Injectable, SkipSelf, Optional, NgModule } from "@angular/core";
import { ModuleWithProviders } from "@angular/compiler/src/core";
import { SignalRConfig } from "./signalr-config";
import { environment } from "../../environments/environment.prod";

@NgModule({
    /* imports:      [ CommonModule ],
     declarations: [ TitleComponent ],
     exports:      [ TitleComponent ],
     providers:    [ UserService ]*/
})
export class SignalRServiceConnector {
    conx: SignalRConnection;
    onMessageSent$ = new BroadcastEventListener<Notification>('UpdateNotices');

    constructor(private signalR: SignalR, @Optional() @SkipSelf() parentModule: SignalRServiceConnector) {
        if (parentModule) {
            throw new Error(
                'CoreModule is already loaded. Import it in the AppModule only');
        }
        this.conx = this.signalR.createConnection();
    }

    public Connect(reinitConf?: boolean): Promise<ISignalRConnection> {
        if (reinitConf) {
            this.conx = this.signalR.createConnection(SignalRConfig.createConfig());
        }
        if (environment.enableSignalRLoging) {
            console.log("Connect");
        }
        this.conx.listen(this.onMessageSent$);
        this.onMessageSent$.subscribe((chatMessage: Notification) => {
            console.log(chatMessage);
        });
        return this.conx.start();
    }

    public Disconnect() {
        if (environment.enableSignalRLoging) {
            console.log("Disconnect");
        }
        this.conx.stop();
    }

    static forRoot(): ModuleWithProviders {
        return {
            ngModule: SignalRServiceConnector,
            providers: []
        };
    }
}
