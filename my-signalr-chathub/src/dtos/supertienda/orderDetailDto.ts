export interface orderDetailDto {
    lineaDetalle: number;
    idPedido: string;
    articuloId: string;
    cantidad?: number;   // Optional 'number' type for nullable double
    descuento?: number;  // Optional 'number' type for nullable double
    costeEnvio?: number; // Optional 'number' type for nullable double
  }
  