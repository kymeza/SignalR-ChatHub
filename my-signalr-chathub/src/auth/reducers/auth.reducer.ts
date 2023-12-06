import { createReducer, on } from '@ngrx/store';
import * as AuthActions from '../actions/auth.actions';

export interface AuthState {
  isLoggedIn: boolean;
  loginError: string | null;
  loading: boolean;
}

export const initialState: AuthState = {
  isLoggedIn: false,
  loginError: null,
  loading: false,
};

export const authReducer = createReducer(
  initialState,
  on(AuthActions.loginSuccess, state => ({ ...state, isLoggedIn: true, loginError: null })),
  on(AuthActions.loginFailure, (state, { error }) => ({ ...state, isLoggedIn: false, loginError: error })),
  on(AuthActions.loginStart, (state) => ({ ...state, loading: true })),
  on(AuthActions.loginEnd, (state) => ({ ...state, loading: false })),
);
