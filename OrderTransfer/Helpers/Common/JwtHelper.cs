using System;
using System.Collections.Generic;
using System.Text;

namespace OrderTransfer.Helpers.Common
{
    class JwtHelper
    {
        public void ValidateToken()
        {
            //TODO: VALİDATE TOKEN
            //var tokenHandler = new JwtSecurityTokenHandler();
            //var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

            //if (jwtToken == null)
            //    return null;

            //var symmetricKey = Convert.FromBase64String(Secret);
            //var validationParameters = new TokenValidationParameters()
            //{
            //    ClockSkew = TimeSpan.Zero,
            //    RequireExpirationTime = true,
            //    ValidateIssuer = false,
            //    ValidateAudience = false,
            //    IssuerSigningKey = new SymmetricSecurityKey(symmetricKey)
            //};

            //try
            //{
            //    return tokenHandler.ValidateToken(token, validationParameters, out SecurityToken securityToken);
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
        }
    }
}
