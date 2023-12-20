import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { orderDetailDto } from 'src/dtos/supertienda/orderDetailDto';


@Injectable({ providedIn: 'root' })
export class OrderDetailsService {
  private baseUrl = 'https://localhost:5001/api/supertienda/orderDetails';

  constructor(private http: HttpClient) { }

  getOrderDetails(): Observable<orderDetailDto[]> {
    return this.http.get<orderDetailDto[]>(this.baseUrl);
  }
}


