import { CommonModule } from '@angular/common';
import { Component, Input, Output, EventEmitter } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';


@Component({
  selector: 'app-form',
  templateUrl: './form.component.html',
  styleUrls: ['./form.component.css'],
  standalone: true,
  imports: [ReactiveFormsModule,FormsModule, CommonModule],
})
export class FormComponent {
  @Input() step!: any;

  @Input() yearlyToggle: any;

  @Output() increase = new EventEmitter();

  @Output() decrease = new EventEmitter();

  @Output() toggled = new EventEmitter();

  @Output() changeEvent = new EventEmitter();

  @Input() submit: any;

  @Input() multiStep: any;

  @Input() costMap: any;

  increaseStep() {
    this.increase.emit();
  }

  decreaseStep() {
    this.decrease.emit();
  }

  termSwitch() {
    this.toggled.emit();
  }

  changeClicked() {
    this.changeEvent.emit();
  }

  calculateTotal() {
    let total = 0;
    let planKey = this.multiStep.value.plans?.plan;
    if (planKey) {
      total += this.costMap[planKey];
    }
    if (this.multiStep.value.addons?.online === true) {
      total += this.costMap['online'];
    }
    if (this.multiStep.value.addons?.storage === true) {
      total += this.costMap['storage'];
    }
    if (this.multiStep.value.addons?.profile === true) {
      total += this.costMap['profile'];
    }

    if (this.yearlyToggle) {
      // multiply by 10
      total *= 10;
    }
    return total;
  }
}
