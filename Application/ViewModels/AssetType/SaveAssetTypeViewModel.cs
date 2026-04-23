using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.AssetType
{
    public class SaveAssetTypeViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "You must enter the name of asset type")]
        public required string Name { get; set; }
        public string? Description { get; set; }
    }
}
