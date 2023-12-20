//orders.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { orderDto } from 'src/dtos/supertienda/orderDto';

@Injectable({ providedIn: 'root' })
export class OrderService {
    private baseUrl = 'https://localhost:5001/api/supertienda/orders';
    
    constructor(private http: HttpClient) { }
    
    getOrders(): Observable<orderDto[]> {
        return this.http.get<orderDto[]>(this.baseUrl);
    }
}