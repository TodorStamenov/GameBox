namespace GameBox.Application.Infrastructure
{
    public static class Constants
    {
        public static class Caching
        {
            public const int CategoryMenuItemsLifeSpan = 60;
            public const string CategoryMenuItemsKey = "Category_Menu_Items";
        }

        public static class Common
        {
            public const int TokenExpiration = 2;
            public const string UserIdClaimKey = "UserId";
            public const string SymmetricSecurityKey = "superSecretKey@345";

            public const string Admin = "Admin";

            public const string InvalidCredentials = "Invalid Credentials!";
            public const string Unauthorised = "Unauthorised!";

            public const string Success = "{0} {1} successfully {2}.";
            public const string DuplicateEntry = "{0} {1} already exists in database!";
            public const string NotExistingEntry = "{0} {1} is not existing in database!";

            public const string Error = "error";

            public const string Locked = "Locked";
            public const string Unlocked = "Unlocked";

            public const string Added = "Added";
            public const string Edited = "Edited";
            public const string Deleted = "Deleted";

            public const string NotValidDateFormat = "{0} is not valid date format!";
        }

        public static class User
        {
            public const int UsernameMinLength = 3;
            public const int UsernameMaxLength = 50;
            public const int PasswordMinLength = 3;
            public const int PasswordMaxLength = 50;
        }

        public static class Role
        {
            public const int NameMinLength = 3;
            public const int NameMaxLength = 50;
        }

        public static class Category
        {
            public const int NameMinLength = 3;
            public const int NameMaxLength = 30;
        }

        public static class Game
        {
            public const int TitleMinLength = 3;
            public const int TitleMaxLength = 100;
            public const decimal MinPrice = 0.01M;
            public const decimal MaxPrice = decimal.MaxValue;
            public const double MinSize = 0.01;
            public const double MaxSize = double.MaxValue;
            public const int MinVideoIdLength = 11;
            public const int MaxVideoIdLength = 11;
            public const int MinDescriptionLength = 20;
        }
    }
}