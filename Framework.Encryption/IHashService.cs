namespace Framework.Encryption
{
    public interface IHashService
    {
        public string Encode(long number);
        public long Decode(string hash);
        public long? NullableDecode(string hash);
    }
}
