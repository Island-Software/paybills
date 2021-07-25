import { Component, OnInit, TemplateRef } from '@angular/core';
import { Bill, NewBillDto } from '../../models/bill';
import { Pagination } from '../../models/pagination';
import { BillsService } from '../../services/bills.service';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { MONTHS } from 'src/app/consts/months';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-bill-list',
  templateUrl: './bill-list.component.html',
  styleUrls: ['./bill-list.component.css']
})
export class BillListComponent implements OnInit {
  bills: Bill[] = [];
  selectedBill?: Bill;
  pagination: Pagination | undefined;
  pageNumber = 1;
  pageSize = 5;
  username: string = '';
  modalRef!: BsModalRef;
  months = MONTHS;
  selectedMonth: number;
  selectedYear: number;
  loading: boolean;

  constructor(private billsService: BillsService, private modalService: BsModalService,
      private toastrServie: ToastrService) {
    this.selectedMonth = new Date().getMonth() + 1;
    this.selectedYear = new Date().getFullYear();
    this.loading = false;
  }

  ngOnInit(): void {
    this.loadUser();    
  }  

  pageChanged(event: any) {
    this.pageNumber = event.page;
    this.loadUser();
  }

  openModalForEdit(template: TemplateRef<any>, billToEdit: Bill)
  {
    this.selectedBill = billToEdit;    
    this.modalRef = this.modalService.show(template);
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
    this.loading = true;
    this.username = JSON.parse(localStorage.getItem('user')!).username;    
    this.billsService.getBills(this.username, this.selectedMonth, this.selectedYear, this.pageNumber, this.pageSize).subscribe(bills => {
      this.bills = bills.result;
      this.pagination = bills.pagination;
      this.loading = false;
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

  copyBills() {
    this.billsService.copyBills(this.selectedMonth, this.selectedYear).subscribe( _ => {
      this.toastrServie.success("Bills copied successfuly");
    });
  }
}
