namespace User.Services.BindingModels.Auth;

public class ChangePasswordBindingModel
{
    public string Username { get; set; }

    public string OldPassword { get; set; }

    public string NewPassword { get; set; }

    public string RepeatPassword { get; set; }
}
