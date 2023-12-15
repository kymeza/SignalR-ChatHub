import { orderDto } from "../supertienda/orderDto";
import { orderDetailDto } from "../supertienda/orderDetailDto";

export interface ordersCompositeDto {
    order: orderDto;
    orderDetails: orderDetailDto[];
}