import { Component, OnInit } from '@angular/core';
import * as $ from 'jquery';
import 'datatables.net';
import { BCService } from 'src/app/Service/BusinessCardService/bc.service';
import { NgxSpinnerService } from 'ngx-spinner';
@Component({
  selector: 'app-all-business-card',
  templateUrl: './all-business-card.component.html',
  styleUrls: ['./all-business-card.component.css']
})
export class AllBusinessCardComponent implements OnInit {
  dataTable: any;
  searchText: string = '';
  selectedYear: any;
  selectedMonth: any;
  selectedDay: any;
  genderFilter: string = '';
  emailFilter: string = '';
  phoneFilter: string = '';
  addressFilter: string = '';
  dataBusinessCard:any=[];
  years: number[] = this.generateYears();
  months: { value: number, name: string }[] = this.getMonths();
  days: number[] = [];
constructor(private bcservice:BCService,private spinner: NgxSpinnerService){}
  ngOnInit() {
    this.spinner.show();
this.getBusinessCard();


  }
  getBusinessCard(){
    this.bcservice.GetAllBusinessCards().subscribe((res)=>{
      if(res.status==200){
       if(res.body){
        this.dataBusinessCard=res.body;     
        this.initializeDataTable();
        setTimeout(()=>{
        this.spinner.hide();

        },1000)
       }
      }
    })
  }

  initializeDataTable() {    
    this.dataTable = $('#example').DataTable({
      data: this.dataBusinessCard,
      columns: [
        { data: 'name' },
        { data: 'dob' },
        { data: 'gender' },
        { data: 'email' },
        { data: 'phone' },
        { data: 'address' },
        {data: null,orderable: false,render: (row) => {return `<div class="btn-group" role="group"><button class="btn btn-danger delete-btn me-2" data-id="${row.id}">Delete</button><button class="btn btn-primary export-btn" data-id="${row.id}">Export</button></div>`;}}
      ]
    });

    $('#example tbody').on('click', '.delete-btn', (event) => {
      const itemId = $(event.currentTarget).data('id'); 
      console.log(itemId);
      const item = this.dataBusinessCard.find((d: { id: any; }) => d.id === itemId); 
      if (item) {
        this.deleteItem(item); 
      } else {
        console.log("Item not found for ID:", itemId); 
      }
    });
    $('#example tbody').on('click', '.export-btn', (event) => {
      const itemId = $(event.currentTarget).data('id'); 
      const item = this.dataBusinessCard.find((d: { id: any; }) => d.id === itemId); 
      if (item) {
        this.exportItem(item); 
      } else {
        console.log("Item not found for ID:", itemId);
      }
    });
  }


  deleteItem(item: any) {
    console.log("Deleting item:", item); 
    
  }
  
  exportItem(item: any) {
    console.log("Exporting item:", item);
    
   
  }

  filterTable() {
    this.dataTable
      .columns(0).search(this.searchText)
      .columns(3).search(this.emailFilter)
      .columns(4).search(this.phoneFilter)
      .columns(5).search(this.addressFilter)
      .draw();

    if (this.genderFilter) {
      this.dataTable
        .columns(2).search('^' + this.genderFilter + '$', true, false)
        .draw();
    } else {
      this.dataTable
        .columns(2).search('')
        .draw();
    }

    let dobFilter = '';

    if (this.selectedYear && this.selectedYear !== 'clear') {
      dobFilter += this.selectedYear;
    }

    if (this.selectedMonth && this.selectedMonth !== 'clear') {
      dobFilter += '-' + (this.selectedMonth < 10 ? '0' : '') + this.selectedMonth;
    }

    if (this.selectedDay) {
      dobFilter += '-' + (this.selectedDay < 10 ? '0' : '') + this.selectedDay;
    }

    this.dataTable
      .columns(1).search(dobFilter).draw();
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
    this.filterTable();
  }

  onMonthChange() {
    if (this.selectedMonth === 'clear') {
      this.selectedMonth = null;
      this.selectedDay = null;
    } else {
      this.days = this.getDaysInMonth(this.selectedMonth, this.selectedYear);
      this.selectedDay = null;
    }
    this.filterTable();
  }

  generateYears(): number[] {
    const years: number[] = [];
    const currentYear = new Date().getFullYear();
    for (let year = 1900; year <= currentYear; year++) {
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
