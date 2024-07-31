namespace University_Management_System.API.Settings;

public class JWTSettings
{
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string SecretKey { get; set; }
        public int ExpirationSecond { get; set; }       
}