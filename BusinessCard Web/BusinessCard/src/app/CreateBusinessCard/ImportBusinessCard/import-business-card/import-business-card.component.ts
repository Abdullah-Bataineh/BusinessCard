import { Component } from '@angular/core';
import { ToastService } from 'src/app/Service/Toast/toast.service';

@Component({
  selector: 'app-import-business-card',
  templateUrl: './import-business-card.component.html',
  styleUrls: ['./import-business-card.component.css']
})
export class ImportBusinessCardComponent {
  selectedFile: File | null = null;

  constructor(private toastservice:ToastService) {}

  onFileChange(event: Event) {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files.length > 0) {
        const file = input.files[0];
        const fileType = file.type;

         if (fileType === 'text/csv' || fileType === 'application/xml' || fileType === 'text/xml') {
          this.selectedFile = file; 

          this.toastservice.showToast({
              severity: "success",
              summary: 'File Uploaded',
              detail: 'The selected file is valid and has been uploaded.',
              life: 5000
          });
      } else {
          
          alert('Please upload a valid CSV or XML file.');
          input.value = ''; 
          
          this.toastservice.showToast({
              severity: "warn",
              summary: 'Invalid File Type',
              detail: 'Please upload a valid CSV or XML file.',
              life: 5000
          });
      }
    }
  }

  uploadFile() {
    console.log(this.selectedFile);
    
    if (this.selectedFile) {
      const formData: FormData = new FormData();
      formData.append('file', this.selectedFile, this.selectedFile.name);
        console.log(formData);
        
    }
  }
}
