namespace User.Services.BindingModels.Auth
{
    public class RegisterBindingModel
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string RepeatPassword { get; set; }
    }
}
