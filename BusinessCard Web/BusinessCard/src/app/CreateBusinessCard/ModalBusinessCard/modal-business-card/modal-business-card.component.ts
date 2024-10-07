import { Component, Input, OnInit } from '@angular/core';
import * as $ from 'jquery';
import { IBusinessCard } from 'src/app/Model/IBusinessCard';
@Component({
  selector: 'app-modal-business-card',
  templateUrl: './modal-business-card.component.html',
  styleUrls: ['./modal-business-card.component.css']
})
export class ModalBusinessCardComponent{
 @Input() ModalBusinessCardData!: IBusinessCard|undefined;
 

}
