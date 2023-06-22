namespace PatientMs.Value.Insurance
{
    public class Deductible
    {
        public Decimal Value { get; init; }

        public Deductible(Decimal value)
        {
            if (value < 385)
            {
                throw new ArgumentException("Deductible cannot be below 385E");
            }
            if (value > 885)
            {
                throw new ArgumentException("Deductible cannot be above 885E");
            }
            this.Value = value;
        }

    }
}
