using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Selma.Core.Application;
using Selma.Core.Domain.Events;
using Selma.Core.Domain.Events.Abstractions;
using Samples.ActorsUseCases.Domain.ProfileRoot;
using Samples.ActorsUseCases.Application.UseCases;
using Samples.ActorsUseCases.Application;
using MediatR;

namespace Samples.ActorsUseCases.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            ICollection<Profile> profiles = new List<Profile>();

            ServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
            serviceCollection.AddScoped(provider => profiles);
            serviceCollection.AddActor<User>();
            serviceCollection.AddDomainEventQueue();
            serviceCollection.AddDeferredDomainEventDispatcher();

            ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
            IDeferredDomainEventDispatcher deferredDomainEventDispatcher = serviceProvider.GetService<IDeferredDomainEventDispatcher>();
            DomainEvent.Queuer = serviceProvider.GetService<IDomainEventQueuer>();

            UserTest(serviceProvider.GetService<User>()).Wait();
            deferredDomainEventDispatcher.DispatchAll().Wait();

            Console.Read();
        }

        public static async Task UserTest(User user)
        {
            RegisterProfileUseCaseRequest registerProfileUseCaseRequest = new RegisterProfileUseCaseRequest("Andreas K. Brandhøj", "andreasbrandhoej@hotmail.com");
            RegisterProfileUseCaseResponse registerProfileUseCaseResponse = await user.Do(registerProfileUseCaseRequest);

            ActivateProfileUseCaseRequest activateProfileUseCaseRequest = new ActivateProfileUseCaseRequest(registerProfileUseCaseResponse.ProfileId);
            await user.Do(activateProfileUseCaseRequest);

            GetProfileInformationUseCaseRequest getProfileInformationUseCaseRequest = new GetProfileInformationUseCaseRequest(registerProfileUseCaseResponse.ProfileId);
            GetProfileInformationUseCaseResponse getProfileInformationUseCaseResponse = await user.Do(getProfileInformationUseCaseRequest);

            Console.WriteLine($"{nameof(registerProfileUseCaseResponse.ProfileId)} = {registerProfileUseCaseResponse.ProfileId}");
            Console.WriteLine($"{nameof(getProfileInformationUseCaseResponse.Name)} = {getProfileInformationUseCaseResponse.Name}");
            Console.WriteLine($"{nameof(getProfileInformationUseCaseResponse.Email)} = {getProfileInformationUseCaseResponse.Email}");
            Console.WriteLine($"{nameof(getProfileInformationUseCaseResponse.Activated)} = {getProfileInformationUseCaseResponse.Activated}");
        }
    }
}
