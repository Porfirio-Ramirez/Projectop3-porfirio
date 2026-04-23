using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModel
{
    public class BasicViewModel<Tkey>
    {
        public required Tkey Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
    }
}
