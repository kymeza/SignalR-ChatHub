import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Store } from '@ngrx/store';
import * as AuthActions from '../actions/auth.actions';
import { selectAuthLoading, selectLoginError } from '../selectors/auth.selectors';
import { Observable } from 'rxjs';
import { AuthState } from '../reducers/auth.reducer'; // make sure to import the AppState interface
import { CommonModule } from '@angular/common';

interface AppState {
    auth: AuthState;
  }
  

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, CommonModule],
  template: `
    <div>
      <form (ngSubmit)="login()">
        <input type="text" [(ngModel)]="rut" name="rut" placeholder="RUT" />
        <input type="password" [(ngModel)]="password" name="password" placeholder="Password" />
        <input type="text" [(ngModel)]="codigoAplicacionOrigen" name="codigoAplicacionOrigen" placeholder="Codigo Aplicacion Origen" />
        <button type="submit">Login</button>
      </form>
    </div>
    <div *ngIf="loading$ | async">
    Loading...
    </div>
    <div *ngIf="(loginError$ | async) as error">
      {{ error }} 
    </div>

  `
})

export class LoginComponent {
  rut: string = '';
  password: string = '';
  codigoAplicacionOrigen: string = '';
  loading$!: Observable<boolean>;
  loginError$!: Observable<string | null>;



  constructor(private store: Store<AppState>) {
    this.loading$ = this.store.select(selectAuthLoading);
    this.loginError$ = this.store.select(selectLoginError);
  }

  login() {
    this.store.dispatch(AuthActions.resetLoginError());
    this.store.dispatch(AuthActions.login({
      rut: this.rut,
      password: this.password,
      codigoAplicacionOrigen: this.codigoAplicacionOrigen
    }));
  }
  

}
