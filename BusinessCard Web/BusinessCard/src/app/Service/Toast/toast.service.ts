import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { IToast } from 'src/app/Model/IToast';

@Injectable({
  providedIn: 'root'
})
export class ToastService {
  private toastSource = new BehaviorSubject<IToast|null>(null);
  toastCurrentMessage = this.toastSource.asObservable();
  constructor() { }
  showToast(data: IToast) {
    this.toastSource.next(data); 
  }
}
