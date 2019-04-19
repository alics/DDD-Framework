namespace Framework.Security.JwtToken
{
    public static class TokenConstants
    {
        public const int ExpiryInMinutes = 900000;
        public const string ValidAudience = "Pap.Security.Token.Bearer";
        public const string ValidIssuer = "Pap.Security.Token.Bearer";
        public const string SymmettricKey = "401b09eab3c013d4ca54922bb802bec8fd5318192b0a75f201d8b3727429090fb337591abd3e44453b954555b7a0812e1081c39b740293f765eae731f123456789mehdi";
    }
}