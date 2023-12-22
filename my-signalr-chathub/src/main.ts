// src/main.ts

import { enableProdMode } from '@angular/core';
import { bootstrapApplication } from '@angular/platform-browser';
import { AppComponent } from './app/app.component';
import { environment } from './environments/environment';
import { HttpClientModule } from '@angular/common/http';
import { provideStore } from '@ngrx/store';
import { provideEffects } from '@ngrx/effects';
import { authReducer } from './auth/reducers/auth.reducer';
import { AuthEffects } from './auth/effects/auth.effects';
import { RouterModule, provideRoutes } from '@angular/router';
import { importProvidersFrom } from '@angular/core';
import { routes } from './app/app-routing.module';


if (environment.production) {
  enableProdMode();
}

bootstrapApplication(AppComponent, {
  providers: [
    importProvidersFrom(HttpClientModule),
    provideStore({ auth: authReducer }),
    provideEffects([AuthEffects]),
    importProvidersFrom(RouterModule.forRoot(routes)),
    // ...any other providers you need globally
  ]
}).catch(err => console.error(err));
