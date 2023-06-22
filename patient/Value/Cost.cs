namespace PatientMs.Value
{
    public class Cost
    {
        public decimal Value { get; private set; }

        public string Product { get; private set; }

        public Cost(decimal cost, string product)
        {
            if (cost < 0) { throw new ArgumentException("Cost cannot be negative!"); }
            Value = cost;
            Product = product;
        }
    }
}
