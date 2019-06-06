using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sam.ToolStock.Model.ViewModels
{
    public class UserInfoViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Name", ResourceType = typeof(Resources.Resource))]
        public string Name { get; set; }

        [Display(Name = "Patronymic", ResourceType = typeof(Resources.Resource))]
        public string Patronymic { get; set; }

        [Display(Name = "Surname", ResourceType = typeof(Resources.Resource))]
        public string Surname { get; set; }

        public string FullName => Patronymic == null ? $"{Name} {Surname}" : $"{Name} {Patronymic} {Surname}";

        [Display(Name = "Phone", ResourceType = typeof(Resources.Resource))]
        public string Phone { get; set; }

        [Display(Name = "Email", ResourceType = typeof(Resources.Resource))]
        public string Email { get; set; }

        [Display(Name = "Role", ResourceType = typeof(Resources.Resource))]
        public string Role { get; set; }

        [Display(Name = "Department", ResourceType = typeof(Resources.Resource))]
        public string DepartmentName { get; set; }

        [Display(Name = "Stock", ResourceType = typeof(Resources.Resource))]
        public string StockName { get; set; }
    }
}
