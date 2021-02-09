namespace Samples.ActorsUseCases.Domain.ProfileRoot
{
    public interface IProfileFactory
    {
        IProfile CreateProfile(string name, string email, Address address);
    }

    internal class ProfileFactory
        : IProfileFactory
    {
        public IProfile CreateProfile(string name, string email, Address address)
            => new Profile(name, email, address);
    }
}
