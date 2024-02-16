using CloudCustomer.API.Config;
using CloudCustomer.API.Model;
using Microsoft.Extensions.Options;

namespace CloudCustomer.API.Services
{
    public class UsersService : IUsersService
    {
        private readonly HttpClient _httpClient;
        private readonly UsersApiOptions _apiConfig;

        public UsersService(HttpClient httpClient, IOptions<UsersApiOptions> apiConfig)
        {
            _httpClient = httpClient;
            _apiConfig = apiConfig.Value;
        }

        public async Task<List<User>?> GetAllUsers()
        {
            var response = await _httpClient.GetAsync(_apiConfig.Endpoint);

            if(response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return new List<User>();

            var responseContent = response.Content;
            var allUsers = await responseContent.ReadFromJsonAsync<List<User>>();

            return allUsers?.ToList();
        }
    }
}
