
namespace FuelPredictor.Models.V2
{
    public class PrixJournalier : ModelBase
    {
        public float prix;
        public DateOnly date;

        public  Station m_Station;
        public Carburant m_Carburant;

        public PrixJournalier()
        {

        }
    }
}
