using GameBox.Core;
using GameBox.Core.Enums;
using GameBox.Services.Contracts;
using GameBox.Services.Models.View.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace GameBox.Api.Controllers
{
    [Authorize(Roles = Constants.Common.Admin)]
    public class UsersController : BaseApiController
    {
        private const string UsernameNotNull = "Username cannot be null.";
        private const string UsernameAndRoleMandatory = "User and Role names should have value!";

        private readonly IUserService user;

        public UsersController(IUserService user)
        {
            this.user = user;
        }

        [HttpGet]
        [Route("all")]
        public IEnumerable<ListUsersViewModel> All([FromQuery]string username)
        {
            return this.user.All(username);
        }

        [HttpGet]
        [Route("lock")]
        public IActionResult Lock([FromQuery]string username)
        {
            if (username == null)
            {
                ModelState.AddModelError(Constants.Common.Error, UsernameNotNull);
                return BadRequest(ModelState);
            }

            ServiceResult result = this.user.Lock(username);

            if (result.ResultType == ServiceResultType.Fail)
            {
                ModelState.AddModelError(Constants.Common.Error, result.Message);
                return BadRequest(ModelState);
            }

            return Ok(result.Message);
        }

        [HttpGet]
        [Route("unlock")]
        public IActionResult Unlock([FromQuery]string username)
        {
            if (username == null)
            {
                ModelState.AddModelError(Constants.Common.Error, UsernameNotNull);
                return BadRequest(ModelState);
            }

            ServiceResult result = this.user.Unlock(username);

            if (result.ResultType == ServiceResultType.Fail)
            {
                ModelState.AddModelError(Constants.Common.Error, result.Message);
                return BadRequest(ModelState);
            }

            return Ok(result.Message);
        }

        [HttpGet]
        [Route("addRole")]
        public IActionResult AddRole([FromQuery]string username, [FromQuery]string roleName)
        {
            if (username == null || roleName == null)
            {
                ModelState.AddModelError(Constants.Common.Error, UsernameAndRoleMandatory);
                return BadRequest(ModelState);
            }

            ServiceResult result = this.user.AddRole(username, roleName);

            if (result.ResultType == ServiceResultType.Fail)
            {
                ModelState.AddModelError(Constants.Common.Error, result.Message);
                return BadRequest(ModelState);
            }

            return Ok(result.Message);
        }

        [HttpGet]
        [Route("removeRole")]
        public IActionResult RemoveRole([FromQuery]string username, [FromQuery]string roleName)
        {
            if (username == null || roleName == null)
            {
                ModelState.AddModelError(Constants.Common.Error, UsernameAndRoleMandatory);
                return BadRequest(ModelState);
            }

            ServiceResult result = this.user.RemoveRole(username, roleName);

            if (result.ResultType == ServiceResultType.Fail)
            {
                ModelState.AddModelError(Constants.Common.Error, result.Message);
                return BadRequest(ModelState);
            }

            return Ok(result.Message);
        }
    }
}