// supertienda.component.ts
import { Component, OnInit } from '@angular/core';
import { ClientService } from 'src/serivces/supertienda/clients.service';
import { OrderService } from 'src/serivces/supertienda/orders.service';
import { ProductService } from 'src/serivces/supertienda/products.service';
import { OrderDetailsService } from 'src/serivces/supertienda/orderDetails.service';
import { clientDto } from 'src/dtos/supertienda/clientDto';
import { orderDto } from 'src/dtos/supertienda/orderDto';
import { productDto } from 'src/dtos/supertienda/productDto';
import { orderDetailDto } from 'src/dtos/supertienda/orderDetailDto';
import { forkJoin } from 'rxjs';
import { ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';


@Component({
  selector: 'app-supertienda',
  templateUrl: './st-table.component.html',
  styleUrls: ['./st-table.component.css'],
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule],
})
export class SupertiendaComponent implements OnInit {
  clients: clientDto[] = [];
  orders: orderDto[] = [];
  products: productDto[] = [];
  orderDetails: orderDetailDto[] = [];
  clientsWithDetails: any[] = [];

  constructor(
    private clientService: ClientService,
    private orderService: OrderService,
    private productService: ProductService,
    private orderDetailService: OrderDetailsService
  ) {}

  ngOnInit() {
    this.fetchData();
  }

  fetchData() {
    forkJoin({
      clients: this.clientService.getClients(),
      orders: this.orderService.getOrders(),
      orderDetails: this.orderDetailService.getOrderDetails(),
      products: this.productService.getProducts()
    }).subscribe(({ clients, orders, orderDetails, products }) => {
      this.clientsWithDetails = clients.map(client => {
        const clientOrders = orders.filter(order => order.clienteId === client.idCliente);
        const clientOrdersWithDetails = clientOrders.map(order => {
          const details = orderDetails.filter(detail => detail.idPedido === order.idPedido);
          const detailsWithProduct = details.map(detail => {
            return { ...detail, product: products.find(product => product.idArticulo === detail.articuloId) };
          });
          return { ...order, orderDetails: detailsWithProduct };
        });
        return { ...client, orders: clientOrdersWithDetails };
      });
    });
  }
}
