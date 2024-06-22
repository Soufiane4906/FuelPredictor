
using FuelPredictor.Data;
using FuelPredictor.Models.Users;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FuelPredictor.Models.V2
{
    public class PrixJournalier : ModelBase
    {
        [Required]

        [Display(Name = "Prix")]
        public float prix { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [UniquePrixJournalier]
        public DateTime date { get; set; }

        public int IDStation { get; set; }
        [DisplayName("Station")]
        [ForeignKey("IDStation")]
        public Station? Station { get; set; }


        public int? IDCarburant { get; set; }
        [Display(Name = "Carburant")]
        [ForeignKey("IDCarburant")]
        public Carburant? Carburant { get; set; }

        public PrixJournalier()
        {

        }
    }


    public class UniquePrixJournalierAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var context = (FuelPredictorContext)validationContext.GetService(typeof(FuelPredictorContext));
            var prixJournalier = (PrixJournalier)validationContext.ObjectInstance;

            var existingPrix = context.PrixJournalier
                .AsNoTracking()
                .FirstOrDefault(p => p.IDStation == prixJournalier.IDStation &&
                                     p.IDCarburant == prixJournalier.IDCarburant &&
                                     p.date.Date == prixJournalier.date.Date &&
                                     p.Id != prixJournalier.Id); // Exclude current entity

            if (existingPrix != null)
            {
                return new ValidationResult("A price entry already exists for this station, carburant, and date.");
            }

            return ValidationResult.Success;
        }
    }
}
