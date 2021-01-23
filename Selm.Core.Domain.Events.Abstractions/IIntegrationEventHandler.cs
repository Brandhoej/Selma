namespace Selma.Core.Domain.Events.Abstractions
{
    public interface IIntegrationEventHandler<T>
        : IDomainEventHandler<T>
        where T : IIntegrationEvent
    { }
}
