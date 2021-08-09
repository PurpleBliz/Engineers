using System.Collections.Generic;

namespace Engineers.Models
{
    public static class Roles
    {
        public const string ADMIN_EN = "admin";
        public const string CUSTOMER_EN = "customer";
        public const string EXECUTOR_EN = "executor";

        public const string ADMIN_RU = "Администратор";
        public const string CUSTOMER_RU = "Заказчик";
        public const string EXECUTOR_RU = "Исполнитель";

        private static readonly Dictionary<string, string> ListRoles = new()
        {
            { "admin", "Администратор" },
            { "customer", "Заказчик" },
            { "executor", "Исполнитель" }
        };
        private static readonly Dictionary<string, string> ListViewRoles = new()
        {
            { "customer", "Заказчик" },
            { "executor", "Исполнитель" }
        };
        public static Dictionary<string, string> GetNames = ListRoles;
        public static Dictionary<string, string> GetNamesForView = ListViewRoles;
    }
}