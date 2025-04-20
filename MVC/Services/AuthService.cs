using MVC.Models.Dtos;
using MVC.Models;
using static Utility.SD;

namespace MVC.Services
{
    public class AuthService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private string Url;
        private readonly BaseService _baseService;
        public AuthService(IHttpClientFactory clientFactory, IConfiguration configuration, BaseService baseService)
        {
            _httpClientFactory = clientFactory;
            Url = configuration.GetValue<string>("ServiceUrls:Api");
            _baseService = baseService;
        }
        public async Task<T> LoginAsync<T>(LoginRequestDto dto)
        {
            return await _baseService.SendAsync<T>(new APIRequest()
            {
                ApiType = ApiType.Post,
                Data = dto,
                Url = Url + "/api/AppUser/Login",
            }, withBearer: false);
        }
        public async Task<T> RegisterAsync<T>(RegisterationRequestDto dto)
        {
            return await _baseService.SendAsync<T>(new APIRequest()
            {
                ApiType = ApiType.Post,
                Data = dto,
                Url = Url + "/api/AppUser/Register",
            }, withBearer: false);
        }
        public async Task<T> LogoutAsync<T>(TokenDto obj)
        {
            return await _baseService.SendAsync<T>(new APIRequest()
            {
                ApiType = ApiType.Post,
                Data = obj,
                Url = Url + "/api/AppUser/Revoke",
            });
        }
    }
}
