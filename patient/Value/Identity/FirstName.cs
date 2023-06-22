namespace PatientMs.Value
{
    public class FirstName
    {
        public string Value { get; init; }

        public FirstName(string value)
        {
            if (value.Length < 1)
            {
                throw new ArgumentException("First name must be at least one character");
            }
            if (value.Length > 100)
            {
                throw new ArgumentException("First name cannot be above 100 characters");
            }
            this.Value = value;
        }
    }
}
