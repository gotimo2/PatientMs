using PatientMs.Controller.Dto.TreatmentDto;

namespace PatientMs.Service.Client
{

    public interface ITreatmentClient
    {
        public Task<bool> ConditionExists(Guid id);
        public Task<TreatmentDto> GetTreatment(Guid id);
    }

    public class TreatmentClient : ITreatmentClient
    {
        private IHttpClientFactory httpClientFactory;

        public TreatmentClient(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }


        public async Task<bool> ConditionExists(Guid id)
        {
            var client = httpClientFactory.CreateClient("treatment");
            var result = await client.GetAsync("api/v1/medical-condition/" + id);
            if (!result.IsSuccessStatusCode)
            {
                return false;
            }
            return true;
        }

        public async Task<TreatmentDto> GetTreatment(Guid id)
        {
            var client = httpClientFactory.CreateClient("treatment");
            var response = await client.GetAsync($"api/v1/treatment/{id}");

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw new KeyNotFoundException("Treatment does not exist!");
            }
            return await response.Content.ReadFromJsonAsync<TreatmentDto>();

        }
    }
}
