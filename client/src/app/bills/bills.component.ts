import { Component, OnInit, TemplateRef } from '@angular/core';
import { Bill, NewBill } from '../models/bill';
import { Pagination } from '../models/pagination';
import { BillsService } from '../services/bills.service';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { BillType } from '../models/bill-type';
import { BillTypesService } from '../services/bill-types.service';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MONTHS } from '../consts/months';

@Component({
  selector: 'app-bills',
  templateUrl: './bills.component.html',
  styleUrls: ['./bills.component.css']
})
export class BillsComponent implements OnInit {
  bills: Bill[] = [];
  billTypes: BillType[] = [];
  pagination: Pagination | undefined;
  pageNumber = 1;
  pageSize = 5;
  username: string = '';
  modalRef!: BsModalRef;
  newBill: NewBill = { value: 0, month: 0, year: 0, typeId: 0 };
  // Reactive forms
  newBillForm!: FormGroup;
  months = MONTHS;

  constructor(private billsService: BillsService, private modalService: BsModalService,
    private billTypesService: BillTypesService,
    private toastr: ToastrService) { }

  ngOnInit(): void {
    this.loadUser();
    this.initializeForm();
  }

  initializeForm() {
    this.newBillForm = new FormGroup({
      billTypeId: new FormControl('', Validators.required),
      value: new FormControl('', Validators.required),
      month: new FormControl('', Validators.required),
      year: new FormControl('', Validators.required)
    })
  }

  pageChanged(event: any) {
    this.pageNumber = event.page;
    this.loadUser();
  }

  openModal(template: TemplateRef<any>) {
    this.billTypesService.getBillTypes().subscribe(
      bt => this.billTypes = bt
    );
    this.modalRef = this.modalService.show(template);
  }

  loadUser() {
    this.username = JSON.parse(localStorage.getItem('user')!).username;
    this.billsService.getBills(this.username, this.pageNumber, this.pageSize).subscribe(bills => {
      this.bills = bills.result;
      this.pagination = bills.pagination;
    })
  }

  add() {
    console.log(this.newBillForm.value);
  }
  // add(newBill: NewBill) {
    // this.billTypesService.getBillType(newBill.typeId).subscribe(
    //   gbt => {
    //     let billToAdd: Bill = { id: 10, value: newBill.value, month: 6, year: 2021, billType: gbt}
    //     this.bills.push(billToAdd);
    //     this.modalRef.hide();
    //   }
    // ); 
    // this.billsService.addBill(bill).subscribe();
  // }

  delete(bill: Bill) {
    console.log("Deleting " + bill.id);
  }
}
