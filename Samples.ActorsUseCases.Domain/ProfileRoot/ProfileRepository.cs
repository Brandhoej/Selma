using System;
using System.Collections.Generic;
using System.Linq;

namespace Samples.ActorsUseCases.Domain.ProfileRoot
{
    public interface IProfileRepository
    {
        int Count { get; }
        IProfile ReadProfileById(Guid profileId);
        IProfile Add(IProfile profile);
    }

    internal class ProfileRepository
        : IProfileRepository
    {
        internal ProfileRepository(ICollection<IProfile> profiles)
            => Profiles = profiles;

        public int Count => Profiles.Count;

        private ICollection<IProfile> Profiles { get; }

        public IProfile Add(IProfile profile)
        {
            try
            {
                Profiles.Add(profile);
                return profile;
            }
            catch (Exception)
            {
                return default;
            }
        }

        public IProfile ReadProfileById(Guid profileId)
            => Profiles.FirstOrDefault(profile => profile.Id == profileId);
    }
}
