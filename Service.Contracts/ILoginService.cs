using Shared.Input;

namespace Service.Contracts;

public interface ILoginService
{
    Task<string>  LoginAsync(LoginDto login);
}