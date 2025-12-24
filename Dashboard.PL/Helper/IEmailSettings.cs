using Dashboard.DAL.Models.Identity;
namespace Dashboard.PL.Helper
{
    public interface IEmailSettings
    {
        public void SendEmail(Email email);
    }
}
