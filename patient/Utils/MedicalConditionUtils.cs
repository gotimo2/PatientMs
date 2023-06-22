using PatientMs.Controller.Dto;
using PatientMs.Domain;

namespace PatientMs.Utils
{
    public static class MedicalConditionUtils
    {
        public static MedicalConditionDto toDto(this MedicalCondition condition)
        {
            return new MedicalConditionDto
            {
                id = condition.foreignId
            };
        }

        public static MedicalCondition toMedicalCondition(this MedicalConditionDto dto)
        {
            return new MedicalCondition
            {
                foreignId = dto.id
            };
        }

        public static MedicalCondition toMedicalCondition(this Consumer.Dto.AddMedicalConditionDto dto)
        {
            return new MedicalCondition
            {
                foreignId = dto.id
            };
        }

        public static List<MedicalConditionDto> toDtos(this List<MedicalCondition> conditions)
        {
            var result = new List<MedicalConditionDto>();
            foreach (var condition in conditions)
            {
                result.Add(toDto(condition));
            }
            return result;
        }


    }
}
