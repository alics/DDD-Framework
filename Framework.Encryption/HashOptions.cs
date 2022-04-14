namespace Framework.Encryption
{
    public class HashOptions
    {
        public string Salt { get; set; }
        public int MinHashLength { get; set; }
        public string Alphabet { get; set; }
    }
}
