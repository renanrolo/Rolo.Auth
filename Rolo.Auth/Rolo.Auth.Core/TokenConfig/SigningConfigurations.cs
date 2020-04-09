using Microsoft.IdentityModel.Tokens;
using System;

namespace Rolo.Auth.Core.TokenConfig
{
    public class SigningConfigurations
    {
        public SecurityKey Key { get; }
        public SigningCredentials SigningCredentials { get; }
        public string SecretKey { get; set; }

        public SigningConfigurations(string secretKey)
        {
            SecretKey = secretKey;

            Key = GetSymetricSecurityKey(secretKey, false);

            SigningCredentials = SetSigningCredentials();
        }


        private SecurityKey GetSymetricSecurityKey(string secretKey, bool isBase64Encoded)
        {
            if (isBase64Encoded)
            {
                byte[] byteKey = Convert.FromBase64String(secretKey);
                return new SymmetricSecurityKey(byteKey);
            }
            else
            {
                byte[] plainSecretKeyBytes = System.Text.Encoding.UTF8.GetBytes(secretKey);
                string encodedKey = System.Convert.ToBase64String(plainSecretKeyBytes);
                byte[] byteKey = Convert.FromBase64String(encodedKey);
                return new SymmetricSecurityKey(byteKey);
            }

            //byte[] plainTextBytes = System.Text.Encoding.UTF8.GetBytes(SecretKey);
            //string base64 = System.Convert.ToBase64String(plainTextBytes);

            //byte[] byteKey = Convert.FromBase64String(base64);
            //return new SymmetricSecurityKey(byteKey);
        }

        private SigningCredentials SetSigningCredentials()
        {
            return new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);
        }

        //private RsaSecurityKey GenerateRandomSecurityKey()
        //{
        //    using (var provider = new RSACryptoServiceProvider(2048))
        //    {
        //        return new RsaSecurityKey(provider.ExportParameters(true));
        //    }
        //}
    }
}
