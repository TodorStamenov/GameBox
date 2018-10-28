using GameBox.Core;
using GameBox.Core.Enums;
using GameBox.Services.Contracts;
using GameBox.Services.Models.Binding.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameBox.Api.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly IAccountService account;

        public AccountController(IAccountService account)
        {
            this.account = account;
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody]LoginBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ServiceResult result = this.account.Login(model.Username, model.Password);

            if (result.ResultType == ServiceResultType.Fail)
            {
                ModelState.AddModelError(Constants.Common.Error, result.Message);
                return BadRequest(ModelState);
            }

            ServiceAuthResult authResult = (ServiceAuthResult)result;

            return Ok(new
            {
                username = authResult.Username,
                token = authResult.Token,
                isAdmin = authResult.IsAdmin,
                message = authResult.Message
            });
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register([FromBody]RegisterBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ServiceResult result = this.account.Register(model.Username, model.Password);

            if (result.ResultType == ServiceResultType.Fail)
            {
                ModelState.AddModelError(Constants.Common.Error, result.Message);
                return BadRequest(ModelState);
            }

            return this.Login(new LoginBindingModel
            {
                Username = model.Username,
                Password = model.Password
            });
        }

        [HttpPost]
        [Authorize]
        [Route("changePassword")]
        public IActionResult ChangePassword([FromBody]ChangePasswordBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ServiceResult result = this.account.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword);

            if (result.ResultType == ServiceResultType.Fail)
            {
                ModelState.AddModelError(Constants.Common.Error, result.Message);
                return BadRequest(ModelState);
            }

            return Ok(result);
        }
    }
}