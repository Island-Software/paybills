import { Component, OnInit } from '@angular/core';
import { BillType } from '../models/bill-type';
import { BillTypesService } from '../services/bill-types.service';

@Component({
  selector: 'app-bill-type-list',
  templateUrl: './bill-type-list.component.html',
  styleUrls: ['./bill-type-list.component.css']
})
export class BillTypeListComponent implements OnInit {
  billTypes: BillType[] = [];
  selectedBillType?: BillType;
  
  constructor(private billTypeService: BillTypesService) { }

  onSelect(billType: BillType): void {
    this.selectedBillType = billType;
  }

  ngOnInit(): void {
    this.getBillTypes();
  }

  getBillTypes() {
    this.billTypeService.getBillTypes()
      .subscribe(bts => this.billTypes = bts);
  }
}
