import { createSelector } from '@ngrx/store';
import { AuthState } from '../reducers/auth.reducer';

export const selectAuthState = (state: { auth: AuthState }) => state.auth;

export const selectAuthLoading = createSelector(
  selectAuthState,
  (state: AuthState) => state.loading,
);

export const selectLoginError = createSelector(
  selectAuthState,
  (state: AuthState) => state.loginError
);
