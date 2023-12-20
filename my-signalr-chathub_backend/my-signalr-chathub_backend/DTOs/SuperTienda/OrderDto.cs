public class OrderDto
{
    public string IdPedido { get; set; }
    public string ClienteId { get; set; }
    public DateTime? FechaPedido { get; set; }
    public DateTime? FechaEnvio { get; set; }
    public string IdPrioridad { get; set; }
    public string IdCiudad { get; set; }
}