import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ordersCompositeDto } from '../dtos/business/ordersCompositeDto';
import { clientDto } from '../dtos/supertienda/clientDto';
import { productDto } from '../dtos/supertienda/productDto';

@Injectable({
  providedIn: 'root'
})
export class OrderService {
  private apiBusinessOrderUrl = 'https://localhost:5001/api/business/orders';
  private apiSupertiendaUrl = 'https://localhost:5001/api/supertienda';


  constructor(private http: HttpClient) { }

  createOrder(orderData: ordersCompositeDto): Observable<any> {
    return this.http.post(this.apiBusinessOrderUrl, orderData);
  }

  getClients(): Observable<clientDto[]> {
    return this.http.get<clientDto[]>(`${this.apiSupertiendaUrl}/clients`);
  }

  getProducts(): Observable<productDto[]> {
    return this.http.get<productDto[]>(`${this.apiSupertiendaUrl}/products`);
  }

}
