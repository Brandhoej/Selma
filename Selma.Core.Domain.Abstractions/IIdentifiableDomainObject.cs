namespace Selma.Core.Domain.Abstractions
{
    public interface IIdentifiableDomainObject<TId> 
        : IDomainObject
    {
        TId Id { get; }
    }
}
