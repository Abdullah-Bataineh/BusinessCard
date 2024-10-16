import { Component, Input } from '@angular/core';
import DataTable from 'datatables.net-dt';
import { NgxSpinnerService, Spinner } from 'ngx-spinner';
import { IBusinessCard } from 'src/app/Model/IBusinessCard';
import { BCService } from 'src/app/Service/BusinessCardService/bc.service';
import { ToastService } from 'src/app/Service/Toast/toast.service';

@Component({
  selector: 'app-delete-business-card',
  templateUrl: './delete-business-card.component.html',
  styleUrls: ['./delete-business-card.component.css']
})
export class DeleteBusinessCardComponent {
  @Input() ModalBusinessCardData!: IBusinessCard|undefined;
  @Input() modal:any;
  @Input() datatable:any;
  constructor(private bcservice:BCService,private spinner: NgxSpinnerService,private toastservice:ToastService){

  }
  DeleteBusinessCard(id: any) {    
    this.spinner.show(); 
    this.bcservice.DeleteBusinessCard(id).subscribe(
      (response) => {
        if (response.status === 200) {
          this.updateDatatable(id);
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
  updateDatatable(id:any){
  let data = this.datatable.data().toArray();
  const updatedData = data.filter((item: { id: any; }) => item.id !== id);
  this.datatable.clear();
  this.datatable.rows.add(updatedData);
  this.datatable.draw();
}
}
