using TelemedApp.Application.DTOs;

namespace TelemedApp.UI.Client.Services
{
    public class AppointmentsService(ApiClient api)
    {
        private readonly ApiClient _api = api;

        public Task<IEnumerable<AppointmentDto>?> GetUpcoming(int days = 7) =>
            _api.GetAsync<IEnumerable<AppointmentDto>>($"api/appointments?days={days}");

        public Task<AppointmentDto?> Get(Guid id) =>
            _api.GetAsync<AppointmentDto>($"api/appointments/{id}");

        public Task<AppointmentDto?> Create(AppointmentDto dto) =>
            _api.PostAsync<AppointmentDto>("api/appointments", dto);

        public Task Update(AppointmentDto dto) =>
            _api.PutAsync($"api/appointments/{dto.Id}", dto);

        public Task Delete(Guid id) =>
            _api.DeleteAsync($"api/appointments/{id}");
    }
}