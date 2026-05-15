namespace CarWebSite.BusinessLayer.Auth
{
    public class JwtSession
    {
        public static string Issuer {  get; set; } = string.Empty;
        public static string Audience {  get; set; } = string.Empty;
        public static string SecretKey {  get; set; } = string.Empty;
        public static int AccessTokenMinutes { get; set; } = 60;

    }
}
