using HashidsNet;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;

namespace Framework.Encryption
{
    public class HashService : IHashService
    {
        private readonly IHashids _hashids;
        private readonly HashOptions hashOptions;
        public HashService(IConfiguration configuration)
        {
            hashOptions = new HashOptions
            {
                Salt = configuration.GetSection("Hash:Salt").Value,
                MinHashLength = int.Parse(configuration.GetSection("Hash:MinHashLength").Value),
                Alphabet = configuration.GetSection("Hash:Alphabet").Value
            };

            _hashids = new Hashids(hashOptions.Salt, hashOptions.MinHashLength, hashOptions.Alphabet);
        }

        public long Decode(string hash)
        {
            var originData = _hashids.DecodeLong(hash).FirstOrDefault();
            return originData;
        }

        public long? NullableDecode(string hash)
        {
            try
            {

                var originData = _hashids.DecodeLong(hash).FirstOrDefault();
                return originData;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public string Encode(long number)
        {
            var hashed = _hashids.EncodeLong(number);
            return hashed;
        }
    }
}
