import { SignalRConfiguration } from "ng2-signalr";
import { AppSettings } from "../Settings/AppSettings";

export class SignalRConfig {
    static createConfig(): SignalRConfiguration {
        const c = new SignalRConfiguration();
        c.hubName = 'NotificationHub';
        c.qs = { user: 'donald' };
        c.url = AppSettings.API_ENDPOINT;
        c.logging = true;

        c.executeEventsInZone = true; // optional, default is true
        c.executeErrorsInZone = false; // optional, default is false
        c.executeStatusChangeInZone = true; // optional, default is true
        return c;
    }

}
