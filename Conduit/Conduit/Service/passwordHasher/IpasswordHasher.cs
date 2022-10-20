namespace Conduit.Service.passwordHasher
{
    public interface IpasswordHasher
    {
        string HashPassword(string password);
        bool VerifyPassword(string password,string HashPassword);
    }
}
