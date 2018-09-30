namespace GameBox.Core
{
    public static class Constants
    {
        public static class Common
        {
            public const string SymmetricSecurityKey = "superSecretKey@345";

            public const string Admin = "Admin";

            public const string InvalidCredentials = "Invalid Username or Password!";

            public const string Success = "{0} {1} successfully {2}.";
            public const string DuplicateEntry = "{0} {1} already exists in database!";
            public const string NotExistingEntry = "{0} {1} is not existing in database!";

            public const string Error = "error";

            public const string Locked = "Locked";
            public const string Unlocked = "Unlocked";
        }

        public static class UserConstants
        {
            public const int UsernameMinLength = 3;
            public const int UsernameMaxLength = 50;
            public const int PasswordMinLength = 3;
            public const int PasswordMaxLength = 50;
        }

        public static class RoleConstants
        {
            public const int NameMinLength = 3;
            public const int NameMaxLength = 50;
        }
    }
}