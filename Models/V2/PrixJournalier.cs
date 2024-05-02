
using FuelPredictor.Models.Users;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FuelPredictor.Models.V2
{
    public class PrixJournalier : ModelBase
    {
        [Required]
   
        [Display(Name ="Prix")]
        public float prix { get; set; }
 
        public DateTime date { get; set; }

        public int IDStation { get; set; }
        [ForeignKey("IDStation")]
        public Station? Station{ get; set; }


        public int? IDCarburant { get; set; }
    
        [ForeignKey("IDCarburant")]
        public Carburant? Carburant { get; set; }

        public PrixJournalier()
        { 

        }
    }
}
