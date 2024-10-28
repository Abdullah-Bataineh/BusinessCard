import { Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';
import { NgxSpinnerService, Spinner } from 'ngx-spinner';
import { IBusinessCard } from 'src/app/Model/IBusinessCard';
import { BCService } from 'src/app/Service/BusinessCardService/bc.service';
import { ToastService } from 'src/app/Service/Toast/toast.service';

@Component({
  selector: 'app-delete-business-card',
  templateUrl: './delete-business-card.component.html',
  styleUrls: ['./delete-business-card.component.css']
})
export class DeleteBusinessCardComponent implements OnChanges, OnInit{
  @Input() ModalDeleteBusinessCardData!: number|undefined;
  @Input() modal:any;
  @Output() rowDelete = new EventEmitter<number>();
  BusinessCardData:any;
  constructor(private bcservice:BCService,private spinner: NgxSpinnerService,private toastservice:ToastService){

  }
  ngOnInit() {
  }

  ngOnChanges(changes: SimpleChanges) {
    if (changes['ModalDeleteBusinessCardData']) {
      console.log('Updated Business Card Data:', this.ModalDeleteBusinessCardData);
      if(this.ModalDeleteBusinessCardData)
      this.GetBusinessCardById(this.ModalDeleteBusinessCardData)
    }
  }
  GetBusinessCardById(id:number){
    this.spinner.show(); 
    this.bcservice.GetBusinessCardById(id).subscribe(
      (response) => {
        if (response.status == 200) {
          this.BusinessCardData=response.body;
         } else {
          this.spinner.hide();
        }
      },
      (error) => {

        console.error('Error Get Business Card:', error);
        
      }
    );
  }
 
  DeleteBusinessCard(id: any) {    
    this.spinner.show(); 
    this.bcservice.DeleteBusinessCard(id).subscribe(
      (response) => {
        if (response.status == 200) {
          this.updateTable();
          this.spinner.hide();
          this.modal.hide();
          this.toastservice.showToast({
            severity: 'success',
            summary: 'Delete Successful',
            detail: 'Business card deleted successfully!',
            life: 5000,
          });
          
  
          
        } else {
          this.spinner.hide();
          this.modal.hide();
          this.toastservice.showToast({
            severity: 'error',
            summary: 'Delete Failed',
            detail: 'The ID Business Card Is Not Found.',
            life: 5000,
          });
        }
      },
      (error) => {
        this.spinner.hide();
        this.modal.hide();
        console.error('Error deleting business card:', error);
        this.toastservice.showToast({
          severity: 'error',
          summary: 'Delete Failed',
          detail: 'There was an error deleting the business card.',
          life: 5000,
        });
      }
    );
  }
  updateTable(){
    this.rowDelete.emit();
}
}
