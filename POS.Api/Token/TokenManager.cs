using Microsoft.IdentityModel.Tokens;
using POS.Api.Extensions;
using POS.BusinessLogic;
using POS.Entity;
using POS.Utility;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace POS.Api.Token
{
    public class TokenManager
    {
        private readonly UserBL _userBL;
        private readonly Logger _logger;
        private JwtSecurityTokenHandler tokenHandler;
        private byte[] secretKey;

        public TokenManager(UserBL userBL,Logger logger)
        {
            _userBL = userBL;
            _logger = logger;
            tokenHandler = new JwtSecurityTokenHandler();
            secretKey = Encoding.ASCII.GetBytes(ApplicationConstants.SYMMETRIC_KEY);
        }

        public bool Authenticate(string emailId,string password)
        {

            if (!string.IsNullOrEmpty(emailId) && !string.IsNullOrEmpty(password))
            {
                UserEntity user = _userBL.GetUserByEmailId(emailId);
                if (user != null)
                {
                    string decryptedPassword = AESCryptography.Decrypt(ApplicationConstants.SYMMETRIC_KEY, user.Password);
                    if (password.IsEqualTo(decryptedPassword))
                    {
                        return true;
                    }
                    _logger.FileLogger("Invalid Password", Logger.TYPE.ERROR);
                }

                _logger.FileLogger("Invalid User", Logger.TYPE.ERROR);
            }
            else
            {
                _logger.FileLogger("Email Id or Password is empty!", Logger.TYPE.ERROR);
            }
            return false;
        }

        public string NewToken(string Id)
        {
            int hours = 0;
            hours = ApplicationConstants.TOKEN_EXPIRATION_HOURS;

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.Name, Id) }),
                Expires = DateTime.UtcNow.AddHours(hours),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtString = tokenHandler.WriteToken(token);
            _logger.FileLogger($"Token Generated succesfully for the #{Id}.", Logger.TYPE.SUCCESS);
            return jwtString;
        }

        private ClaimsPrincipal VerifyToken(string token, bool validateToken)
        {
            var claims = tokenHandler.ValidateToken(token,
                new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                    ValidateLifetime = validateToken,
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ClockSkew = TimeSpan.Zero,
                }, out SecurityToken validatedToken);
            return claims;
        }

        public ClaimsPrincipal VerifyToken(string token)
        {
            return VerifyToken(token, true);
        }

        public ClaimsPrincipal DecryptToken(string token)
        {
            return VerifyToken(token, false);
        }


    }
}
