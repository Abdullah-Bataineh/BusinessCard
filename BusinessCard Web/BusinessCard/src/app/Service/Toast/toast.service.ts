import { Injectable } from '@angular/core';
import { MessageService } from 'primeng/api';

@Injectable({
  providedIn: 'root'
})
export class ToastService {

  constructor(private messageService: MessageService) { }
  showToastRequiredFields() {
    this.messageService.add({
      severity: 'warn',
      summary: 'Required Fields Missing',
      detail: 'Please fill in all required fields or check the input format before submitting.',
      life: 5000,
    });
  }
}
