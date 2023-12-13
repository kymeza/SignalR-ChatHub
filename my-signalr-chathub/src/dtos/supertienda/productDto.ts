interface productDto {
    idArtículo: string;
    idSubCategoria: string;
    producto?: string;  // The '?' denotes an optional property
    precioUnitario?: number;  // C# 'double' translates to 'number' in TypeScript
    costoUnitario?: number;
  }
  