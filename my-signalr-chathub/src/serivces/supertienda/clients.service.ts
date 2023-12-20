import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { clientDto } from 'src/dtos/supertienda/clientDto';


@Injectable({ providedIn: 'root' })
export class ClientService {
  private baseUrl = 'https://localhost:5001/api/supertienda/clients';

  constructor(private http: HttpClient) { }

  getClients(): Observable<clientDto[]> {
    return this.http.get<clientDto[]>(this.baseUrl);
  }
}


