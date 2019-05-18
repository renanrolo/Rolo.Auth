using System;

namespace Rolo.Auth.Core.ValueObjects
{
    public class TokenModel
    {
        public DateTime Created { get; set; }
        public DateTime Expiration { get; set; }
        public string Email { get; set; }
        public string AccessToken { get; set; }
    }
}
