import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './Home/home/home.component';
import { CreateBusinesscardComponent } from './CreateBusinessCard/create-businesscard/create-businesscard.component';
import { AllBusinessCardComponent } from './AllBusinessCard/all-business-card/all-business-card.component';

const routes: Routes = [
  {path:'',component:HomeComponent},
  {path:'CreateBusinessCard',component:CreateBusinesscardComponent},
  {path:'ViewAllBusinessCards',component:AllBusinessCardComponent}
  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
