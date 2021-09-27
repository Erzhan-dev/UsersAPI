using System.Text.RegularExpressions;

namespace UsersRESTAPI.Services
{
    public class DataValidation
    {
        static Regex rgxLin = new Regex(@"^[\d]{12}$");
        static Regex rgxName = new Regex(@"^([А-Я]{1}[а-яё]{1,23}|[A-Z]{1}[a-z]{1,23})$");

        public static bool IsName(string name)
        {
            return rgxName.IsMatch(name);
        }
        public static bool IsLin(string lin)
        {
            return rgxLin.IsMatch(lin);

        }
    }
}
