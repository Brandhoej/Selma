using Samples.ActorsUseCases.Domain.ProfileRoot;
using Selma.Core.Application;
using System;
using System.Threading;
using System.Threading.Tasks;


namespace Samples.ActorsUseCases.Application.UseCases
{
    public class RegisterProfileUseCaseRequest : UseCaseRequest<RegisterProfileUseCaseResponse>
    {
        public RegisterProfileUseCaseRequest(string name, string email, Address address)
        {
            Name = name;
            Email = email;
            Address = address;
        }

        private RegisterProfileUseCaseRequest()
        { }

        public string Name { get; }
        public string Email { get;  }
        public Address Address { get; }
    }

    public class RegisterProfileUseCaseResponse
    {
        public RegisterProfileUseCaseResponse(Guid profileId)
        {
            ProfileId = profileId;
        }

        private RegisterProfileUseCaseResponse()
        { }

        public Guid ProfileId { get; }
    }

    public class RegisterProfileUseCase : UseCase<RegisterProfileUseCaseRequest, RegisterProfileUseCaseResponse>
    {
        private readonly IProfileRepository m_profileRepository;
        private readonly IProfileFactory m_profileFactory;

        public RegisterProfileUseCase(IProfileRepository profileRepository, IProfileFactory profileFactory)
        {
            m_profileRepository = profileRepository;
            m_profileFactory = profileFactory;
        }

        public override Task<RegisterProfileUseCaseResponse> Handle(RegisterProfileUseCaseRequest request, CancellationToken cancellationToken = default)
        {
            IProfile profile = m_profileFactory.CreateProfile(request.Name, request.Email, request.Address);
            m_profileRepository.Add(profile);
            return Task.FromResult(new RegisterProfileUseCaseResponse(profile.Id));
        }
    }
}
