import { SignalR, ISignalRConnection, SignalRConnection, BroadcastEventListener } from "ng2-signalr";
import { AccountService } from "../Services/User/AccountService";
import { Injectable, SkipSelf, Optional, NgModule } from "@angular/core";
import { ModuleWithProviders } from "@angular/compiler/src/core";
import { SignalRConfig } from "./signalr-config";
import { environment } from "../../environments/environment";
import { NotificationsService } from "angular2-notifications";
import { NotificationDto } from "../Models/Messaging/notification";

@NgModule({})
export class SignalRServiceConnector {
    public connected: boolean;
    connection: ISignalRConnection;
    conx: SignalRConnection;
    onUpdateNotices = new BroadcastEventListener<NotificationDto>('UpdateNotices');

    constructor(private signalR: SignalR, @Optional() @SkipSelf() parentModule: SignalRServiceConnector, private notificationService: NotificationsService) {
        if (parentModule) {
            throw new Error(
                'CoreModule is already loaded. Import it in the AppModule only');
        }
        this.conx = this.signalR.createConnection();
        this.onUpdateNotices.subscribe((notification: NotificationDto) => this.showNotify(notification));
    }

    public Connect(reinitConf?: boolean) {
        if (reinitConf) {
            if (this.connected) {
                this.Disconnect();
            }
            this.conx = this.signalR.createConnection(SignalRConfig.createConfig());
        }
        if (this.connected) {
            return this.connection;
        }
        let context = this;
        this.conx.listen(this.onUpdateNotices);
        this.conx.start().then(function (con) {
            context.connection = con;
            context.connected = true;
        }, function (reason) {
            console.log(reason);
            console.log('WebSockt could not connected');
        });
    }

    public getConnection(): ISignalRConnection {
        return this.connection;
    }

    public getIsConnected(): boolean {
        return this.connected;
    }

    public Disconnect() {
        this.conx.stop();
        this.connected = false;
    }

    private showNotify(notification: NotificationDto) {
        this.notificationService.success(notification.Title, notification.Message, {
            position: ["bottom", "left"],
            timeOut: 1000,
            showProgressBar: false,
            lastOnBottom: true
        });
    }

    static forRoot(): ModuleWithProviders {
        return {
            ngModule: SignalRServiceConnector,
            providers: []
        };
    }
}
