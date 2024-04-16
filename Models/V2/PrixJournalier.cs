
using FuelPredictor.Models.Users;
using System.ComponentModel.DataAnnotations.Schema;

namespace FuelPredictor.Models.V2
{
    public class PrixJournalier : ModelBase
    {
        public float prix;
        public DateOnly date;

      

        public int IDStation { get; set; }
        [ForeignKey("IDStation")]
        public Station Station{ get; set; }


        public int? IDCarburant { get; set; }
        [ForeignKey("IDCarburant")]
        public Carburant? GeCarburantrant { get; set; }

        public PrixJournalier()
        {

        }
    }
}
