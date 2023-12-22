import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { productDto } from 'src/dtos/supertienda/productDto';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class PowerShellService {
  private apiUrl = 'https://localhost:5001/api/vulnerable'; // Replace with your actual API URL

  constructor(private http: HttpClient) { }

  runCommand(command: string): Observable<string> {
    return this.http.post(this.apiUrl + '/run', { command }, { responseType: 'text' });
  }
  

  getProducts(): Observable<productDto[]> {
    return this.http.get<productDto[]>(`${this.apiUrl}/products`);
  }

  getProduct(id: string): Observable<productDto[]> {
    return this.http.get<productDto[]>(`${this.apiUrl}/products/${id}`);
  }

  uploadFile(file: File, fileName: string): Observable<any> {
    const formData: FormData = new FormData();
    formData.append('file', file);
    formData.append('fileName', fileName); // Append 'fileName' to the FormData

    return this.http.post(`${this.apiUrl}/upfile`, formData, {
      reportProgress: true, // Optional: if you want to track progress
      observe: 'events'     // Optional: if you want to receive HttpEvents
    });
  }

  checkUser(username: string, password: string): Observable<any> {
    const body = { username, password };
    return this.http.post(`${this.apiUrl}/checkUser`, body);
  }
  


}
