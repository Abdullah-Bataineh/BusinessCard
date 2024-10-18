import { HttpResponse } from '@angular/common/http';
import { Component, Input } from '@angular/core';
import { typeFile } from 'src/app/Enum/TypeFile';
import { IBusinessCard } from 'src/app/Model/IBusinessCard';
import { BCService } from 'src/app/Service/BusinessCardService/bc.service';

@Component({
  selector: 'app-export-business-card',
  templateUrl: './export-business-card.component.html',
  styleUrls: ['./export-business-card.component.css']
})
export class ExportBusinessCardComponent {
  @Input() ModalBusinessCardData!: IBusinessCard|undefined;
  @Input() modal:any;
  constructor(private bcservice:BCService){

  }
  exportToCSV() {
    this.modal.hide();
  
    if (this.ModalBusinessCardData) {
      this.bcservice.ExportBusinessCard(this.ModalBusinessCardData, typeFile.CSV).subscribe(
        (res) => {
          this.downloadFile(res.body, 'business_card.csv'); // Specify your filename here
        },
        error => {
          console.error('Error exporting business card:', error);
        }
      );
    }
  }
  
  private downloadFile(data: ArrayBuffer | null, fileName: string) {
    if (data) {
      const blob = new Blob([data]);
      const url = window.URL.createObjectURL(blob);
      const a = document.createElement('a');
      a.href = url;
      a.download = fileName;
      document.body.appendChild(a);
      a.click();
      document.body.removeChild(a);
      window.URL.revokeObjectURL(url);
    } else {
      console.error('Data is null or undefined');
    }
  }
  exportToXML(){
this.modal.hide();
    console.log(typeFile.XML);
    console.log(this.ModalBusinessCardData);
  }

 


}
