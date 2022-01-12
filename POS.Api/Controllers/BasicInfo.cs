using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POS.Api.Cache;
using POS.Api.Extensions;
using POS.Api.Filter;
using POS.Api.Models.Basic;
using POS.BusinessLogic;
using POS.DataContext;
using POS.Entity;
using POS.Utility;
using System;

namespace POS.Api.Controllers
{
    
    public class BasicInfo : BaseController
    {
        private readonly IHttpContextAccessor _http;
        private readonly Logger _logger;
        private readonly LoggerBL _loggerBL;
        private readonly UserDataContext _userDataContext;

        public BasicInfo(IHttpContextAccessor http,Logger logger,LoggerBL loggerBL,UserDataContext userDataContext)
        {
            _http = http;
            _logger = logger;
            _loggerBL = loggerBL;
            _userDataContext = userDataContext;
        }

        [HttpGet]
        public IActionResult GetCurrentUser()
        {
            try
            {
                return Ok(_http.GetCurrentUser());
            }
            catch (Exception ex)
            {
                _logger.FileWithDBLogger(ex);
                return BadRequest(ex);
            }
        }

        [HttpGet]
        public IActionResult GetMyIP()
        {
            _logger.FileLogger("GetMyIP Method is working Fine.", Logger.TYPE.SUCCESS);
            return Ok(_http.GetClientIPAddress());
        }

        [HttpGet]
        public IActionResult GetServerIP()
        {
            _logger.FileLogger("GetServerIP Method is working Fine.", Logger.TYPE.SUCCESS);
            return Ok(_http.GetServerIPAddress());
        }


        [HttpGet]
        public IActionResult GetLogTypes()
        {
            try
            {
                return Ok(CacheReference.GetLoggerTypes());
            }
            catch(Exception ex)
            {

                
                
                return BadRequest(ex.Message);
            }
        }

        

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Encrypt(CipherModel model)
        {
            try
            {
                string encryptedText = AESCryptography.Encrypt(model.Key, model.Text);
                return Ok(encryptedText);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Decrypt(CipherModel model)
        {
            try
            {
                string decryptedText = AESCryptography.Encrypt(model.Key, model.Text);
                return Ok(decryptedText);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetMD5Hash(string text)
        {
            try
            {
                return Ok(MD5Hash.Hash(text));
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        public IActionResult GetCurrentUserInfo()
        {
            try
            {
                UserEntity user = _userDataContext.GetUserByUserId(_http.GetCurrentUser());
                if (user == null)
                {
                    ModelState.AddModelError("User", "User Not Found!");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                return Ok(user);
                
            }
            catch (Exception ex)
            {
                _logger.FileWithDBLogger(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


    }


}
