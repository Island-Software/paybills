import { Component, Input, OnInit } from '@angular/core';
import { BillType } from '../models/bill-type';
import { BillTypesService } from '../services/bill-types.service';
import { Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-bill-type',
  templateUrl: './bill-type.component.html',
  styleUrls: ['./bill-type.component.css']
})
export class BillTypeComponent implements OnInit {
  @Input() billType?: BillType;
  @Output() saveBillTypeEvent = new EventEmitter<boolean>();

  constructor(
    private billTypeService: BillTypesService) { }

  ngOnInit(): void {
  }

  save() {
    if (this.billType) {
      this.billTypeService.updateBillType(this.billType)
        .subscribe(_ => this.saveBillTypeEvent.emit(true));
    }
  }
}
