using System.ComponentModel.DataAnnotations;

namespace WebApp.UI.Models
{
    public class AddRegionViewModel
    {

        public string Code { get; set; }

        public string Name { get; set; }

        public string? RegionImageUrl { get; set; }
    }
}
