import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ChatComponent } from './chat/chat.component';
import { EffectsModule } from '@ngrx/effects';
import { HttpClientModule } from '@angular/common/http';
import { Store, StoreModule } from '@ngrx/store';
import { Observable } from 'rxjs';
import { AuthState } from 'src/auth/reducers/auth.reducer';
import { LoginComponent } from 'src/auth/login/login.component';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  standalone: true,
  imports: [
    RouterModule,
    CommonModule,
    ChatComponent,
    LoginComponent,
    HttpClientModule,
    // Other components or modules used in this component
  ],
})
export class AppComponent {
  title = 'my-signalr-chathub';
  isLoggedIn$: Observable<boolean>;

  constructor(private store: Store<{ auth: AuthState }>) {
    this.isLoggedIn$ = this.store.select(state => state.auth.isLoggedIn);
  }
}
