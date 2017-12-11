using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VivoosVR.Web.App_Start
{
    public class CorePasswordHasher : IPasswordHasher
    {
        public string HashPassword(string password)
        {
            //return password.Encrypt();
            return password;
        }

        public PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword)
        {
            //bool matches = hashedPassword.Decrypt() == providedPassword;
            bool matches = hashedPassword == providedPassword;
            return matches ? PasswordVerificationResult.Success : PasswordVerificationResult.Failed;
        }
    }
}