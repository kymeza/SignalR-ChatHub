import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { of } from 'rxjs';
import { catchError, map, switchMap, tap } from 'rxjs/operators';
import * as AuthActions from '../actions/auth.actions';
import { AuthService } from '../services/auth.service';
import { Store } from '@ngrx/store'; // Make sure to import Store
import { AuthState } from '../reducers/auth.reducer'; // Import your AppState interface



@Injectable()
export class AuthEffects {
  login$ = createEffect(() =>
    this.actions$.pipe(
      ofType(AuthActions.login),
      tap(() => this.store.dispatch(AuthActions.loginStart())),
      switchMap(({ rut, password, codigoAplicacionOrigen }) =>
        this.authService.login(rut, password, codigoAplicacionOrigen).pipe(
          map(() => AuthActions.loginSuccess()),
          catchError(error => of(AuthActions.loginFailure({ error }))),
          tap(() => this.store.dispatch(AuthActions.loginEnd()))
        )
      )
    )
  );

  constructor(
    private actions$: Actions,
    private authService: AuthService,
    private store: Store<AuthState> // Inject the Store here
  ) {}
}
