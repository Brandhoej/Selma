using Samples.ActorsUseCases.Domain.ProfileRoot;
using Selma.Core.Application;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;


namespace Samples.ActorsUseCases.Application.UseCases
{
    public class RegisterProfileUseCaseRequest : UseCaseRequest<RegisterProfileUseCaseResponse>
    {
        public RegisterProfileUseCaseRequest(string name, string email)
        {
            Name = name;
            Email = email;
        }

        private RegisterProfileUseCaseRequest()
        { }

        public string Name { get; private set; }
        public string Email { get; private set; }
    }

    public class RegisterProfileUseCaseResponse
    {
        public RegisterProfileUseCaseResponse(Guid profileId)
        {
            ProfileId = profileId;
        }

        private RegisterProfileUseCaseResponse()
        { }

        public Guid ProfileId { get; private set; }
    }

    public class RegisterProfileUseCase : UseCase<RegisterProfileUseCaseRequest, RegisterProfileUseCaseResponse>
    {
        private readonly ICollection<Profile> m_profiles;

        public RegisterProfileUseCase(ICollection<Profile> profiles)
        {
            m_profiles = profiles;
        }

        public override Task<RegisterProfileUseCaseResponse> Handle(RegisterProfileUseCaseRequest request, CancellationToken cancellationToken = default)
        {
            Profile profile = new Profile(request.Name, request.Email);
            m_profiles.Add(profile);
            return Task.FromResult(new RegisterProfileUseCaseResponse(profile.Id));
        }
    }
}
