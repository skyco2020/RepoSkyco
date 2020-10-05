using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Thinktecture.IdentityModel.Tokens;

namespace SkyCoApi.Providers
{
    public class CustomJwtFormat : ISecureDataFormat<AuthenticationTicket>
    {
        private readonly string _issuer = string.Empty;

        // el _issuer es el emisor del token que sera la misma api, que será el que provee authorizacion y datos
        // ya que no lo tenemos separados ponemos que seriamos nosotros mismos aunque, podria pasarsea uri o un string.
        public CustomJwtFormat(string issuer)
        {
            _issuer = issuer;
        }
        public string Protect(AuthenticationTicket data)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }

            string audienceId = System.Configuration.ConfigurationManager.AppSettings["as:AudienceId"];

            string symmetricKeyAsBase64 = System.Configuration.ConfigurationManager.AppSettings["as:AudienceSecret"];

            var keyByteArray = TextEncodings.Base64Url.Decode(symmetricKeyAsBase64);

            var signingKey = new HmacSigningCredentials(keyByteArray);

            var issued = data.Properties.IssuedUtc;

            var expires = data.Properties.ExpiresUtc;

            var token = new System.IdentityModel.Tokens.JwtSecurityToken(_issuer, audienceId, data.Identity.Claims, issued.Value.UtcDateTime, expires.Value.UtcDateTime, signingKey);

            var handler = new System.IdentityModel.Tokens.JwtSecurityTokenHandler();

            var jwt = handler.WriteToken(token);
            //this.Unprotect("eyJ0eXAiOiJKV1QiLCJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTM4NCJ9.eyJuYW1laWQiOiIyZGMxZTRlMC0xNjdjLTQ4MWQtOTZjMC0zOGQzYmIxNzA5ZDgiLCJ1bmlxdWVfbmFtZSI6IlVzdWFyaW8iLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL2FjY2Vzc2NvbnRyb2xzZXJ2aWNlLzIwMTAvMDcvY2xhaW1zL2lkZW50aXR5cHJvdmlkZXIiOiJBU1AuTkVUIElkZW50aXR5IiwiQXNwTmV0LklkZW50aXR5LlNlY3VyaXR5U3RhbXAiOiJiNWNiMGMxYi05OWI2LTQ1NmItOWRiMC0xODRiNjE0NzVjNDciLCJyb2xlIjoiVXNlciIsIk15VHlwZSI6IjQ1IiwiaXNzIjoiaHR0cDovL2xvY2FsaG9zdDo4MDg5IiwiYXVkIjoiVHdpY2VUYWxlbnQiLCJleHAiOjE0ODk4NTMwNzgsIm5iZiI6MTQ4OTc2NjY3OH0.wuj8cRpwjCr75eyLrPpgvwUk8l0cmR07Cxetm_Ei2_Ym6At32QteM22tqT2hSaph");
            return jwt;
        }

        public AuthenticationTicket Unprotect(string protectedText)
        {
            throw new NotImplementedException();
        }
    }
}