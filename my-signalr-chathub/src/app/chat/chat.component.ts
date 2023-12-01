import { Component, OnInit } from '@angular/core';
import { ChatService } from '../chat.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css'],
  standalone : true,
  imports: [
    CommonModule,
    FormsModule,
    ButtonModule,
    InputTextModule,
  ]
})
export class ChatComponent implements OnInit {
  username: string = '';
  message: string = '';
  messages: string[] = [];
  isUsernameSet: boolean = false;
  
  constructor( private chatService: ChatService ) { }

  ngOnInit(): void {
    this.chatService.startConnection();
    this.chatService.onNewMessage().subscribe(msg => {
      this.messages.push(msg);
    });
  }

  join() {
    if (this.username) {
      this.isUsernameSet = true;
      this.chatService.joinChat(this.username).catch(error => {
        console.error(error);
        this.isUsernameSet = false; 
      });
    }
  }

  send() {
    this.chatService.sendMessage(this.message);
  }

}
