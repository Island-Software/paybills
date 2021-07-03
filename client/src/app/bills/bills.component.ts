import { Component, OnInit } from '@angular/core';
import { Bill } from '../models/bill';
import { Pagination } from '../models/pagination';
import { User } from '../models/user';
import { BillsService } from '../services/bills.service';
import { UsersService } from '../services/users.service';

@Component({
  selector: 'app-bills',
  templateUrl: './bills.component.html',
  styleUrls: ['./bills.component.css']
})
export class BillsComponent implements OnInit {
  bills: Bill[] = [];
  pagination: Pagination | undefined;
  pageNumber = 1;
  pageSize = 5;
  username: string = '';
  inserting = false;

  constructor(private billsService: BillsService) { }

  ngOnInit(): void {   
    this.loadUser(); 
  }

  validatingForm: FormGroup;

  ngOnInit() {
    this.validatingForm = new FormGroup({
      loginFormModalEmail: new FormControl('', Validators.email),
      loginFormModalPassword: new FormControl('', Validators.required)
    });
  }

  get loginFormModalEmail() {
    return this.validatingForm.get('loginFormModalEmail');
  }

  get loginFormModalPassword() {
    return this.validatingForm.get('loginFormModalPassword');
  }

  loadUser() {
    this.username = JSON.parse(localStorage.getItem('user')!).username;
    this.billsService.getBills(this.username, this.pageNumber, this.pageSize).subscribe(bills => {
      this.bills = bills.result;
      this.pagination = bills.pagination;
    })
  }

  pageChanged(event: any) {
    this.pageNumber = event.page;
    this.loadUser();
  }

  add(bill: Bill) {
    this.inserting = true;
    // this.billsService.addBill(bill).subscribe();
  }

  delete(bill: Bill) {
    console.log("Deleting " + bill.id);
  }
}
