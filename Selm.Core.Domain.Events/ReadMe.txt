Dependencies:
Private:
 - Microsoft.Extensions.DependencyInjection.Abstractions (5.0.0)
Public:
 - MediatR (9.0.0)
 - Selma.Core.MessageQueue.MediatR
 - Selma.Core.Domain.Abstractions 
 - Selma.Core.Domain.Events.Abstractions 
 - Selma.Core.MessageQueue.Abstractions 

Public API
 - Public interfaces:
   - IDomainEvent: Enqueue
   - IDomainEventHandler: Handle
- 3rd party interfaces
   - INotification:
   - INotificationHandler: Handle
 - Public classes:
   - Abstract class DomainEvent is IDomainEvent, INotification
   - Abstract class DomainEventHandler is IDomainEventHandler, INotificationHandler
   - static class ServiceCollectionExtensions: AddDeferredDomainEventMessageQueue, AddDeferredDomainEventMessageQueue, 
		AddImmediateDomainEventMessageQueue, AddImmediateDomainEventMessageQueue

Features
- DomainEvent
   - Domain specific event capable of working with the Core MessageQueue
   - Easy Enqueue through the interface by default the value is the static producer instance
- DomainEventHandler
   - Can handle both immediate and deferred event dispatching and enqueueing
- ServiceCollectionExtensions
   - Ensures that the service collection contains the correct dependencies