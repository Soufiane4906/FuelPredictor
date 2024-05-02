using FuelPredictor.Models.Users;
using System.ComponentModel.DataAnnotations.Schema;
using NetTopologySuite.Geometries;


namespace FuelPredictor.Models.V2
{
    public class Station :ModelBase
    {


        public string Nom { get; set; }
        public string Adresse { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
/*        public Point Location { get; set; }
*/
        // Propriété de navigation vers l'utilisateur (gerant)


        public string? IDGerant { get; set; }
        [ForeignKey("IDGerant")]
        public ApplicationUser? Gerant { get; set; }

        public int? IDCompany { get; set; }
        [ForeignKey("IDCompany")]
        public Company? Company { get; set; }
        public virtual ICollection<PrixJournalier>? PrixJournaliers { get; set; }



    }
}
