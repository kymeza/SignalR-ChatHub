//products.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { productDto } from 'src/dtos/supertienda/productDto';

@Injectable({ providedIn: 'root' })
export class ProductService {
    private baseUrl = 'https://localhost:5001/api/supertienda/products';
    
    constructor(private http: HttpClient) { }
    
    getProducts(): Observable<productDto[]> {
        return this.http.get<productDto[]>(this.baseUrl);
    }
}
