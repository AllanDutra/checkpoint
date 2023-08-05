namespace Checkpoint.Core.DomainServices.Crypto
{
    public interface ICryptoDomainService
    {
        string EncryptToSha256(string value);
    }
}
