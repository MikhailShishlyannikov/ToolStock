using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Sam.ToolStock.DataProvider.Models
{
    public class UserModel : IdentityUser
    {

        public virtual UserInfoModel UserInfo { get; set; }

        public string DepartmentId { get; set; }
        public virtual DepartmentModel Department { get; set; }

        public string StockId { get; set; }
        public virtual StockModel Stock { get; set; }

        public virtual ICollection<ToolModel> Tools { get; set; }

        public virtual ICollection<ToolLogModel> ToolLogs { get; set; }

        //public virtual Order Orders { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<UserModel> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            // Add custom user claims here
            return userIdentity;

        }
    }
}
