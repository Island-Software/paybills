import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BillType } from '../models/bill-type';
import { BillTypesService } from '../services/bill-types.service';

@Component({
  selector: 'app-bill-type',
  templateUrl: './bill-type.component.html',
  styleUrls: ['./bill-type.component.css']
})
export class BillTypeComponent implements OnInit {
  @Input() billType?: BillType;
  submitted = false;
  // billType!: BillType;

  constructor(
    // private route: ActivatedRoute,
    private billTypeService: BillTypesService) { }

  ngOnInit(): void {
    // this.getBillType();
  }

  getBillType(): void {
    // const id = Number(this.route.snapshot.paramMap.get('id'));
    // this.billTypeService.getBillType(id)
    //   .subscribe(bt => this.billType = bt);
  }

  save() {
    if (this.billType) {
      this.billTypeService.updateBillType(this.billType)
        .subscribe();
    }
  }
}
