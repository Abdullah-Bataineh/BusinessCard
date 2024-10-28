import { Component, Input, OnInit } from '@angular/core';
import {  Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { IBusinessCard } from 'src/app/Model/IBusinessCard';
import { BCService } from 'src/app/Service/BusinessCardService/bc.service';
import { ToastService } from 'src/app/Service/Toast/toast.service';
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
  private spinner: NgxSpinnerService,
  private bsService:BCService,
private route:Router,
private toastservice:ToastService)
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
              this.toastservice.showToast({ 
                severity: 'success',
                summary: 'Successful',
                detail: 'You have successfully created a Business Card. Thank you.',
                life: 5000});
              this.route.navigate(['/ViewAllBusinessCards'])
            }, 1000);
            this.modal.hide();
           
          } else {
            setTimeout(() => {
              this.spinner.hide();
              this.toastservice.showToast({ 
                severity: 'warn',
                summary: 'Failed Please Try again',
                detail: 'Please Try Again Sumbiting Form submitting.',
              life: 5000});
            }, 1000);
            this.modal.hide();
          
          }
        },
        error: (err) => {
          console.error('Error creating business card:',err);
          setTimeout(() => {
            this.spinner.hide();
            this.toastservice.showToast({ 
              severity: 'warn',
              summary: 'Failed Please Try again',
              detail: err.error.message,
            life: 5000});
          }, 1000);
          this.modal.hide();
        }
      });
    }
  }

}
