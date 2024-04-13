namespace FuelPredictor.Models.V2
{
    public class Carburant : ModelBase
    {
        public TypeCarburant TypeCarburant;
    }

    public enum TypeCarburant
    {
        Essence,
        Diesel
        
    }
}

