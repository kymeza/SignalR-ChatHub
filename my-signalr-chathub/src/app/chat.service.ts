import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ChatService {
  private hubConnection!: HubConnection;
  private messageReceived = new Subject<string>();

  startConnection() {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl('https://localhost:5001/chatHub') // Adjust URL as needed
      .build();

    this.hubConnection
      .start()
      .then(() => console.log('Connection started'))
      .catch(err => console.log('Error while starting connection: ' + err));
  }

  joinChat(username: string) {
    return this.hubConnection.invoke('SetUsername', username)
  }

  sendMessage(message: string) {
    return this.hubConnection.invoke('SendMessage', message)
  }

  public onNewMessage = () => {
    this.hubConnection.on('NewMessage', (timestamp, username, message) => {
      const msg = `${timestamp} ${username} dice: ${message}`;
      this.messageReceived.next(msg);
    });
    return this.messageReceived.asObservable();
  }
}
