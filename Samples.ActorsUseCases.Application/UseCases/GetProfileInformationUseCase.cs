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

        public Guid ProfileId { get; }
    }

    public class GetProfileInformationUseCaseResponse
    {
        public GetProfileInformationUseCaseResponse(IProfile profile)
        {
            Name = profile.Name;
            Email = profile.Email;
            Activated = profile.Activated;
            Address = profile.Address;
        }

        private GetProfileInformationUseCaseResponse()
        { }

        public string Name { get; }
        public string Email { get; }
        public bool Activated { get; }
        public Address Address { get; }
    }

    public class GetProfileInformationUseCase : UseCase<GetProfileInformationUseCaseRequest, GetProfileInformationUseCaseResponse>
    {
        private readonly IProfileRepository m_profileRepository;

        public GetProfileInformationUseCase(IProfileRepository profileRepository)
        {
            m_profileRepository = profileRepository;
        }

        public override Task<GetProfileInformationUseCaseResponse> Handle(GetProfileInformationUseCaseRequest request, CancellationToken cancellationToken = default)
        {
            IProfile profile = m_profileRepository.ReadProfileById(request.ProfileId);
            return Task.FromResult(new GetProfileInformationUseCaseResponse(profile));
        }
    }
}
