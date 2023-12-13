public class OrderDetailDto
{
    public double LineaDetalle { get; set; }
    public string IdPedido { get; set; }
    public string ArticuloId { get; set; }
    public double? Cantidad { get; set; }
    public double? Descuento { get; set; }
    public double? CosteEnvío { get; set; }
}