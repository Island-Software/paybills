import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Bill, NewBillDto } from '../models/bill';
import { PaginatedResult } from '../models/pagination';
import { UsersService } from './users.service';

@Injectable({
  providedIn: 'root'
})
export class BillsService {
  baseUrl = environment.apiUrl;
  paginatedResult: PaginatedResult<Bill[]> = new PaginatedResult<Bill[]>();

  constructor(private http: HttpClient, private usersService: UsersService) { }

  getBills(username: string, page?: number, itemsPerPage?: number) {    
    let params = new HttpParams();

    if (page !== null && itemsPerPage !== null) {
      params = params.append('pageNumber', page!.toString());
      params = params.append('pageSize', itemsPerPage!.toString());
    }

    return this.http.get<Bill[]>(this.baseUrl + '/bill/name/' + username, {observe: 'response', params}).pipe(
      map(response => {
        this.paginatedResult.result = response.body!;
        if (response.headers.get('Pagination') !== null) {
          this.paginatedResult.pagination = JSON.parse(response.headers.get('Pagination')!);
        }
        return this.paginatedResult;
      })
    );    
  }

  addBill(bill: NewBillDto) {
    bill.userId = this.usersService.getCurrentUserId();
    console.log("Bill: " + bill);
    return this.http.post(this.baseUrl + '/bill/create', bill);
  }

  deleteBill(bill: Bill) {
    return this.http.delete(this.baseUrl + '/bill/' + bill.id);
  }  
}
