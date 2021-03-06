﻿using System;

namespace SportsBetting.Core.Domain
{
    public class User
    {
        public Guid Id { get; protected set; }
        public string Email { get; protected set; }
        public string Username { get; protected set; }
        public string FullName { get; protected set; }
        public string Password { get; protected set; }
        public string Salt { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }

        protected User()
        {}

        public User (string email, string username, string password, string salt)
        {
            Id = Guid.NewGuid();
            Email = email.ToLowerInvariant();
            Username = username;
            Salt = salt;
            CreatedAt = DateTime.UtcNow;
        }

        public void SetUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new Exception("Your username is invalid!");
            Username = username;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new Exception("Your email is invalid!");
            Email = email.ToLowerInvariant();
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetPassword(string password)
        {
            if(string.IsNullOrWhiteSpace(password))
                throw new Exception("Your password is invalid!");
            Password = password;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetFullName(string fullname)
        {
            if (string.IsNullOrWhiteSpace(fullname))
                throw new Exception("Your FullName is invalid!");
            FullName = fullname;
            UpdatedAt = DateTime.UtcNow;
        }

        public static User Crate(string email, string username, string password, string salt)
            => new User(email,username, password, salt);
    }
}
