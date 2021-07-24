import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Bill, NewBillDto, UpdateBillDto } from '../models/bill';
import { PaginatedResult } from '../models/pagination';
import { UsersService } from './users.service';

@Injectable({
  providedIn: 'root'
})
export class BillsService {
  baseUrl = environment.apiUrl;
  paginatedResult: PaginatedResult<Bill[]> = new PaginatedResult<Bill[]>();

  constructor(private http: HttpClient, private usersService: UsersService) { }

  getBills(username: string, month: number, year: number, page?: number, itemsPerPage?: number) {    
    let params = new HttpParams();

    if (page !== null && itemsPerPage !== null) {
      params = params.append('pageNumber', page!.toString());
      params = params.append('pageSize', itemsPerPage!.toString());
    }

    return this.http.get<Bill[]>(this.baseUrl + 'bill/name/' + username + '/' + month + '/' + year, {observe: 'response', params}).pipe(
      map(response => {
        this.paginatedResult.result = response.body!;
        if (response.headers.get('Pagination') !== null) {
          this.paginatedResult.pagination = JSON.parse(response.headers.get('Pagination')!);
        }
        return this.paginatedResult;
      })
    );    
  }

  createBill(bill: NewBillDto) {
    bill.userId = this.usersService.getCurrentUserId();    
    return this.http.post(this.baseUrl + 'bill/create', bill);
  }

  updateBill(bill: UpdateBillDto) {
    return this.http.put(this.baseUrl + 'bill/' + bill.id, bill);
  }

  deleteBill(bill: Bill) {
    return this.http.delete(this.baseUrl + 'bill/' + bill.id);
  }  
}
