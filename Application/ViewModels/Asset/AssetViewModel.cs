using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.ViewModel;
using Application.ViewModels.AssetType;

namespace Application.ViewModels.Asset
{
    public class AssetViewModel : BasicViewModel<int>
    {
        public required string Symbol { get; set; }

        public required int AssetTypeId { get; set; }
        public AssetTypeViewModel? AssetType { get; set; }
    }
}
