using System.Web.Http;
using Sam.ToolStock.Logic.Interfaces;
using Sam.ToolStock.Model.ViewModels;

namespace Sam.ToolStock.Web.Controllers.ApiControllers
{
    [RoutePrefix("api/Info")]
    public class InfoController : ApiController
    {
        private readonly IUserService _userService;

        public InfoController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet, Route("{id}")]
        public UserInfoViewModel Show(string id)
        {
            var userInfo = _userService.GetUserInfo(id);

            return userInfo;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _userService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
