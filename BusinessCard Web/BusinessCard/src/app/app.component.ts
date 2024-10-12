import { Component, OnInit } from '@angular/core';
import { ToastService } from './Service/Toast/toast.service';
import { MessageService } from 'primeng/api';
import { IToast } from './Model/IToast';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  title = 'BusinessCard';
constructor(private toastservice:ToastService,private messageService: MessageService){}
  ngOnInit(): void {
    this.toastservice.toastCurrentMessage.subscribe((message:IToast|null)=>{
      if(message)
      this.messageService.add(message)
    })
  }
  
}
