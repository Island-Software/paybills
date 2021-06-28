import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { Bill } from '../models/bill';
import { PaginatedResult } from '../models/pagination';

@Injectable({
  providedIn: 'root'
})
export class BillsService {
  baseUrl = 'http://localhost:5000/api/';
  // bills: any;
  paginatedResult: PaginatedResult<Bill[]> = new PaginatedResult<Bill[]>();

  constructor(private http: HttpClient) { }

  getBills(username: string, page?: number, itemsPerPage?: number) {    
    let params = new HttpParams();

    if (page !== null && itemsPerPage !== null) {
      params = params.append('pageNumber', page!.toString());
      params = params.append('pageSize', itemsPerPage!.toString());
    }

    return this.http.get<Bill[]>(this.baseUrl + 'bill/name/' + username, {observe: 'response', params}).pipe(
      map(response => {
        this.paginatedResult.result = response.body!;
        if (response.headers.get('Pagination') !== null) {
          this.paginatedResult.pagination = JSON.parse(response.headers.get('Pagination')!);
        }
        return this.paginatedResult;
      })
    );    
  }
}
