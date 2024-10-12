import { Component, OnInit } from '@angular/core';

import * as $ from 'jquery';
import 'datatables.net';
@Component({
  selector: 'app-all-business-card',
  templateUrl: './all-business-card.component.html',
  styleUrls: ['./all-business-card.component.css']
})
export class AllBusinessCardComponent implements OnInit {
  tableElement:any;
  ngOnInit(): void {
   this.initializeDataTable();
   this.tableElement = $('#example');
  }
  initializeDataTable() {
    const tableElement = $('#example'); 

    // Setup - add a text input to each footer cell 
    tableElement.find('tfoot tr:eq(0) th').each(function () {
      const title = $(this).text();
      
      if (title === 'Gender') {
        $(this).html('<select class="column_search"><option value="">Search Gender</option><option value="Male">Male</option><option value="Female">Female</option></select>');
      } else if (title === 'DOB') {
        $(this).html(
          '<input type="text" placeholder="Year" class="column_search year_search" />' +
          '<input type="text" placeholder="Month" class="column_search month_search" />' +
          '<input type="text" placeholder="Day" class="column_search day_search" />'
        );
    
        
      } else {
        $(this).html('<input type="text" placeholder="Search ' + title + '" class="column_search" />');
      }
    });
    
    const table = tableElement.DataTable({
      orderCellsTop: true
    });
    
    tableElement.on('keyup', '.column_search', function () {
      table
        .column($(this).parent().index())
        .search(this.value)
        .draw();
    });
    
  }

}
