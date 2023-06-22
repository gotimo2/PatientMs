namespace PatientMs.Data
{
    public interface ICostAccessor
    {
        public Task<decimal> GetCostSoFar();
        public Task Increment(decimal amount);
    }

    //er was een idee om een hele DbSet te bouwen om een nummer in de DB op te slaan... was.
    public class CostAccessor : ICostAccessor
    {
        private decimal _amount;
        public Task<decimal> GetCostSoFar() => Task.FromResult(_amount);
        public Task Increment(decimal amount)
        {
            _amount += amount;
            return Task.CompletedTask;
        }
    }
}
