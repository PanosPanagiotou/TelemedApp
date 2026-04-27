using TelemedApp.Application.DTOs;

namespace TelemedApp.UI.Client.Services
{
    public class PatientsService(ApiClient api)
    {
        private readonly ApiClient _api = api;

        public Task<IEnumerable<PatientDto>?> GetAll()
            => _api.GetAsync<IEnumerable<PatientDto>>("api/patients");

        public Task<PatientDto?> Get(Guid id)
            => _api.GetAsync<PatientDto>($"api/patients/{id}");

        public Task<PatientDto?> Create(PatientDto p)
            => _api.PostAsync<PatientDto>("api/patients", p);

        public Task Update(PatientDto p)
            => _api.PutAsync($"api/patients/{p.Id}", p);

        public Task Delete(Guid id)
            => _api.DeleteAsync($"api/patients/{id}");
    }
}