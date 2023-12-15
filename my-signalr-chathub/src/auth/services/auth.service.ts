import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  constructor(private http: HttpClient) {}

  login(rut: string, password: string, codigoAplicacionOrigen: string) {
    return this.http.post(`https://localhost:5001/api/login?rut=${encodeURIComponent(rut)}&password=${encodeURIComponent(password)}&codigoAplicacionOrigen=${encodeURIComponent(codigoAplicacionOrigen)}`, null, { responseType: 'text', withCredentials: true } );
  }

  // Additional methods for session validation and logout
}
