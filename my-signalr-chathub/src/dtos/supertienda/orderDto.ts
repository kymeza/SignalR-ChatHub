interface orderDto {
    idPedido: string;
    clienteId: string;
    fechaPedido?: Date;  // Optional Date type
    fechaEnvío?: Date;   // Optional Date type
    idPrioridad: string;
    idCiudad: string;
  }
  