using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Selma.Core.Application.Actors.UnregisteredUser.UseCases
{
    public class RegisterProfileRequestDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class RegisterProfileResponseDTO
    {
        public Guid ProfileId { get; set; }
    }

    public class RegisterProfile : UseCase<RegisterProfileRequestDTO, RegisterProfileResponseDTO>
    {
        public override Task<RegisterProfileResponseDTO> Handle(RegisterProfileRequestDTO request, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
