namespace User.Services.Infrastructure
{
    public static class Constants
    {
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
    }
}
