import { Component, OnInit } from '@angular/core';
import { BillType } from '../models/bill-type';
import { BillTypesService } from '../services/bill-types.service';

@Component({
  selector: 'app-bill-type-list',
  templateUrl: './bill-type-list.component.html',
  styleUrls: ['./bill-type-list.component.css']
})
export class BillTypeListComponent implements OnInit {
  originalBillTypes: BillType[] = [];
  billTypes: BillType[] = [];
  searchText: string = "";
  selectedBillType?: BillType;
  
  constructor(private billTypeService: BillTypesService) { }

  onSelect(billType: BillType): void {
    this.selectedBillType = billType;
  }

  ngOnInit(): void {
    this.getBillTypes();
  }

  closeChild(value: boolean) {
    if (value) {
      this.selectedBillType = undefined;
    }
  }

  getBillTypes() {
    this.billTypeService.getBillTypes()
      .subscribe(bts => {
        this.billTypes = bts;
        this.originalBillTypes = bts;
      });
  }

  onFilter() {
    this.billTypes = this.originalBillTypes.filter(
      t => t.description.toUpperCase().match(this.searchText.toUpperCase() + '.*')); // The /i option doesn't work
  }
}
