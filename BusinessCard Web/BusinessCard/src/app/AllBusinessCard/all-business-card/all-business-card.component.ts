import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import 'datatables.net';
import { BCService } from 'src/app/Service/BusinessCardService/bc.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { DateFormatPipe } from 'src/app/pipe/date-format.pipe';
import { ToastService } from 'src/app/Service/Toast/toast.service';
@Component({
  selector: 'app-all-business-card',
  templateUrl: './all-business-card.component.html',
  styleUrls: ['./all-business-card.component.css'],
  encapsulation: ViewEncapsulation.None
})
export class AllBusinessCardComponent implements OnInit {
  dataBusinessCard: any[] = [];
  totalRecords: number = 0;
  rowsPerPage: number = 5; 
  first: number = 0; 
  searchText: string = '';
  selectedYear: any;
  selectedMonth: any;
  selectedDay: any;
  genderFilter: string = '';
  emailFilter: string = '';
  phoneFilter: string = '';
  addressFilter: string = '';
  years: number[] = this.generateYears();
  months: { value: number, name: string }[] = this.getMonths();
  days: number[] = [];
   filters ={ 
    name:'',
    email: '',
    phone: '',
    address: '',
    gender: '',
    year: '',
    month:'',
    day: ''};
    showClearFiltter:boolean=false;
    defaultPhoto = '../../../assets/image/default-avatar-icon-of-social-media-user-vector.jpg';
    _modaldelete:any;
    _modalexport:any;
    _modaledit:any;
    deletedatabusinesscard:number|undefined;
    exportbusinesscard:number|undefined;
    editbusinesscard:number|undefined;
    dateFormatPipe: DateFormatPipe;

  constructor(private bcservice: BCService, private spinner:NgxSpinnerService,private toastservice:ToastService) {this.dateFormatPipe = new DateFormatPipe()}

  ngOnInit() {
    this.loadData({ first: 0, rows: this.rowsPerPage });
  }
  

  deleteRowTable() {
    this.loadData({ first: 0, rows: this.rowsPerPage });
  }
  UpdateRowTable(){
    this.loadData({ first: 0, rows: this.rowsPerPage });

  }

  loadData(event: any) {
    this.spinner.show();
    const page = (event.first / event.rows) + 1;
    this.bcservice.GetAllBusinessCards(
      page,
      event.rows,this.filters.name,this.filters.email,this.filters.phone,this.filters.address,this.filters.gender,this.filters.year,this.filters.month,this.filters.day
    ).subscribe((resp: any) => {
      if(resp.status==200){
        this.dataBusinessCard = resp.body.data;
        this.totalRecords = resp.body.recordsTotal;
        setTimeout(()=>{
          this.spinner.hide();

        },1000)      }
      else{
        setTimeout(()=>{
          this.spinner.hide();

        },1000)
      }
    },error=>{
console.log(error);
setTimeout(()=>{
  this.spinner.hide();

},1000)    
});
  }
  
  isFirstPage(): boolean {
    return this.first === 0;
  }

  isLastPage(): boolean {
    return this.first >= (this.totalRecords - this.rowsPerPage);
  }

  prev() {
    if (!this.isFirstPage()) {
      this.first = this.first - this.rowsPerPage;
      this.loadData({ first: this.first, rows: this.rowsPerPage });
    }
  }

  reset() {
    this.first = 0;
    this.loadData({ first: this.first, rows: this.rowsPerPage });
  }

  next() {
    if (!this.isLastPage()) {
      this.first = this.first + this.rowsPerPage;
      this.loadData({ first: this.first, rows: this.rowsPerPage });
    }
  }

  editItem(id: number) {
    console.log(id);
    this.editbusinesscard=id
     const modalElement = document.getElementById('EditBusinessCard');
     if (modalElement) {
       this._modaledit= new window.bootstrap.Modal(modalElement);
       this.spinner.show();
       setTimeout(() => {
         this.spinner.hide();
         this._modaledit.show();
       }, 1000);
     }
    console.log("Editing item:", id);
  }

  deleteItem(id: any) {
    console.log(id);
    this.deletedatabusinesscard=id
     const modalElement = document.getElementById('DeleteBusinessCard');
     if (modalElement) {
       this._modaldelete = new window.bootstrap.Modal(modalElement);
       this.spinner.show();
       setTimeout(() => {
         this.spinner.hide();
         this._modaldelete.show();
       }, 1000);
     }
     console.log("Preparing to delete item with ID:", id);  }

  exportItem(id: any) {
    console.log(id);
    this.exportbusinesscard=id
        const modalElement = document.getElementById('fileExportModal');
        if (modalElement) {
          this._modalexport = new window.bootstrap.Modal(modalElement);
          this.spinner.show();
          setTimeout(() => {
            this.spinner.hide();
            this._modalexport.show();
          }, 1000);
        }
        console.log("Preparing to export item with ID:", id);  }
  filterTable() {
  this.showClearFiltter=true;
     this.filters = {
      name: this.searchText,
      email: this.emailFilter,
      phone: this.phoneFilter,
      address: this.addressFilter,
      gender: this.genderFilter,
      year: this.selectedYear!=undefined?this.selectedYear:'',
      month: this.selectedMonth!=undefined?this.selectedMonth:'',
      day: this.selectedDay!=undefined?this.selectedDay:''
    };
 
    this.loadData({ first: 0, rows: this.rowsPerPage });
  
  }
  clearFilter(){
   
  
      this.filters = {
       name: '',
       email: '',
       phone: '',
       address: '',
       gender: '',
       year: '',
       month: '',
       day: ''
     };
     this.searchText='';
     this.addressFilter='';
     this.selectedYear='';
     this.selectedMonth='';
     this.selectedDay='';
     this.emailFilter='';
     this.genderFilter='';
     this.addressFilter='';
     this.phoneFilter='';
  
     this.loadData({ first: 0, rows: this.rowsPerPage });
   this.showClearFiltter=false;
   
  }
 
  onYearChange() {
    if (this.selectedYear === 'clear') {
      this.selectedYear = null;
      this.selectedMonth = null;
      this.selectedDay = null;
    } else {
      this.selectedMonth = null;
      this.selectedDay = null;
      this.days = [];
    }
  }

  onMonthChange() {
    if (this.selectedMonth === 'clear') {
      this.selectedMonth = null;
      this.selectedDay = null;
    } else {
      this.days = this.getDaysInMonth(this.selectedMonth, this.selectedYear);
      this.selectedDay = null;
    }
  }

  generateYears(): number[] {
    const years: number[] = [];
    const currentYear = new Date().getFullYear()-1;
    for (let year = 1960; year <= currentYear; year++) {
      years.push(year);
    }
    return years;
  }

  getMonths(): { value: number, name: string }[] {
    return [
      { value: 1, name: 'January' },
      { value: 2, name: 'February' },
      { value: 3, name: 'March' },
      { value: 4, name: 'April' },
      { value: 5, name: 'May' },
      { value: 6, name: 'June' },
      { value: 7, name: 'July' },
      { value: 8, name: 'August' },
      { value: 9, name: 'September' },
      { value: 10, name: 'October' },
      { value: 11, name: 'November' },
      { value: 12, name: 'December' },
    ];
  }

  getDaysInMonth(month: number, year: number): number[] {
    const daysInMonth = new Date(year, month, 0).getDate();
    return Array.from({ length: daysInMonth }, (_, i) => i + 1);
  }
}
