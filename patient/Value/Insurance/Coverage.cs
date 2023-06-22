namespace PatientMs.Value.Insurance
{
    public class Coverage
    {
        public Decimal Value { get; init; }

        public Coverage(Decimal value)
        {
            if (value > 100)
            {
                throw new ArgumentException("Coverage cannot be above 100%");
            }
            if (value < 0)
            {
                throw new ArgumentException("coverage cannot be below 0%");
            }
            this.Value = value;
        }
    }
}
