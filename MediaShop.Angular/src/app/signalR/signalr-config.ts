import { SignalRConfiguration, ConnectionTransports } from "ng2-signalr";
import { environment } from "../../environments/environment";

export class SignalRConfig {
    static createConfig(): SignalRConfiguration {
        const c = new SignalRConfiguration();
        c.hubName = 'NotificationHub';
        c.qs = { 'access_token': localStorage.getItem(environment.tokenKey) };
        c.url = environment.API_ENDPOINT;
        c.logging = environment.enableSignalRLoging;
        c.transport = [ConnectionTransports.webSockets];
        c.executeEventsInZone = true;
        c.executeErrorsInZone = false;
        c.executeStatusChangeInZone = true;
        return c;
    }
}
