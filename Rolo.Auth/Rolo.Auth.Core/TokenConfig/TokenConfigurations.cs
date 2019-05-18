namespace Rolo.Auth.Core.TokenConfig
{
    public class TokenConfigurations
    {
        public TokenConfigurations()
        {
            Seconds = 500;
        }

        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int Seconds { get; set; }
    }
}
