import { Injectable } from '@angular/core';
import * as signalR from "@microsoft/signalr"
import { Subject } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  private hub: signalR.HubConnection | undefined
  refreshChart$ = new Subject<boolean>();
  constructor() {
    this.refreshChart$.next(true);
  }

  public startConnection = () => {
    this.hub = new signalR.HubConnectionBuilder()
                            .withUrl(`${environment.BaseUrl}/client`,{ skipNegotiation: true, transport: signalR.HttpTransportType.WebSockets})
                            .build();
    this.hub.start().then(() => console.log('Connection started'))
      .catch(err => console.log('Error while starting connection: ' + err));
  }

  public refreshChart = () => {
    this.hub?.on('refreshChart', () => {this.refreshChart$.next(!this.refreshChart$); console.log("Am primit prin webSocket");})
  }

  public notification = () => {
    this.hub?.on('notifyClient', () => alert("Energy limit exceeded!"))
  }
}
