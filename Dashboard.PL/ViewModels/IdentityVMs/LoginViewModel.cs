global using System.ComponentModel.DataAnnotations;

namespace Dashboard.PL.ViewModels.IdentityVMs;


public class LoginViewModel
{
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
    [DataType(DataType.Password)]
    public string Password { get; set; }
    public bool RememberMe { get; set; }
}
