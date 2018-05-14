using System;
using Micro.Base.Exceptions;
using Micro.Services.Identity.Service;

namespace Micro.Services.Identity.Domain.Models
{
    public class User
    {
        public Guid Id { get; protected set; }
        public string Name { get; protected set; }
        public string Email { get; protected set; }
        public string Password { get; protected set; }
        public string Salt { get; protected set; }
        public DateTime CreatedAt { get; set; }

        protected User()
        {

        }
        public User(string email, string name)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new MicroException("empty_user_email",
               $"User email cnt be empty!");
            if (string.IsNullOrWhiteSpace(name))
                throw new MicroException("empty_user_name",
               $"User name cnt be empty!");
            Id = Guid.NewGuid();
            Email = email.ToLowerInvariant();
            Name = name;
            CreatedAt = DateTime.UtcNow;
        }

        public void AddPassword(string pswrd, IPswrdEncr encr)
        {
            if (string.IsNullOrEmpty(pswrd))
                throw new MicroException("empty_password", $"UserPaswword cnt be empty!");

            Salt = encr.GetSalt(pswrd);
            // Password = encr.GetHash(pswrd, encr.GetSalt(pswrd));
            Password = encr.GetHash(pswrd, Salt);
        }

        public bool IsPswrdCorrect(string pswr, IPswrdEncr encr)
        {
            return Password.Equals(encr.GetHash(pswr, Salt));
        }


    }
}