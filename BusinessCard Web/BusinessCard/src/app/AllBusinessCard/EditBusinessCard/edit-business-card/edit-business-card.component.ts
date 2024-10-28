import { Component, EventEmitter, Input, OnChanges, Output, SimpleChanges } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { IConfig } from 'ngx-countries-dropdown';
import { NgxSpinnerService } from 'ngx-spinner';
import { IBusinessCard } from 'src/app/Model/IBusinessCard';
import { IDiallingcode } from 'src/app/Model/IDiallingCode';
import { BCService } from 'src/app/Service/BusinessCardService/bc.service';
import { ImageService } from 'src/app/Service/Image/image.service';
import { ToastService } from 'src/app/Service/Toast/toast.service';

@Component({
  selector: 'app-edit-business-card',
  templateUrl: './edit-business-card.component.html',
  styleUrls: ['./edit-business-card.component.css']
})
export class EditBusinessCardComponent implements OnChanges {
  @Input() ModalEditBusinessCardData!: number|undefined;
  @Input() modal:any; 
  @Output() rowUpdate = new EventEmitter<number>();

  BusinessCardData:IBusinessCard | undefined;
  BusinessCardEditForm: FormGroup | undefined;
  BusinessCard: IBusinessCard | undefined;
  base64Image: any;
currentYear: number;
  minDate: string;
  maxDate: string;

  constructor(private imageservice: ImageService,private fb: FormBuilder,private bcservice:BCService,private spinner: NgxSpinnerService,private toastservice:ToastService){
    this.currentYear = new Date().getFullYear();
    this.minDate = '1960-01-01';
    this.maxDate = `${this.currentYear - 1}-12-31`;
  }
  
  ngOnChanges(changes: SimpleChanges) {
    if (changes['ModalEditBusinessCardData']) {
      console.log('Updated Business Card Data:', this.ModalEditBusinessCardData);
      if(this.ModalEditBusinessCardData)
      this.GetBusinessCardById(this.ModalEditBusinessCardData)
    }
    else{
      if(this.ModalEditBusinessCardData)
      this.GetBusinessCardById(this.ModalEditBusinessCardData)
      console.log('Updated Business Card Data2:', this.ModalEditBusinessCardData);


    }

  }
  GetBusinessCardById(id:number){
    this.spinner.show(); 
    this.bcservice.GetBusinessCardById(id).subscribe(
      (response) => {
        if (response.status == 200) {
          this.BusinessCardData=response.body??undefined;
          this.initializeBusinessFormEdit()
         } else {
          this.spinner.hide();
        }
      },
      (error) => {

        console.error('Error Get Business Card:', error);
        
      }
    );
  }

  initializeBusinessFormEdit() {
    const currentYear = new Date().getFullYear();
    this.BusinessCardEditForm = this.fb.group({
      id:[this.BusinessCardData?.id],
      name: [this.BusinessCardData?.name, [
        Validators.required,
        Validators.pattern(/^[A-Za-z\s]+$/) 
      ]],
      gender: [this.BusinessCardData?.gender, Validators.required],
      dob: [this.BusinessCardData?.dob ?  new Date(new Date(this.BusinessCardData.dob).setDate(new Date(this.BusinessCardData.dob).getDate() +1)).toISOString().split('T')[0]  : '', [
        Validators.required,
        Validators.min(new Date(1960, 0, 1).getTime()), 
        Validators.max(new Date(currentYear - 1, 11, 31).getTime()) 
      ]],
      email: [this.BusinessCardData?.email, [Validators.required, Validators.email]],
      phone: [this.BusinessCardData?.phone, [
        Validators.required,
        Validators.minLength(10),
        Validators.pattern(/^\d+$/) 
      ]],
      photo: [this.BusinessCardData?.photo?this.BusinessCardData?.photo:null],
      address: [this.BusinessCardData?.address, Validators.required]
    });
    this.base64Image=this.BusinessCardData?.photo?this.BusinessCardData?.photo:''
  }

  onSubmit() {
    this.BusinessCardEditForm?.markAllAsTouched();
     this.spinner.show();
    if (this.BusinessCardEditForm?.valid) {
      console.log(this.BusinessCardEditForm.value);
      this.bcservice.UpdateBusinessCard(this.BusinessCardEditForm.value).subscribe((res:any)=>{
        if(res.status === 200){
          this.toastservice.showToast({
            severity: "success", 
            summary: 'Business Card Updated',
            detail: 'The business card was successfully updated.',
            life: 5000 
          });
          this.spinner.hide();
          this.modal.hide();
          this.BusinessCardEditForm?.reset();
          this.updateTable();
        } else {
          this.toastservice.showToast({
            severity: "error", 
            summary: 'Update Failed',
            detail: 'An error occurred while updating the business card. Please try again.',
            life: 5000 
          });
          this.spinner.hide();
        }
        }, error => {
          this.toastservice.showToast({
            severity: "error",
            summary: 'Update Failed',
            detail: 'An error occurred while updating the business card. Please try again.',
            life: 5000 
          });
          this.spinner.hide();
          console.log(error); 
        });
      
    } else {
      this.spinner.hide();
      this.toastservice.showToast({severity:"warn",summary: 'Required Fields Missing',detail: 'Please fill in all required fields or check the input format before submitting.',life: 5000});
    }
  }
  

  isInvalid(controlName: string): boolean {
    const control = this.BusinessCardEditForm?.get(controlName);
    return control ? control.invalid && control.touched : false;
  }
  async handleFile(file: File) {
    if (!file.type.startsWith('image/')) {
      this.toastservice.showToast({  severity: 'error',
        summary: 'Invalid File Type',
        detail: 'Please upload an image file (JPG, PNG, etc.).',
        life: 5000});
     
      return;
    }

    const maxSizeInBytes = 1 * 1024 * 1024;
    if (file.size > maxSizeInBytes) {
      this.toastservice.showToast({severity: 'warn',
        summary: 'File Too Large',
        detail: 'Please upload an image smaller than 1MB.',
        life: 5000});
      return;
    }

    try {
      this.base64Image = await this.imageservice.convertToBase64(file);
      this.BusinessCardEditForm?.get('photo')?.setValue(this.base64Image);
    } catch (error) {
      this.toastservice.showToast({severity: 'warn',
        summary: 'Upload Photo Missing',
        detail: 'Please upload another photo.',
        life: 5000});
    }
  }

  async onFileChange(event: Event) {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files[0]) {
      await this.handleFile(input.files[0]);
    }
  }

  async onDrop(event: DragEvent) {
    event.preventDefault();
    const files = event.dataTransfer?.files;
    if (files && files[0]) {
      await this.handleFile(files[0]);
    }
  }

  onDragOver(event: DragEvent) {
    event.preventDefault();
    event.stopPropagation();
  }

  onDragLeave(event: DragEvent) {
    event.preventDefault();
    event.stopPropagation();
  }

  clearImage() {
    this.base64Image = null;
    if (this.BusinessCardData?.photo) {
      this.BusinessCardData.photo = "";
      this.BusinessCardEditForm?.get('photo')?.setValue(null);    }
    const fileInput = document.getElementById('photo') as HTMLInputElement;
    if (fileInput) {
      fileInput.value = '';
    }
  }

  onClear() {
    this.BusinessCardEditForm?.reset();
    this.clearImage();
  }
  updateTable(){
    this.rowUpdate.emit();
}
}
