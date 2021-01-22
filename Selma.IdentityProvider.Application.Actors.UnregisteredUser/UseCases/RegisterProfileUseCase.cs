using Selma.Core.Application;
using Selma.Core.Application.Abstractions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Selma.IdentityProvider.Application.Actors.UnregisteredUser.UseCases
{
    public class RegisterProfileUseCaseRequestDTO : UseCaseRequest<RegisterProfileUseCaseResponseDTO>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class RegisterProfileUseCaseResponseDTO
    {
        public Guid ProfileId { get; set; }
    }

    public class RegisterProfileUseCase : UseCase<RegisterProfileUseCaseRequestDTO, RegisterProfileUseCaseResponseDTO>
    {
        public override async Task<RegisterProfileUseCaseResponseDTO> Handle(RegisterProfileUseCaseRequestDTO request, CancellationToken cancellationToken = default)
        {
            RegisterProfileUseCaseResponseDTO registerProfileUseCaseResponseDTO = new RegisterProfileUseCaseResponseDTO
            {
                ProfileId = Guid.NewGuid()
            };

            return await Task.FromResult(registerProfileUseCaseResponseDTO);
        }
    }
}
