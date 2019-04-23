using System;

namespace Sam.ToolStock.DataProvider.Models
{
    public class UserInfoModel
    {
        public string Id { get; set; }

        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public bool IsDeleted { get; set; }

        public virtual UserModel User { get; set; }
    }
}
