import { Component, OnInit } from '@angular/core';
import { IConfig } from 'ngx-countries-dropdown/lib/models';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { IBusinessCard } from 'src/app/Model/IBusinessCard';
import { IDiallingcode } from 'src/app/Model/IDiallingCode';
import { ImageService } from 'src/app/Service/Image/image.service';
import { MessageService } from 'primeng/api';
import { NgxSpinnerService } from 'ngx-spinner';
import 'bootstrap';
declare var $: any;
@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.css'],
  providers: [MessageService]
})
export class FormComponent implements OnInit {
  BusinessCardForm: FormGroup | undefined;
  BusinessCard: IBusinessCard | undefined;
  DiallingCode: IDiallingcode | undefined;
  base64Image: any;
  selectedCountryConfig: IConfig = {
    hideCode: true,
    hideName: true
  };
  countryListConfig: IConfig = {
    hideCode: true,
  };
   _modal:any;

  constructor(
    private fb: FormBuilder,
    private imageservice: ImageService,
    private messageService: MessageService,
    private spinner: NgxSpinnerService
  ) { }

  ngOnInit(): void {
    this.initializeBusinessForm();
  }

  initializeBusinessForm() {
    this.BusinessCardForm = this.fb.group({
      name: ['', [Validators.required]],
      gender: ['', Validators.required],
      dob: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      phone: ['', [Validators.required, Validators.minLength(10)]],
      photo: [null],
      address: ['', Validators.required]
    });
  }

  onCountryChange(country: any) {
    this.DiallingCode = country.dialling_code;
  }

  onSubmit() {
    this.BusinessCardForm?.markAllAsTouched();

    if (this.BusinessCardForm?.valid) {
      this.prepareBusinessCardData();

      const modalElement = document.getElementById('BusinessCard');
      if (modalElement) {
        this._modal = new window.bootstrap.Modal(modalElement);
        this.spinner.show();
        setTimeout(() => {
          this.spinner.hide();
          this._modal.show();
        }, 1000);
      }
    } else {
      this.messageService.add({
        severity: 'warn',
        summary: 'Required Fields Missing',
        detail: 'Please fill in all required fields or check the input format before submitting.',
        life: 5000
      });
    }
  }

  prepareBusinessCardData() {
    this.BusinessCard = this.BusinessCardForm?.value as IBusinessCard;
    const phoneValue = this.BusinessCardForm?.get('phone')?.value.toString();
    this.BusinessCard.phone = phoneValue.startsWith('0')
      ? this.DiallingCode + phoneValue.slice(1)
      : this.DiallingCode + phoneValue;
  }

  isInvalid(controlName: string): boolean {
    const control = this.BusinessCardForm?.get(controlName);
    return control ? control.invalid && control.touched : false;
  }

  async handleFile(file: File) {
    if (!file.type.startsWith('image/')) {
      this.messageService.add({
        severity: 'error',
        summary: 'Invalid File Type',
        detail: 'Please upload an image file (JPG, PNG, etc.).',
        life: 5000
      });
      return;
    }

    const maxSizeInBytes = 1 * 1024 * 1024;
    if (file.size > maxSizeInBytes) {
      this.messageService.add({
        severity: 'warn',
        summary: 'File Too Large',
        detail: 'Please upload an image smaller than 1MB.',
        life: 5000
      });
      return;
    }

    try {
      this.base64Image = await this.imageservice.convertToBase64(file);
      this.BusinessCardForm?.get('photo')?.setValue(this.base64Image);
    } catch (error) {
      this.messageService.add({
        severity: 'warn',
        summary: 'Upload Photo Missing',
        detail: 'Please upload another photo.',
        life: 5000
      });
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
    if (this.BusinessCard?.photo) {
      this.BusinessCard.photo = "";
    }
    const fileInput = document.getElementById('photo') as HTMLInputElement;
    if (fileInput) {
      fileInput.value = '';
    }
  }

  onClear() {
    this.BusinessCardForm?.reset();
    this.clearImage();
  }
}
