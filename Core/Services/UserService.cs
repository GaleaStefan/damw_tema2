using Core.Dtos;
using DataLayer.Entities;
using DataLayer.Repositories;

namespace Core.Services;

public class UserService
{
    #region Fields
    private readonly AuthorizationService _authorizationService;
    private readonly PasswordService _passwordService;
    private readonly UserRepository _userRepository;
    #endregion

    #region Constructors
    public UserService(UserRepository userRepository, PasswordService passwordService,
        AuthorizationService authorizationService)
    {
        _userRepository = userRepository;
        _passwordService = passwordService;
        _authorizationService = authorizationService;
    }
    #endregion

    #region Public members
    public bool Register(RegisterDto registerInformation)
    {
        var existing = _userRepository.FindByEmail(registerInformation.Email);
        if (existing != null)
            return false;
        var newUser = new User
        {
            Email = registerInformation.Email,
            PasswordHash = _passwordService.Hash(registerInformation.Password),
            Role = registerInformation.Role
        };
        _userRepository.Insert(newUser);
        _userRepository.SaveChanges();
        return true;
    }

    public string? TryLogin(LoginDto loginInformation, out string? token)
    {
        token = null;

        var existing = _userRepository.FindByEmail(loginInformation.Email);
        if (existing == null)
            return "Invalid username";
        if (!_passwordService.VerifyPassword(existing.PasswordHash, loginInformation.Password))
            return "Invalid password";

        token = _authorizationService.GetToken(existing);
        return null;
    }
    #endregion
}
