using FuelPredictor.Data;
using Microsoft.EntityFrameworkCore;

namespace FuelPredictor.Service
{
    public class PredictionService
    {
        private readonly FuelPredictorContext _context;

        public PredictionService(FuelPredictorContext context)
        {
            _context = context;
        }
        
        public async Task<float> PredictPrixJournalierAsync(int stationId, int carburantId, DateTime date, double alpha = 0.5)
        {
            var prixJournalierList = await _context.PrixJournalier
                .Where(p => p.IDStation == stationId && p.IDCarburant == carburantId)
                .OrderBy(p => p.date)
                .ToListAsync();

            if (!prixJournalierList.Any())
            {
                throw new InvalidOperationException("No historical data available for the specified station and carburant.");
            }

            float previousSmoothedValue = prixJournalierList.First().prix;
            foreach (var prixJournalier in prixJournalierList.Skip(1))
            {
                previousSmoothedValue = (float)(alpha * prixJournalier.prix + (1 - alpha) * previousSmoothedValue);
            }

            return previousSmoothedValue;
        }
    }

}
