using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Sam.ToolStock.Logic.Interfaces;
using Sam.ToolStock.Model.ViewModels;

namespace Sam.ToolStock.Web.Areas.Keepers.Controllers
{
    [Authorize(Roles = "Stock keeper")]
    public class MenuController : Controller
    {
        private readonly IUserService _userService;
        private readonly IStockService _stockService;

        public MenuController(IUserService userService, IStockService stockService)
        {
            _userService = userService;
            _stockService = stockService;
        }

        // GET: Keepers/Menu
        public ActionResult Show()
        {
            var userId = User.Identity.GetUserId();
            var vm = new MenuViewModel {StockName = _stockService.Get(_userService.GetUser(userId).StockId).Name};

            return PartialView(vm);
        }
    }
}