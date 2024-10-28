import { CUSTOM_ELEMENTS_SCHEMA, NgModule, NO_ERRORS_SCHEMA } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http'; 
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './Home/home/home.component';
import { HeaderComponent } from './Header/header/header.component';
import { FooterComponent } from './Footer/footer/footer.component';
import { FormComponent } from './CreateBusinessCard/Form/form/form.component';
import { NgxCountriesDropdownModule } from 'ngx-countries-dropdown';
import { ReactiveFormsModule } from '@angular/forms';
import { ToastModule } from 'primeng/toast';
import { MessageService } from 'primeng/api';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations'; 
import { NgxSpinnerModule } from 'ngx-spinner';
import { ModalBusinessCardComponent } from './CreateBusinessCard/ModalBusinessCard/modal-business-card/modal-business-card.component';
import { CreateBusinesscardComponent } from './CreateBusinessCard/create-businesscard/create-businesscard.component';
import { AllBusinessCardComponent } from './AllBusinessCard/all-business-card/all-business-card.component';
import { FormsModule } from '@angular/forms';
import { ImportBusinessCardComponent } from './CreateBusinessCard/ImportBusinessCard/import-business-card/import-business-card.component';
import { DateFormatPipe } from './pipe/date-format.pipe';
import { DeleteBusinessCardComponent } from './AllBusinessCard/DeleteBusinessCard/delete-business-card/delete-business-card.component';
import { ExportBusinessCardComponent } from './AllBusinessCard/ExportBusinessCard/export-business-card/export-business-card.component';
import { ZXingScannerModule } from '@zxing/ngx-scanner';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { EditBusinessCardComponent } from './AllBusinessCard/EditBusinessCard/edit-business-card/edit-business-card.component';



@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    HeaderComponent,
    FooterComponent,
    FormComponent,
    ModalBusinessCardComponent,
    CreateBusinesscardComponent,
    AllBusinessCardComponent,
    ImportBusinessCardComponent,
    DateFormatPipe,
    DeleteBusinessCardComponent,
    ExportBusinessCardComponent,
    EditBusinessCardComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    NgxCountriesDropdownModule,
    ReactiveFormsModule,
    ToastModule,
    BrowserAnimationsModule,
    NgxSpinnerModule,
    FormsModule , 
    ZXingScannerModule,
    TableModule,
    ButtonModule
    
  ],
  providers: [MessageService],
  bootstrap: [AppComponent],
  schemas: [CUSTOM_ELEMENTS_SCHEMA,NO_ERRORS_SCHEMA],
})
export class AppModule { }
