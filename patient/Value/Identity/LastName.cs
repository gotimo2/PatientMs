namespace PatientMs.Value
{
    public class LastName
    {
        public string Value { get; init; }

        public LastName(string value)
        {
            {
                if (value.Length < 1)
                {
                    throw new ArgumentException("Last name must be at least one character");
                }
                if (value.Length > 100)
                {
                    throw new ArgumentException("Last name cannot be above 100 characters");
                }
                this.Value = value;
            }
        }
    }
}
