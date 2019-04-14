namespace Sam.ToolStock.Common.Constants
{
    public static class ConfigEntityFramework
    {
        //strings
        /// <summary>
        /// The name of the specific connection string used to connect to the database for the Entity Framework.
        /// </summary>
        public static readonly string ConnectionString = "ToolStock";
        /// <summary>
        /// The table name of the IdentityUserLogin in the database.
        /// </summary>
        public const string IdentityUserLoginTableName = "UserLogins";
        /// <summary>
        /// The table name of the DepartmentModel entity in the database.
        /// </summary>
        public const string DepartmentTableName = "Departments";
        /// <summary>
        /// The table name of the RoleModel entity in the database.
        /// </summary>
        public const string RoleTableName = "Roles";
        /// <summary>
        /// The table name of the StockModel entity in the database.
        /// </summary>
        public const string StockTableName = "Stocks";
        /// <summary>
        /// The table name of the ToolModel entity in the database.
        /// </summary>
        public const string ToolTableName = "Tools";
        /// <summary>
        /// The table name of the ToolLogModel entity in the database.
        /// </summary>
        public const string ToolLogTableName = "ToolLogs";
        /// <summary>
        /// The table name of the ToolTypeModel entity in the database.
        /// </summary>
        public const string ToolTypeTableName = "ToolTypes";
        /// <summary>
        /// The table name of the UserModel entity in the database.
        /// </summary>
        public const string UserTableName = "Users";
        /// <summary>
        /// The table name of the UserInfoModel entity in the database.
        /// </summary>
        public const string UserInfoTableName = "UserInfos";

        //numbers
        /// <summary>
        /// Max length of the name property of the DepartmentModel.
        /// </summary>
        public const int MaxLengthOfDepartmentName = 350;
        /// <summary>
        /// Max length of the name property of the ToolTypeModel.
        /// </summary>
        public const int MaxLengthOToolTypeName = 150;
        /// <summary>
        /// Max length of the name property of the StockModel.
        /// </summary>
        public const int MaxLengthOfStockName = 70;
        /// <summary>
        /// Max length of the name property of the ToolModel.
        /// </summary>
        public const int MaxLengthOfToolName = 60;
        /// <summary>
        /// Max length of the manufacturer property of the ToolModel.
        /// </summary>
        public const int MaxLengthOfToolManufacturer = 60;
        /// <summary>
        /// Max length of the name property of the UserInfoModel.
        /// </summary>
        public const int MaxLengthOfUserInfoName = 60;
        /// <summary>
        /// Max length of the Patronymic property of the UserInfoModel.
        /// </summary>
        public const int MaxLengthOfUserInfoPatronymic = 70;
        /// <summary>
        /// Max length of the Surname property of the UserInfoModel.
        /// </summary>
        public const int MaxLengthOfUserInfoSurname = 80;
        /// <summary>
        /// Max length of the Email property of the UserInfoModel.
        /// </summary>
        public const int MaxLengthOfUserInfoEmail = 100;
        /// <summary>
        /// Max length of the Phone property of the UserInfoModel.
        /// </summary>
        public const int MaxLengthOfUserInfoPhone = 16;
    }
}
