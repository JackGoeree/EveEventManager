using EveFrontend.Models;

namespace EveFrontend.Services
{
    public class EventService
    {
        private readonly HttpClient _httpClient;

        public EventService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Fetch all events
        public async Task<List<Event>> GetEvents(string? date = null, string? category = null)
        {
            var query = "";

            if (!string.IsNullOrEmpty(date) || !string.IsNullOrEmpty(category))
            {
                query = $"?{(date != null ? $"date={date}" : "")}{(category != null ? $"&category={category}" : "")}";
            }

            return await _httpClient.GetFromJsonAsync<List<Event>>($"api/events{query}");
        }

        // Create a new event
        public async Task<Event> CreateEvent(Event newEvent)
        {
           var response = await _httpClient.PostAsJsonAsync("api/events", newEvent);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Event>();
        }

        // Update an event
        public async Task UpdateEvent(int id, Event updatedEvent)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/events/{id}", updatedEvent);
            response.EnsureSuccessStatusCode();
        }

        // Delete an event
        public async Task DeleteEvent(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/events/{id}");
            response.EnsureSuccessStatusCode();
        }

        // RSVP to an event
        public async Task RSVPToEvent(int id, string attendeeEmail)
        {
            var response = await _httpClient.PostAsJsonAsync($"api/events/{id}/rsvp", attendeeEmail);
            response.EnsureSuccessStatusCode();
        }
    }
}