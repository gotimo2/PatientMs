

using Newtonsoft.Json;

namespace PatientMs.Domain
{
    public class MedicalCondition
    {
        public long id { get; private init; }

        public Guid foreignId { get; init; }

        [JsonConstructor]
        public MedicalCondition() { }

        public override bool Equals(object? obj)
        {
            if (obj == null) throw new ArgumentNullException(nameof(obj));
            if (ReferenceEquals(this, obj)) return true;
            var cond = obj as MedicalCondition;
            if (foreignId.Equals(cond.foreignId)) { return true; }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}