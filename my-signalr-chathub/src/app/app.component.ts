import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ChatComponent } from './chat/chat.component';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  standalone: true,
  imports: [
    RouterModule,
    CommonModule,
    ChatComponent
    // Other components or modules used in this component
  ],
})
export class AppComponent {
  title = 'my-signalr-chathub';
}
