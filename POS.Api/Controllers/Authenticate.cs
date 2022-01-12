using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POS.Api.Extensions;
using POS.Api.Models.Authenticate;
using POS.Api.Models.User;
using POS.Api.Token;
using POS.BusinessLogic;
using POS.Entity;
using POS.Utility;
using System;

namespace POS.Api.Controllers
{
    
    [AllowAnonymous]
    public class Authenticate : BaseController
    {
        private readonly UserBL _userBL;
        private readonly IMapper _mapper;
        private readonly Logger _logger;
        private readonly TokenManager _tokenManager;

        public Authenticate(UserBL userBL, IMapper mapper, Logger logger,TokenManager tokenManager)
        {
            _userBL = userBL;
            _mapper = mapper;
            _logger = logger;
            _tokenManager = tokenManager;
        }

        [HttpPost]
        public IActionResult RegisterUser([FromBody] RegisterModel registerModel)
        {
            try
            {

                if (_userBL.IsUserAvailableByEmailId(registerModel.EmailId))
                {
                    ModelState.AddModelError("EmailId", "Email Id Already Exists!");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                UserEntity user = _mapper.Map<UserEntity>(registerModel);
                bool isSuccess = _userBL.RegisterUser(user);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.FileLogger(ex.Message, Logger.TYPE.ERROR);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }


        }

        [HttpPost]
        public IActionResult IsEmailExists([FromBody] GetUserModel model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.EmailId))
                {
                    ModelState.AddModelError("EmailId", "Email Id is Required!");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                return Ok(_userBL.IsUserAvailableByEmailId(model.EmailId));
            }
            catch (Exception ex)
            {
                _logger.FileLogger(ex.Message, Logger.TYPE.ERROR);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPost]
        public IActionResult Login([FromBody] LoginModel model)
        {
            if (_tokenManager.Authenticate(model.EmailId, model.Password))
            {
                UserEntity user = _userBL.GetUserByEmailId(model.EmailId);
                return Ok(new { token = _tokenManager.NewToken(user.UserId) });
            }
            else
            {
                ModelState.AddModelError("Unauthorized", "You are not authorized.");
                return Unauthorized(ModelState);
            }
        }




    }
}
