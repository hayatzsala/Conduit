namespace Conduit.Service.passwordHasher
{
    public class BycryptPasswordHasher : IpasswordHasher
    {
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string password, string HashPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, HashPassword);
        }
    }
}
