namespace ProyectoDeAprendizajeP3.Core.Domain.Common
{
     public class BasicEntity<Tkey>
    {
        public required Tkey Id { get; set; }
        public required string name { get; set; }
        public string? description { get; set; }
    }
}
