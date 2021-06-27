import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class BillsService {
  baseUrl = 'http://localhost:5000/api/';
  bills: any;

  constructor(private http: HttpClient) { }

  getBills() : any {
    this.http.get(this.baseUrl + 'bills').subscribe(b => this.bills = b);
    return this.bills;
  }
}
