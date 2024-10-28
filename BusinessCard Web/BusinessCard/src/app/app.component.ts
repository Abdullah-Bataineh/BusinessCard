import { Component, OnInit } from '@angular/core';
import { ToastService } from './Service/Toast/toast.service';
import { MessageService } from 'primeng/api';
import { IToast } from './Model/IToast';
import { Title } from '@angular/platform-browser';
import { ActivatedRoute, NavigationEnd, Router } from '@angular/router';
import { filter, map, mergeMap } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  title = 'BusinessCard';
constructor(private toastservice:ToastService,private messageService: MessageService,private titleService: Title, private router: Router, private activatedRoute: ActivatedRoute){}
  ngOnInit(): void {
    this.toastservice.toastCurrentMessage.subscribe((message:IToast|null)=>{
      if(message)
      this.messageService.add(message)
    })


    this.router.events.pipe(
      filter(event => event instanceof NavigationEnd),
      map(() => {
        let route = this.activatedRoute.firstChild;
        while (route?.firstChild) route = route.firstChild;
        return route;
      }),
      filter(route => route?.outlet === 'primary'),
      mergeMap(route => route?.data || [])
    ).subscribe(data => {
      this.titleService.setTitle(data['title'] || 'Default Title');
    });
  }

  
  
}
