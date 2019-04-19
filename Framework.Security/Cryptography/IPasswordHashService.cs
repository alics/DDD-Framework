namespace Framework.Security.Cryptography
{
    public interface IPasswordHashService
    {
        string HashSaltAndPassword(string salt, string clearTextPassword);
    }
}