using PatientMs.Data;

namespace PatientMs.Service
{
    public interface ICostService
    {
        public Task<decimal> GetCostsSoFar();
        public Task IncrementCosts(decimal amount);
    }

    public class CostService : ICostService
    {

        private readonly ICostAccessor accessor;

        public CostService(ICostAccessor accessor)
        {
            this.accessor = accessor;
        }

        public async Task<decimal> GetCostsSoFar()
        {
            return await accessor.GetCostSoFar();
        }

        public async Task IncrementCosts(decimal amount)
        {
            await accessor.Increment(amount);
        }
    }
}
