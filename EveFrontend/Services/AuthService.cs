using Microsoft.AspNetCore.Components;

namespace EveFrontend.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
         private readonly NavigationManager _navigationManager;

        public AuthService(HttpClient httpClient, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _navigationManager = navigationManager;
        }

        public async Task SignInWithMicrosoft()
        {           
            _navigationManager.NavigateTo("http://localhost:5066/api/auth/login", forceLoad: true);
        }
    }
}