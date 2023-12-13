import { createAction, props } from '@ngrx/store';

export const login = createAction(
  '[Auth] Login',
  props<{ rut: string; password: string; codigoAplicacionOrigen: string }>()
);

export const loginSuccess = createAction('[Auth] Login Success');
export const loginFailure = createAction(
  '[Auth] Login Failure',
  props<{ error: string }>()
);

export const loginStart = createAction('[Auth] Login Start');
export const loginEnd = createAction('[Auth] Login End');
export const resetLoginError = createAction('[Auth] Reset Login Error');