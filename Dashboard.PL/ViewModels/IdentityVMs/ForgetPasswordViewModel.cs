namespace Dashboard.PL.ViewModels.IdentityVMs
{
    public class ForgetPasswordViewModel
    {
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
