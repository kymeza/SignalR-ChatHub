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
import { FormArray, FormControl, FormGroup, Validators } from '@angular/forms';
import { FormComponent } from 'src/form/form.component';



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
    FormComponent,
  ],
})
export class AppComponent {
  title = 'my-signalr-chathub';
  isLoggedIn$: Observable<boolean>;

  constructor(private store: Store<{ auth: AuthState }>) {
    this.isLoggedIn$ = this.store.select(state => state.auth.isLoggedIn);
  }

  step = 1;

  yearlyToggle = false;

  multiStep = new FormGroup({
    user: new FormGroup({
      name: new FormControl('', Validators.required),
      email: new FormControl('', [
        Validators.required,
        Validators.pattern(/^\S+@\S+\.\S+$/),
      ]),
      phone: new FormControl('', [
        Validators.required,
        Validators.pattern('^\\+?[0-9]{1,3}?[-.\\s]?\\(?[0-9]{1,3}?\\)?[-.\\s]?[0-9]{1,4}[-.\\s]?[0-9]{1,4}[-.\\s]?[0-9]{1,9}$'),
      ]),
    }),
    plans: new FormGroup({
      plan: new FormControl('', Validators.required),
    }),
    addons: new FormGroup({
      online: new FormControl(false),
      storage: new FormControl(false),
      profile: new FormControl(false),
    }),
  });

  costMap = {
    arcade: 9,
    advanced: 12,
    pro: 15,
    online: 1,
    storage: 2,
    profile: 2,
  };

  increaseStep() {
    this.step += 1;
  }

  decreaseStep() {
    this.step -= 1;
  }

  term() {
    this.yearlyToggle = !this.yearlyToggle;
  }

  change() {
    this.step = 2;
  }

  submit() {
    if (this.step == 1 && this.multiStep.controls.user.invalid) {
      return;
    }
    if (this.step == 2 && this.multiStep.controls.plans.invalid) {
      return;
    }
    this.increaseStep();
  }
}
