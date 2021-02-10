using System;
using System.Threading.Tasks;
using Selma.Core.Application;
using Selma.Core.Domain.Events;
using Selma.Core.Domain.Events.Abstractions;
using Selma.Core.MessageQueue.Abstractions;
using Samples.ActorsUseCases.Domain.ProfileRoot;
using Samples.ActorsUseCases.Application.UseCases;
using Samples.ActorsUseCases.Application;
using Microsoft.Extensions.DependencyInjection;
using Samples.ActorsUseCases.Domain;
using Selma.Core.FSM;
using System.Collections.Generic;
using Selma.Core.FSM.Abstractions;

namespace Samples.ActorsUseCases.CLI
{
    class Program
    {
        private enum State { S1, S2 };
        private enum Alphabeth { A1, A2, A3 };

        static void Main(string[] args)
        {
            // If false then immediate dispatcher is used
            const bool deferredEventDispatcher = true;

            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddUnitOfWork();
            serviceCollection.AddActor<User>();

            if (deferredEventDispatcher)
            {
#pragma warning disable CS0162 // Unreachable code detected
                serviceCollection.AddDeferredDomainEventMessageQueue();
#pragma warning restore CS0162 // Unreachable code detected
            }
            else
            {
#pragma warning disable CS0162 // Unreachable code detected
                serviceCollection.AddImmediateDomainEventMessageQueue();
#pragma warning restore CS0162 // Unreachable code detected
            }

            ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
            DomainEvent.Producer = serviceProvider.GetService<IMessageQueueProducer<IDomainEvent>>();

            User user = serviceProvider.GetService<User>();
            UserActorSample(user).Wait();

            if (deferredEventDispatcher)
            {
#pragma warning disable CS0162 // Unreachable code detected
                IDeferredMessageQueue<IDomainEvent> deferredDomainEventDispatcher = serviceProvider.GetService<IDeferredMessageQueue<IDomainEvent>>();
                deferredDomainEventDispatcher.Dispatch().Wait();
#pragma warning restore CS0162 // Unreachable code detected
            }

            Console.WriteLine($"There is {serviceProvider.GetService<IProfileRepository>().Count} profile(s)");
            Console.Read();
        }

        public static async Task UserActorSample(User user)
        {
            Address addressOfUser = new Address("Langagervej", 4);
            RegisterProfileUseCaseRequest registerProfileUseCaseRequest = new RegisterProfileUseCaseRequest("Andreas K. Brandhøj", "andreasbrandhoej@hotmail.com", addressOfUser);
            RegisterProfileUseCaseResponse registerProfileUseCaseResponse = await user.Do(registerProfileUseCaseRequest);

            Console.WriteLine($"{nameof(registerProfileUseCaseResponse.ProfileId)} = {registerProfileUseCaseResponse.ProfileId}");

            ActivateProfileUseCaseRequest activateProfileUseCaseRequest = new ActivateProfileUseCaseRequest(registerProfileUseCaseResponse.ProfileId);
            await user.Do(activateProfileUseCaseRequest);

            GetProfileInformationUseCaseRequest getProfileInformationUseCaseRequest = new GetProfileInformationUseCaseRequest(registerProfileUseCaseResponse.ProfileId);
            GetProfileInformationUseCaseResponse getProfileInformationUseCaseResponse = await user.Do(getProfileInformationUseCaseRequest);

            Console.WriteLine($"{nameof(getProfileInformationUseCaseResponse.Name)} = {getProfileInformationUseCaseResponse.Name}");
            Console.WriteLine($"{nameof(getProfileInformationUseCaseResponse.Email)} = {getProfileInformationUseCaseResponse.Email}");
            Console.WriteLine($"{nameof(getProfileInformationUseCaseResponse.Address)} = {getProfileInformationUseCaseResponse.Address}");
            Console.WriteLine($"{nameof(getProfileInformationUseCaseResponse.Activated)} = {getProfileInformationUseCaseResponse.Activated}");
        }
    }
}
