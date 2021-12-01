namespace Api.Domain.Security
{
    public class TokenConfiguration
    {
        public string Audience { get; set; } // o token vai ter um publico
        public string Issuer { get; set; } //Emissor
        public int Seconds { get; set; }//validade
    }
}
