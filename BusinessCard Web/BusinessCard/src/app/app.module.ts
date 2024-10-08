import { NgModule } from '@angular/core';
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



@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    HeaderComponent,
    FooterComponent,
    FormComponent,
    ModalBusinessCardComponent,
    CreateBusinesscardComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    NgxCountriesDropdownModule,
    ReactiveFormsModule,
    ToastModule,
    BrowserAnimationsModule,
    NgxSpinnerModule
  ],
  providers: [MessageService],
  bootstrap: [AppComponent]
})
export class AppModule { }
