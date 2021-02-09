using Samples.ActorsUseCases.Domain.ProfileRoot;
using System;
using System.Collections.Generic;
using System.Text;

namespace Samples.ActorsUseCases.Domain
{
    internal class DomainContext
    {
        internal DomainContext()
        {
            Profiles = new List<IProfile>();
        }

        internal ICollection<IProfile> Profiles { get; private set; }
    }
}
