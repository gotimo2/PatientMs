namespace PatientMs.Value.MedicalDetails
{
    public class Weight
    {
        public double Value { get; init; }

        public Weight(double value)
        {
            if (value < 0)
            {
                throw new ArgumentException("Weight must be avove 0");
            }

            if (value > 2000)
            {
                throw new ArgumentException("Weight must be below 2000");
            }
            this.Value = value;
        }
    }
}
