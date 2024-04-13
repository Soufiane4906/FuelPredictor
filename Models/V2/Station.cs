using FuelPredictor.Models.Users;
using System.ComponentModel.DataAnnotations.Schema;
using NetTopologySuite.Geometries;


namespace FuelPredictor.Models.V2
{
    public class Station :ModelBase
    {


        public string Nom { get; set; }
        public string Adresse { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
/*        public Point Location { get; set; }
*/
        // Propriété de navigation vers l'utilisateur (gerant)


        public string? IDGerant { get; set; }
        [ForeignKey("IDGerant")]
        public ApplicationUser? Gerant { get; set; }
        public virtual ICollection<PrixJournalier>? PrixJournaliers { get; set; }



    }
}
