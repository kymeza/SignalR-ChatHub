export interface orderDto {
    idPedido: string;
    clienteId: string;
    fechaPedido?: Date;  // Optional Date type
    fechaEnvio?: Date;   // Optional Date type
    idPrioridad: string;
    idCiudad: string;
  }
  