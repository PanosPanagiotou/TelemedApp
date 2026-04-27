using TelemedApp.Application.DTOs;

namespace TelemedApp.UI.Client.Services
{
    public class DoctorsService(ApiClient api)
    {
        private readonly ApiClient _api = api;

        public Task<IEnumerable<DoctorDto>?> GetAll() => _api.GetAsync<IEnumerable<DoctorDto>>("api/doctors");
        public Task<DoctorDto?> Get(Guid id) => _api.GetAsync<DoctorDto>($"api/doctors/{id}");
        public Task<DoctorDto?> Create(DoctorDto d) => _api.PostAsync<DoctorDto>("api/doctors", d);
        public Task Update(DoctorDto d) => _api.PutAsync($"api/doctors/{d.Id}", d);
        public Task Delete(Guid id) => _api.DeleteAsync($"api/doctors/{id}");
    }
}