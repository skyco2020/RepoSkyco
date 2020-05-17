using System;
using System.Collections.Generic;
using System.Net.Http;
using SkyCoApi.Infraestructure;
using SkyCoApi.Models.DTO;
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

namespace SkyCoApi.Controllers
{
    /// <summary>
    /// login controller class for authenticate users
    /// </summary>
    /// 
    public class AccountController : BaseApiController
    {
        #region Single
        private ISkyco_AccountServices _services;
        public AccountController(ISkyco_AccountServices services)
        {
            _services = services;
        }
        #endregion

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
                    //AccountType = Convert.ToByte(identityClaims.FindFirst("Role").Value),
                    PasswordHash = identityClaims.FindFirst("Password").Value,
                    EmailAddress = identityClaims.FindFirst("EmailAddress").Value,
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
                        Username = usr.Username != String.Empty ? usr.Username : "without username",
                        EmailAddress = usr.EmailAddress != String.Empty ? usr.EmailAddress : "without EmailAddress",
                        PhoneNumber = usr.PhoneNumber != String.Empty ? usr.PhoneNumber : "without PhoneNumber",
                        UserId = usr.UserId != 0 ? usr.UserId : 0,
                        AccountId = usr.AccountId != 0 ? usr.AccountId : 0,
                        AccountType = usr.AccountType != 0 ? usr.AccountType : 0,
                        PasswordHash = usr.PasswordHash != String.Empty ? usr.PasswordHash : "without PasswordHash"
                    };
                    String token = TokenGenerator.GenerateTokenJwt(mdl);
                    return Ok(token);
                }
                return Unauthorized();
            }
            catch (Exception ex)
            {
                throw HandlerExceptions.GetInstance().RunCustomExceptions(ex);
            }
        }
    }
}