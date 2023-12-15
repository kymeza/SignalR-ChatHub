import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, FormArray, ReactiveFormsModule } from '@angular/forms';
import { OrderService } from './st-form.service';
import { CommonModule } from '@angular/common';
import { clientDto } from '../dtos/supertienda/clientDto';
import { productDto } from '../dtos/supertienda/productDto';

import { ordersCompositeDto } from 'src/dtos/business/ordersCompositeDto';

@Component({
  selector: 'app-order-form',
  templateUrl: './st-form.component.html',
  styleUrls: ['./st-form.component.css'],
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule],
})
export class OrderFormComponent implements OnInit{
  orderForm: FormGroup;
  orderDetailsFormArray: FormArray;
  
  @Input() clients: clientDto[] = [];
  @Input() products: productDto[] = [];

  @Output() onDataLoadRequest = new EventEmitter<void>();

  ngOnInit() {
    this.onDataLoadRequest.emit();
  }
  
  constructor(
    private fb: FormBuilder,
    private orderService: OrderService
  ) {
    // Create form group for order
    this.orderForm = this.fb.group({
      order: this.fb.group({
        idPedido: '',
        clienteId: '',
        fechaPedido: Date.now(),  
        fechaEnvío: Date.now(),
        idPrioridad: '',
        idCiudad: ''
      }),
      orderDetails: this.fb.array([])
    });

    this.orderDetailsFormArray = this.orderForm.get('orderDetails') as FormArray;
  }

  

  addOrderDetail() {
    const orderDetailIndex = this.orderDetailsFormArray.length + 1; // This will start from 1 for the first item
    const orderDetail = this.fb.group({
      lineaDetalle: orderDetailIndex,
      articuloId: '',
      cantidad: '',
      descuento: '',
      costeEnvío: ''
    });

    this.orderDetailsFormArray.push(orderDetail);
  }

  removeOrderDetail(index: number) {
    this.orderDetailsFormArray.removeAt(index);
  }

  onSubmit() {
    if (this.orderForm.valid) {
      const orderData: ordersCompositeDto = this.orderForm.value;
      const orderId = orderData.order.idPedido;

      // Set idPedido for each order detail
      orderData.orderDetails.forEach(detail => {
        detail.idPedido = orderId;
      });

      this.orderService.createOrder(orderData).subscribe({
        next: (result) => {
          console.log('Order and details created', result);
          // Handle successful creation
        },
        error: (error) => {
          console.error('There was an error creating the order and details', error);
          // Handle errors
        }
      });
    }
  }
}
