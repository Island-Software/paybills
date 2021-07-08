import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { environment } from 'src/environments/environment';
import { BillType } from '../models/bill-type';

@Injectable({
  providedIn: 'root'
})
export class BillTypesService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getBillType(id: number): Observable<BillType> {
    return this.http.get<BillType>(this.baseUrl + '/billtype/' + id);
  }

  getBillTypes(): Observable<BillType[]> {
    return this.http.get<BillType[]>(this.baseUrl + '/billtype');
  }

  updateBillType(billType: BillType) {
    return this.http.put(this.baseUrl + '/billtype/' + billType.id, billType);
  }
}
