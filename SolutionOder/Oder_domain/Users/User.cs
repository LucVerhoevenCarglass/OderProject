using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;

namespace Order.Domain.Users
{
    public class User
    {
        public enum Roles
        {
            Customer,
            Admin,
        }

        public string UserId { get; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Roles UserRole { get; set; }

        private const string ErrorMessage = "UserDomain : ";
        private const int MIN_PASSWORD_LENGTH = 8;
        public User(string userid = null)
        {
            if (string.IsNullOrEmpty(userid))
            {
                UserId = Guid.NewGuid().ToString();
            }
            else UserId = userid;


            UserRole = Roles.Customer;
        }

        public void CheckUserValues()
        {
            CheckEmail(Email);
            CheckPassWord(Password);
        }

        private void CheckEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new OrderExeptions($" {ErrorMessage} Email is required");
            }

            if (!IsEmailValid(email))
            {
                throw new OrderExeptions($" {ErrorMessage} not a correct Email-format");
            }
        }

        private bool IsEmailValid(string email)
        {
            try
            {
                MailAddress m = new MailAddress(email);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        private void CheckPassWord(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new OrderExeptions($" {ErrorMessage} Password is required");
            }
            if (password.Length < MIN_PASSWORD_LENGTH)
            {
                throw new OrderExeptions($" {ErrorMessage} Password must contain at least {MIN_PASSWORD_LENGTH} characters");
            }
            if (!Regex.Match(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[A-Za-z\d]").Success)
            {
                throw new OrderExeptions($" {ErrorMessage} The password is not valid. It should contain at least one uppercase character, one lowercase character and one digit");
            }
         }
    }
}
