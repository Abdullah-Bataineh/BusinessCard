import { HttpResponse } from '@angular/common/http';
import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { typeFile } from 'src/app/Enum/TypeFile';
import { IBusinessCard } from 'src/app/Model/IBusinessCard';
import { BCService } from 'src/app/Service/BusinessCardService/bc.service';
import { ToastService } from 'src/app/Service/Toast/toast.service';

@Component({
  selector: 'app-export-business-card',
  templateUrl: './export-business-card.component.html',
  styleUrls: ['./export-business-card.component.css']
})
export class ExportBusinessCardComponent implements OnChanges, OnInit{
  @Input() ModalExportBusinessCardData!: number|undefined;
  @Input() modal:any;
  BusinessCardData:any;
  constructor(private bcservice:BCService,private spinner: NgxSpinnerService,private toastservice:ToastService){

  }
  ngOnInit() {
  }

  ngOnChanges(changes: SimpleChanges) {
    if (changes['ModalExportBusinessCardData']) {
      console.log('Updated Business Card Data:', this.ModalExportBusinessCardData);
      if(this.ModalExportBusinessCardData)
      this.GetBusinessCardById(this.ModalExportBusinessCardData)
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
  exportToCSV() {
    this.modal.hide();
  
    if (this.BusinessCardData) {
      this.bcservice.ExportBusinessCard(this.BusinessCardData, typeFile.CSV).subscribe(
        (res) => {
          this.toastservice.showToast({
            severity: 'success',
            summary: 'Export Successful',
            detail: 'Business card exported to CSV successfully!',
            life: 5000,
          });
          this.downloadFile(res.body, 'business_card.csv');
        },
        error => {
          this.toastservice.showToast({
            severity: 'error',
            summary: 'Export Failed',
            detail: 'The  Business Card Is Not Exported Please Try Again Or Call Admin.',
            life: 5000,
          });
          console.error('Error exporting business card:', error);
        }
      );
    }
  }
  
  private downloadFile(data: ArrayBuffer | null, fileName: string) {
    if (data) {
      this.spinner.show();
      const blob = new Blob([data]);
      const url = window.URL.createObjectURL(blob);
      const a = document.createElement('a');
      a.href = url;
      a.download = fileName;
      document.body.appendChild(a);
      a.click();
      document.body.removeChild(a);
      window.URL.revokeObjectURL(url);
      this.toastservice.showToast({
        severity: 'success',
        summary: 'Download Successful',
        detail: 'Business card Downloaded successfully!',
        life: 5000,
      });
      setTimeout(()=>{
        this.spinner.hide();
      },1000)
    } else {
      this.toastservice.showToast({
        severity: 'error',
            summary: 'Download Failed',
            detail: 'The  Business Card Is Not Downloaded Please Try Again Or Call Admin.',
            life: 5000,
      });
      console.error('Data is null or undefined');
      this.spinner.hide();
    }
  }
  exportToXML(){
    this.modal.hide();
  
    if (this.BusinessCardData) {
      this.bcservice.ExportBusinessCard(this.BusinessCardData, typeFile.XML).subscribe(
        (res) => {
          this.toastservice.showToast({
            severity: 'success',
            summary: 'Export Successful',
            detail: 'Business card exported to XML successfully!',
            life: 5000,
          });
          this.downloadFile(res.body, 'business_card.xml');
        },
        error => { 
          this.toastservice.showToast({
            severity: 'error',
            summary: 'Export Failed',
            detail: 'The  Business Card Is Not Exported Please Try Again Or Call Admin.',
            life: 5000,
          });
          console.error('Error exporting business card:', error);
        }
      );
    }
  }
  exportToQrCode(){
    this.modal.hide();
  
    if (this.BusinessCardData) {
      this.BusinessCardData.photo=null;
      this.bcservice.ExportBusinessCard(this.BusinessCardData, typeFile.QRCODE).subscribe(
        (res) => {
          this.toastservice.showToast({
            severity: 'success',
            summary: 'Export Successful',
            detail: 'Business card exported to Qr Code successfully!',
            life: 5000,
          });
          this.downloadFile(res.body, 'business_card.png');
        },
        error => {
          this.toastservice.showToast({
            severity: 'error',
            summary: 'Export Failed',
            detail: 'The  Business Card Is Not Exported Please Try Again Or Call Admin.',
            life: 5000,
          });
          console.error('Error exporting business card:', error);
        }
      );
    }
  }

 


}
