using Samples.ActorsUseCases.Domain.ProfileRoot;
using Selma.Core.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Samples.ActorsUseCases.Application.UseCases
{
    public class ActivateProfileUseCaseRequest : UseCaseRequest<ActivateProfileUseCaseResponse>
    {
        public ActivateProfileUseCaseRequest(Guid profileId)
        {
            ProfileId = profileId;
        }

        private ActivateProfileUseCaseRequest()
        { }

        public Guid ProfileId { get; set; }
    }

    public class ActivateProfileUseCaseResponse
    { }

    public class ActivateProfileUseCase : UseCase<ActivateProfileUseCaseRequest, ActivateProfileUseCaseResponse>
    {
        private readonly ICollection<Profile> m_profiles;

        public ActivateProfileUseCase(ICollection<Profile> profiles)
        {
            m_profiles = profiles;
        }

        public override Task<ActivateProfileUseCaseResponse> Handle(ActivateProfileUseCaseRequest request, CancellationToken cancellationToken = default)
        {
            Profile profile = m_profiles.First(curr => curr.Id == request.ProfileId);
            profile.Activate();
            return Task.FromResult(new ActivateProfileUseCaseResponse());
        }
    }
}
