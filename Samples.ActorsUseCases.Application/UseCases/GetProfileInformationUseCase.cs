using Samples.ActorsUseCases.Domain.ProfileRoot;
using Selma.Core.Application;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Samples.ActorsUseCases.Application.UseCases
{
    public class GetProfileInformationUseCaseRequest : UseCaseRequest<GetProfileInformationUseCaseResponse>
    {
        public GetProfileInformationUseCaseRequest(Guid profileId)
        {
            ProfileId = profileId;
        }

        private GetProfileInformationUseCaseRequest()
        { }

        public Guid ProfileId { get; set; }
    }

    public class GetProfileInformationUseCaseResponse
    {
        public GetProfileInformationUseCaseResponse(string name, string email, bool activated)
        {
            Name = name;
            Email = email;
            Activated = activated;
        }

        private GetProfileInformationUseCaseResponse()
        { }

        public string Name { get; set; }
        public string Email { get; set; }
        public bool Activated { get; set; }
    }

    public class GetProfileInformationUseCase : UseCase<GetProfileInformationUseCaseRequest, GetProfileInformationUseCaseResponse>
    {
        private readonly ICollection<Profile> m_profiles;

        public GetProfileInformationUseCase(ICollection<Profile> profiles)
        {
            m_profiles = profiles;
        }

        public override Task<GetProfileInformationUseCaseResponse> Handle(GetProfileInformationUseCaseRequest request, CancellationToken cancellationToken = default)
        {
            Profile profile = m_profiles.First(curr => curr.Id == request.ProfileId);
            return Task.FromResult(new GetProfileInformationUseCaseResponse(profile.Name, profile.Email, profile.Activated));
        }
    }
}
