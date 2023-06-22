using Newtonsoft.Json;
using PatientMs.Value.Insurance;

namespace PatientMs.Domain
{
    public class InsurancePolicy
    {
        public long id { get; private init; }
        public string Name { get; init; }
        public decimal Coverage { get; init; }
        public decimal Deductible { get; init; }

        public InsurancePolicy(string name, Coverage coverage, Deductible deductible)
        {
            Name = name;
            Coverage = coverage.Value;
            Deductible = deductible.Value;
        }

        public static InsurancePolicy NoInsurance()
        {
            return new InsurancePolicy("No insurance", new Coverage(0), new Deductible(885));
        }

        [JsonConstructor]
        private InsurancePolicy() { }

        public Decimal apply(Decimal cost)
        {
            Decimal result = cost;
            if (result > Deductible)
            { result = Deductible; }
            result = result * ((100 - Coverage) / 100);
            return result;
        }
    }
}
