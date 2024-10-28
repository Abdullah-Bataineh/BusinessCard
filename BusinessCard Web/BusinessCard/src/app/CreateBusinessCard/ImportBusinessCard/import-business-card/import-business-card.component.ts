import { Component, ElementRef, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { NgxSpinnerService} from 'ngx-spinner';
import { BCService } from 'src/app/Service/BusinessCardService/bc.service';
import { ToastService } from 'src/app/Service/Toast/toast.service';
import { BarcodeFormat } from '@zxing/library';
import * as bootstrap from 'bootstrap';
import { IBusinessCard } from 'src/app/Model/IBusinessCard';
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
    @ViewChild('canvasElement', { static: false })
  canvasElement!: ElementRef<HTMLCanvasElement>;

  constructor(private fileimportservice:BCService,private toastservice:ToastService,private spinner: NgxSpinnerService) {}
  ngOnInit(): void {
  }
  device: MediaDeviceInfo | undefined;
  torch: boolean = false; 
  formats: BarcodeFormat[] = [BarcodeFormat.QR_CODE];
  isScanning: boolean = false;
  isQrCodeDetected = false;
  qrScanning:boolean=true

  ngOnDestroy() {
    this.stopScanning();
  }

 
  
  handleQrCodeCaptured(result: string) {
    console.log('QR Code captured:', result);
    this.isQrCodeDetected = true;
  
    this.captureImage().then((imageDataUrl) => {
      this.selectedFile = this.dataURLtoFile(imageDataUrl);
      console.log(this.selectedFile);
          this.uploadFile();
    });
  
    this.stopScanning();
    setTimeout(() => {
      this.modal.hide();
    }, 600);
  }

  async captureImage(): Promise<string> {
    const canvas: HTMLCanvasElement = this.canvasElement?.nativeElement;
    const video = document.querySelector('video'); 
  
    return new Promise((resolve, reject) => {
      if (video) {
        canvas.width = video.videoWidth;
        canvas.height = video.videoHeight;
        const ctx = canvas.getContext('2d');
        if (ctx) {
          ctx.drawImage(video, 0, 0);
          const imageDataUrl = canvas.toDataURL('image/png');
          console.log('Captured Image Data URL:', imageDataUrl);
          resolve(imageDataUrl); 
        } else {
          reject('Failed to get canvas context');
        }
      } else {
        reject('Video element not found');
      }
    });
  }
  
  dataURLtoFile(dataURL: string): File {
    const byteString = atob(dataURL.split(',')[1]); 
    const mimeString = dataURL.split(',')[0].split(':')[1].split(';')[0]; 
    const ab = new ArrayBuffer(byteString.length); 
    const ia = new Uint8Array(ab); 
    for (let i = 0; i < byteString.length; i++) {
      ia[i] = byteString.charCodeAt(i); 
    }
    return new File([ab], 'qrcode.png', { type: mimeString }); 
  }

  toggleScanMode() {
    this.isScanning = !this.isScanning; 
    if (!this.isScanning) {
      this.stopScanning(); 
    }
  }

  stopScanning() {
    this.isScanning = false; 
    this.isQrCodeDetected = false; 
   
  }
  onFileChange(event: Event) {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files.length > 0) {
      const file = input.files[0];
      const fileType = file.type;

      if (fileType === 'text/csv' || fileType === 'application/xml' || fileType === 'text/xml' || fileType==="image/jpeg"|| fileType==="image/png") {
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
      console.log(this.selectedFile);
      
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
    this.dataImport.emit(data);
  }
}
