import { Component, OnInit, TemplateRef } from '@angular/core';
import { Bill, NewBillDto } from '../../models/bill';
import { Pagination } from '../../models/pagination';
import { BillsService } from '../../services/bills.service';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { MONTHS } from 'src/app/consts/months';

@Component({
  selector: 'app-bill-list',
  templateUrl: './bill-list.component.html',
  styleUrls: ['./bill-list.component.css']
})
export class BillListComponent implements OnInit {
  bills: Bill[] = [];  
  pagination: Pagination | undefined;
  pageNumber = 1;
  pageSize = 5;
  username: string = '';
  modalRef!: BsModalRef;
  newBill: NewBillDto = { value: 0, month: 0, year: 0, typeId: 0, userId: 0 };
  months = MONTHS;
  selectedMonth: number;
  selectedYear: number;

  constructor(private billsService: BillsService, private modalService: BsModalService) {
    this.selectedMonth = new Date().getMonth() + 1;
    this.selectedYear = new Date().getFullYear();
  }

  ngOnInit(): void {
    this.loadUser();    
  }  

  pageChanged(event: any) {
    this.pageNumber = event.page;
    this.loadUser();
  }

  openModal(template: TemplateRef<any>) {    
    this.modalRef = this.modalService.show(template);
  }

  closeModal(value: boolean)
  {    
    this.modalRef.hide();
    if (value)
      this.loadUser();
  }

  loadUser() {
    this.username = JSON.parse(localStorage.getItem('user')!).username;
    this.billsService.getBills(this.username, this.selectedMonth, this.selectedYear, this.pageNumber, this.pageSize).subscribe(bills => {
      this.bills = bills.result;
      this.pagination = bills.pagination;
    })
  }

  delete(bill: Bill) {
    this.billsService.deleteBill(bill).subscribe(_ => this.loadUser());
  }

  onFilterMonth() {    
    this.loadUser();
  }

  onFilterYear() {
    if (this.selectedYear.toString().length === 4)
      this.loadUser();
  }
}
