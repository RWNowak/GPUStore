using GPUStoreMVC.Models.Other;

namespace GPUStoreMVC.Repositories.Abstract
{
    public interface IUserAuthentication
    {
        Task<Status> LoginAsync(Login model);
        Task LogoutAsync();
        Task<Status> RegisterAsync(Registration model);
        //Task<Status> ChangePasswordAsync(ChangePasswordModel model, string username);
    }
}
