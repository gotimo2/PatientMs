namespace PatientMs.Value.MedicalDetails
{
    public class Length
    {
        public double Value { get; init; }

        public Length(double value)
        {
            if (value < 0)
            {
                throw new ArgumentException("length cannot be below 0m");
            }
            if (value > 3.5)
            {
                throw new ArgumentException("length cannot be above 3.5m");
            }
            this.Value = value;
        }
    }
}
