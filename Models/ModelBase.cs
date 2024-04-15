using System.ComponentModel.DataAnnotations;

namespace FuelPredictor.Models
{
    public partial class ModelBase
    {
        public int Id { get; set; }
        [ScaffoldColumn(false)]
        public DateTime CreatedAt { get; set; }
        [ScaffoldColumn(false)]
        public DateTime? UpdatedAt { get; set; }
        [ScaffoldColumn(false)]
        public DateTime? DeletedAt { get; set; }
    }
}
