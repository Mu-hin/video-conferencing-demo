﻿namespace VideoConferencingDemo.Infrastructure.BusinessObjects
{
    public class SignIn
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}