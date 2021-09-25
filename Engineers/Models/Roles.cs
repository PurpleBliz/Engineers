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

        public const string START = "Нулевой";
        public const string JUNIOR = "Начальный";
        public const string MIDDLE = "Средний";
        public const string SENJOR = "Опытный";
        public const string PROWIN = "Профессионал win";
        public const string PROUNIX = "Профессионал unix";

        private static readonly Dictionary<string, string> ListRoles = new()
        {
            { ADMIN_EN, ADMIN_RU },
            { CUSTOMER_EN, CUSTOMER_RU },
            { EXECUTOR_EN, EXECUTOR_RU }
        };
        private static readonly Dictionary<string, string> ListViewRoles = new()
        {
            { CUSTOMER_EN, CUSTOMER_RU },
            { EXECUTOR_EN, EXECUTOR_RU }
        };
        public static Dictionary<string, string> GetNames = ListRoles;
        public static Dictionary<string, string> GetNamesForView = ListViewRoles;

        private static readonly List<string> ListQualification = new()
        {
            START,
            JUNIOR,
            MIDDLE,
            SENJOR,
            PROWIN,
            PROUNIX
        };

        public static List<string> GetQualification = ListQualification;
    }
}