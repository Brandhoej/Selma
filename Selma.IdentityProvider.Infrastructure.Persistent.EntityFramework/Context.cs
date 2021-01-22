using Microsoft.EntityFrameworkCore;
using Selma.IdentityProvider.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Selma.IdentityProvider.Infrastructure.Persistent.EntityFramework
{
    public class Context : Core.Infrastructure.Persistent.EntityFramework.Context
    {
        private DbSet<Profile> Profiles { get; set; }
    }
}
