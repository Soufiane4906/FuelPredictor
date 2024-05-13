using System.ComponentModel.DataAnnotations.Schema;

namespace FuelPredictor.Models.V2
{
    public class Company
    {
        public int Id { get; set; } // Identifiant unique de la compagnie

        public string Nom { get; set; } // Nom de la compagnie (ex: Africa, Shell)

        public string Pays { get; set; } // Pays d'origine de la compagnie

        public string Adresse { get; set; } // Adresse de la compagnie

        public string Email { get; set; } // Adresse e-mail de contact de la compagnie

        public string Telephone { get; set; } // Numéro de téléphone de contact de la compagnie

        public virtual ICollection<Station>? Stations { get; set; }
        [NotMapped]
        public IFormFile photo { get; set; }
        public string? PhotoPath { get; set; }

    }

}