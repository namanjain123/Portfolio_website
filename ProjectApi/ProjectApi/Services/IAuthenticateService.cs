using ProjectApi.Model;

namespace ProjectApi.Services
{
    public interface IAuthenticateService
    {
        User Authenticate(string userName, string password);
    }
}
