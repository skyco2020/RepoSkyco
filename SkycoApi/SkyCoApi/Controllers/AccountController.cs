using System;
using System.Collections.Generic;
using System.Net.Http;
using SkyCoApi.Infraestructure;
using SkyCoApi.Models.DTO.Single;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Security;
using SkyCoApi.Controllers.Token;
using Resolver.Exceptions;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Threading;
using System.Security.Claims;
using Resolver.Cryptography;
using Resolver.Mailing;
using Resolver.Enumerations;
using BusinessServices.Interfaces;
using BusinessEntities.BE;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace SkyCoApi.Controllers
{
    /// <summary>
    /// login controller class for authenticate users
    /// </summary>
    /// 
    [AllowAnonymous]
    public class AccountController : BaseApiController
    {
        #region Single
        private ISkyco_AccountServices _services;
        public AccountController(ISkyco_AccountServices services)
        {
            _services = services;
        }
        #endregion

        private String[] refreshTokens = {};

        [Authorize]
        [HttpGet]
        [Route("api/GetUserClaims")]
        public IHttpActionResult GetUserClaims()
        {
            try
            {

                ClaimsIdentity identityClaims = (ClaimsIdentity)User.Identity;
                Skyco_AccountDTO mdl = new Skyco_AccountDTO()
                {
                    AccountId = Convert.ToInt64(identityClaims.FindFirst("Idaccount").Value),
                    Username = identityClaims.FindFirst("username").Value,
                    PhoneNumber = identityClaims.FindFirst("PhoneNumber").Value,
                    UserId = Convert.ToInt64(identityClaims.FindFirst("UserId").Value),
                    EmailAddress = identityClaims.FindFirst("EmailAddress").Value,
                    Role = identityClaims.FindFirst("UserRole").Value,
                };
                return Ok(mdl);
            }
            catch (Exception ex)
            {
                throw HandlerExceptions.GetInstance().RunCustomExceptions(ex);
            }           
        }           
       
        [AllowAnonymous]
        [HttpPost]
        [Route("api/authenticate")]
        public IHttpActionResult Authenticate(Skyco_AccountDTO acc)
        {
            try
            {
                Skyco_AccountBE usr = _services.GetLogin(acc.Username, acc.PasswordHash);
                if (usr != null)
                {
                    Skyco_AccountDTO mdl = new Skyco_AccountDTO()
                    {
                        Username = usr.Username != null ? usr.Username : "without username",
                        EmailAddress = usr.EmailAddress != null ? usr.EmailAddress : "without EmailAddress",
                        PhoneNumber = usr.PhoneNumber != null ? usr.PhoneNumber : "without PhoneNumber",
                        UserId = usr.UserId != 0 ? usr.UserId : 0,
                        AccountId = usr.AccountId != 0 ? usr.AccountId : 0,
                        Role = usr.Skyco_AccountType.AccountTypeName,
                        refreshtoken = TokenGenerator.GenerateRefreshToken(),
                    };
                    String token = TokenGenerator.GenerateTokenJwt(mdl);
                    TokenMD tk = new TokenMD()
                    {
                        jwt = token,
                        refreshToken = mdl.refreshtoken
                    };
                    
                    return Ok(tk);
                }
                return Unauthorized();
            }
            catch (Exception ex)
            {
                throw HandlerExceptions.GetInstance().RunCustomExceptions(ex);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/refresh")]
        public IHttpActionResult refresh(TokenMD token)
        {
           
            try
            {
                var username = User.Identity.Name;
                ClaimsIdentity identityClaims = (ClaimsIdentity)User.Identity;
                String rfrtoken = identityClaims.FindFirst("refreshtoken").Value;

                if (token.refreshToken != rfrtoken)
                    throw new SecurityTokenException("Invalid refresh token");

                String generatenew = TokenGenerator.GenerateRefreshToken();
                String tokennew = TokenGenerator.GenerateToken(identityClaims,generatenew);

                TokenMD tk = new TokenMD()
                {
                    jwt = tokennew,
                    refreshToken = generatenew
                };
                return Ok(tk);
            }
            catch (Exception ex)
            {
                throw HandlerExceptions.GetInstance().RunCustomExceptions(ex);
            }
        }

        [Authorize]
        [HttpPost]
        [Route("api/CloseSession")]
        public IHttpActionResult CloseSession(TokenMD token)
        {

            try
            {
                ClaimsIdentity identityClaims = (ClaimsIdentity)User.Identity;
                String rfrtoken = identityClaims.FindFirst("refreshtoken").Value;
                var identity = User.Identity as ClaimsIdentity;
                var claim = (from c in identity.Claims.ToList()
                             where c.Value == rfrtoken
                             select c).FirstOrDefault();
                identity.RemoveClaim(claim);
                return Ok();
            }
            catch (Exception ex)
            {
                throw HandlerExceptions.GetInstance().RunCustomExceptions(ex);
            }
        }       
    }
}