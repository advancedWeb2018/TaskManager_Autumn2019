﻿namespace MakeIt.BLL.DTO
{
    public class UserAuthDTO
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
