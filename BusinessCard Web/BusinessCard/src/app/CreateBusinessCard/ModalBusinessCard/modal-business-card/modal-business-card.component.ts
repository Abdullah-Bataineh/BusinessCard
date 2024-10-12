import { Component, Input, OnInit } from '@angular/core';
import * as $ from 'jquery';
import { NgxSpinnerService } from 'ngx-spinner';
import { MessageService } from 'primeng/api';
import { Observable } from 'rxjs';
import { IBusinessCard } from 'src/app/Model/IBusinessCard';
import { BCService } from 'src/app/Service/BusinessCardService/bc.service';
@Component({
  selector: 'app-modal-business-card',
  templateUrl: './modal-business-card.component.html',
  styleUrls: ['./modal-business-card.component.css']
})
export class ModalBusinessCardComponent implements OnInit{
 @Input() ModalBusinessCardData!: IBusinessCard|undefined;
 @Input() modal:any;
 modalElement:any
 constructor(
  private messageService: MessageService,
  private spinner: NgxSpinnerService,
  private bsService:BCService)
  {}
  ngOnInit(): void {
    
  }
  public CreateBusinessCard(){
    console.log("modalElement",this.modalElement);
    console.log("modeal",this.modal);
    
    this.spinner.show();
    if (this.ModalBusinessCardData) {
      this.bsService.CreateBusinessCard(this.ModalBusinessCardData).subscribe({
        next: (res:any) => {          
          if (res.status === 200&&this.modal) {
            setTimeout(() => {
              this.spinner.hide();
              this.messageService.add({
              severity: 'success',
              summary: 'Successful',
              detail: 'You have successfully created a Business Card. Thank you.',
              life: 2000
            });
            }, 1000);
            this.modal.hide();
          } else {
            setTimeout(() => {
              this.spinner.hide();
              this.messageService.add({
                severity: 'warn',
                summary: 'Failed Please Try again',
                detail: 'Please Try Again Sumbiting Form submitting.',
              life: 2000
            });
            }, 1000);
            this.modal.hide();
          
          }
        },
        error: (err) => {
          console.error('Error creating business card:',err);
        }
      });
    }
  }

}
