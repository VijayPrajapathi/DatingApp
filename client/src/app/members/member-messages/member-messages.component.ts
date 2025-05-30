import { Component, inject, input, OnInit, output, ViewChild } from '@angular/core';
import { Message } from '../../_models/Message';
import { MessageService } from '../../_services/message.service';
import { TimeagoModule } from 'ngx-timeago';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-member-messages',
  imports: [TimeagoModule,FormsModule],
  templateUrl: './member-messages.component.html',
  styleUrl: './member-messages.component.css'
})
export class MemberMessagesComponent  {
  @ViewChild('messageForm') messageForm?: any;
  username = input.required<string>();
  messages= input.required<Message[]>();
  private messageService = inject(MessageService);
  messageContent = '';
  updateMessages = output<Message>();


  sendMessage(){
    this.messageService.sendMessage(this.username(),this.messageContent).subscribe({
      next: message => {
       this.updateMessages.emit(message);
       this.messageForm.reset();
      }
    })
  }
}
