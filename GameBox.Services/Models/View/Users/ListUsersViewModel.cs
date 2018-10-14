using System;

namespace GameBox.Services.Models.View.Users
{
    public class ListUsersViewModel
    {
        public Guid Id { get; set; }

        public string Username { get; set; }

        public bool IsLocked { get; set; }

        public bool IsAdmin { get; set; }
    }
}