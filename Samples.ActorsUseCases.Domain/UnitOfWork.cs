using Samples.ActorsUseCases.Domain.ProfileRoot;

namespace Samples.ActorsUseCases.Domain
{
    internal class UnitOfWork
    {
        internal UnitOfWork(DomainContext context)
        {
            ProfileRepository = new ProfileRepository(context.Profiles);
        }

        internal IProfileRepository ProfileRepository { get; }
    }
}
