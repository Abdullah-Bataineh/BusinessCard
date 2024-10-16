import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { NgxSpinnerService} from 'ngx-spinner';
import { BCService } from 'src/app/Service/BusinessCardService/bc.service';
import { ToastService } from 'src/app/Service/Toast/toast.service';
import * as bootstrap from 'bootstrap';
@Component({
  selector: 'app-import-business-card',
  templateUrl: './import-business-card.component.html',
  styleUrls: ['./import-business-card.component.css']
})
export class ImportBusinessCardComponent implements OnInit{
  IsmodelShow:boolean=false;
  selectedFile: File | null = null;
  @Input() modal:any;
    @Output() dataImport: EventEmitter<string> = new EventEmitter<string>();

  constructor(private fileimportservice:BCService,private toastservice:ToastService,private spinner: NgxSpinnerService) {}
  ngOnInit(): void {
  }

  onFileChange(event: Event) {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files.length > 0) {
      const file = input.files[0];
      const fileType = file.type;

      if (fileType === 'text/csv' || fileType === 'application/xml' || fileType === 'text/xml') {
        this.selectedFile = file;

        this.toastservice.showToast({
          severity: 'success',
          summary: 'File Uploaded',
          detail: 'The selected file is valid and has been uploaded.',
          life: 5000,
        });
      } else {
        alert('Please upload a valid CSV or XML file.');
        input.value = '';
        this.toastservice.showToast({
          severity: 'warn',
          summary: 'Invalid File Type',
          detail: 'Please upload a valid CSV or XML file.',
          life: 5000,
        });
      }
    }
  }

  uploadFile() {
   

    if (this.selectedFile) {
      this.spinner.show();
  
      const formData: FormData = new FormData();
      formData.append('file', this.selectedFile, this.selectedFile.name);
  
      this.fileimportservice.FileImportBusinessCard(formData)
        .subscribe(
          (response: any) => {
            if (response.status === 200) {
              setTimeout(()=>{
console.log('Upload successful!', response);
this.sendData(response.body)
              this.toastservice.showToast({
                severity: 'success',
                summary: 'Upload Successful',
                detail: 'File uploaded successfully!',
                life: 5000,
              });
              this.spinner.hide();
              this.modal.hide();
              },2000)
              
            } else {
              console.error('Upload failed with status:', response.status);
              setTimeout(()=>{
 this.toastservice.showToast({
                severity: 'error',
                summary: 'Upload Failed',
                detail: 'Unexpected server response.',
                life: 5000,
              });
              
              this.spinner.hide();
              this.modal.hide();

              },2000)
             
            }
          },
          (error) => {
            console.error('Upload failed!', error);
            setTimeout(()=>{
this.toastservice.showToast({
              severity: 'error',
              summary: 'Upload Failed',
              detail: 'There was an error uploading the file.',
              life: 5000,
            });
  
            this.spinner.hide();
            this.modal.hide();

            },2000)
            
          }
        );
    } else {
      alert('Please select a file to upload.');
    }
  }
  sendData(data:any) {
    this.dataImport.emit(data);  // Emit the data to the parent
  }
}
