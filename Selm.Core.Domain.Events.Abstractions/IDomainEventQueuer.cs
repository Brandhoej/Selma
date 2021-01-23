namespace Selma.Core.Domain.Events.Abstractions
{
    public interface IDomainEventQueuer
    {
        void Enqueue(IDomainEvent domainEvent);
    }
}
