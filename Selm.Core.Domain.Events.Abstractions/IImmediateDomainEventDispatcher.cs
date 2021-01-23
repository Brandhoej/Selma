namespace Selma.Core.Domain.Events.Abstractions
{
    public interface IImmediateDomainEventDispatcher
        : IDomainEventDispatcher
        , IDomainEventQueuer
    { }
}
