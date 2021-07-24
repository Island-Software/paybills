import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { MONTHS } from 'src/app/consts/months';
import { Bill } from 'src/app/models/bill';
import { BillType } from 'src/app/models/bill-type';
import { BillsService } from 'src/app/services/bills.service';

@Component({
  selector: 'app-bill-edit',
  templateUrl: './bill-edit.component.html',
  styleUrls: ['./bill-edit.component.css']
})
export class BillEditComponent implements OnInit {
  @Input() bill?: any;
  @Output() saveBillEvent = new EventEmitter<boolean>();

  constructor(private billsService: BillsService) { }

  ngOnInit(): void {
    
  }

  save() {
    if (this.bill)
      this.billsService.updateBill(this.bill)
        .subscribe(_ => this.saveBillEvent.emit(true));
  }

  close() {
    console.log(this.bill.value);
    this.saveBillEvent.emit(false);
  }

  closeAndReloadParent() {
    this.saveBillEvent.emit(true);
  }
}
