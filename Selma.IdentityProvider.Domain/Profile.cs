using Selma.Core.Domain;
using System;

namespace Selma.IdentityProvider.Domain
{
    public class Profile : EntityRoot
    {
        protected Profile()
            : this(string.Empty, string.Empty, string.Empty)
        { }

        public Profile(string name, string email, string password)
        {
            Name = name;
            Email = email;
            Password = password;
        }

        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public bool Activated { get; protected set; }

        public void Activate()
        {
            if (Activated)
            {
                throw new InvalidOperationException();
            }
            Activated = true;
        }

        public void Deativate()
        {
            if (!Activated)
            {
                throw new InvalidOperationException();
            }
            Activated = false;
        }
    }
}
