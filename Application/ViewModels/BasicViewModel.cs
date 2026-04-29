using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoDeAprendizajeP3.Core.Application.ViewModels
{
    public class BasicViewModel<Tkey>
    {
        public required Tkey Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
    }
}
