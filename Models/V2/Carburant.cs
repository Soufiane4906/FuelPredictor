using Microsoft.AspNetCore.Cors;
using System.ComponentModel;

namespace FuelPredictor.Models.V2
{
    [DisplayName("Carburant")]
    public class Carburant : ModelBase
    {
        public string TypeCarburant { get; set; }
    }


}

