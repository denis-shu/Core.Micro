namespace Micro.Base.Auth
{
    public class JwtOpt
    {
        public string ConfidentialKey { get; set; }
        public int ValidMinutes { get; set; }
        public  string Publisher { get; set; }
    }
}