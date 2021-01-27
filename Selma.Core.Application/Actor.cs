using MediatR;
using Selma.Core.Application.Abstractions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Selma.Core.Application
{
    /// <summary>
    ///     Represents an <see cref="Actor"/> in a domain. 
    ///     The <see cref="Actor"/> can define supported use cases based on <see cref="Type"/> and can look through a whole <see cref="Assembly"/> to search for all <see cref="IUseCase{IUseCaseRequest{TResponse}, TResponse}"/>.
    ///     To <see cref="Do{TResponse}(IUseCaseRequest{TResponse}, CancellationToken)"/> a use case the <see cref="Actor"/> uses a <see cref="IMediator"/> to send the <see cref="IUseCaseRequest{TResponse}"/> which then is recevied by a <see cref="IUseCase{IUseCaseRequest{TResponse}, TResponse}"/>.
    /// </summary>
    /// <example>
    ///     One can assume that the <see cref="Actor"/>: "UnregisterUser", supports a <see cref="IUseCase{TRequest, TResponse}"/> for user registration.
    /// </example>
    /// <example>
    ///     An <see cref="Actor"/> called User which supports both a specific <see cref="Type"/> and <see cref="Assembly"/>.
    ///     <code>
    ///         public class User : <see cref="Actor"/>
    ///         {
    ///             public User(<see cref="IMediator"/> mediator)
    ///                 : base(default, mediator)
    ///             { }
    ///             
    ///             protected override <see cref="IEnumerable{Assembly}"/> GetSupportedAssemblies()
    ///             {
    ///                 yield return <see cref="object.GetType"/>.<see cref="Assembly"/>;
    ///             }
    ///             
    ///             protected override <see cref="IEnumerable{Type}"/> GetSupportedUseCases()
    ///             {
    ///                 yield return <see cref="Type"/>;
    ///             }
    ///         }
    ///     </code>
    /// </example>
    public abstract class Actor
        : IActor
    {
        /// <summary>
        ///     Initializes an <see cref="Actor"/> without an <see cref="IActor"/> as a <see cref="Successor"/>.
        /// </summary>
        /// <param name="mediator">
        ///     The <see cref="IMediator"/> used to <see cref="ISender.Send{TResponse}(IRequest{TResponse}, CancellationToken)"/> the different <see cref="IUseCaseRequest{TResponse}"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     The use case enumerable is null.
        /// </exception>
        public Actor(IMediator mediator)
            : this(default, mediator)
        { }

        /// <summary>
        ///     Initializes an <see cref="Actor"/> without an <see cref="IActor"/> as a <see cref="Successor"/>.
        /// </summary>
        /// <param name="successor">The <see cref="IActor"/>
        ///     Used as the next link in the chain of responsibility. 
        ///     If the <see cref="IUseCaseRequest{TResponse}"/> is not supported by the <see cref="IActor"/>
        ///     then the <see cref="Successor"/> will attempt to <see cref="IActor.Do{TResponse}(IUseCaseRequest{TResponse}, CancellationToken)"/> the <see cref="IUseCaseRequest{TResponse}"/>
        /// </param>
        /// <param name="mediator">
        ///     The <see cref="IMediator"/> used to <see cref="ISender.Send{TResponse}(IRequest{TResponse}, CancellationToken)"/> the different <see cref="IUseCaseRequest{TResponse}"/>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     The use case enumerable is null.
        /// </exception>
        public Actor(IActor successor, IMediator mediator)
        {
            Successor = successor;
            Mediator = mediator;

            SupportedUseCaseCacheHits = new Dictionary<Type, bool>();
            SupportedUseCases = new HashSet<Type>(GetAllUseCases() ?? throw new ArgumentNullException());
        }

        /// <summary>
        ///     Used as the next link in the chain of responsibility. 
        ///     If the <see cref="IUseCaseRequest{TResponse}"/> is not supported by the <see cref="IActor"/>
        ///     then the <see cref="Successor"/> will attempt to <see cref="IActor.Do{TResponse}(IUseCaseRequest{TResponse}, CancellationToken)"/> 
        ///     the <see cref="IUseCaseRequest{TResponse}"/>.
        /// </summary>
        public IActor Successor { get; }

        /// <summary>
        ///     The <see cref="IMediator"/> used to mediate the <see cref="IUseCaseRequest{TResponse}"/> to the correct <see cref="IUseCase{TRequest, TResponse}"/>.
        /// </summary>
        private IMediator Mediator { get; }

        /// <summary>
        ///     The <see cref="ICollection{Type}"/> contains types which are all 
        ///     concretion assignable to <see cref="IUseCase{TRequest, TResponse}"/>.
        /// </summary>
        private ICollection<Type> SupportedUseCases { get; }

        /// <summary>
        ///     Stores results from <see cref="SupportsUseCase{TResponse}"/>.
        /// </summary>
        private IDictionary<Type, bool> SupportedUseCaseCacheHits { get; }

        public int Count => (this as IEnumerable<IActor>).Count();

        /// <summary>
        ///     Applies a use case to the <see cref="Actor"/> through a <see cref="IUseCaseRequest{TResponse}"/>.
        ///     If the <see cref="IUseCaseRequest{TResponse}"/> is not supported by the <see cref="IActor"/>
        ///     then the <see cref="Successor"/> will attempt to <see cref="IActor.Do{TResponse}(IUseCaseRequest{TResponse}, CancellationToken)"/> 
        ///     the <see cref="IUseCaseRequest{TResponse}"/>.
        /// </summary>
        /// <typeparam name="TResponse">
        ///     The type for the response of the <see cref="IUseCaseRequest{TResponse}"/>.
        /// </typeparam>
        /// <param name="request">
        ///     The <see cref="IUseCaseRequest{TResponse}"/> to do by the <see cref="Actor"/> or its <see cref="Successor"/>.
        /// </param>
        /// <param name="cancellationToken">
        ///     Optional cancellation token for the <see cref="IUseCase{TRequest, TResponse}"/> operation.
        /// </param>
        /// <returns>
        ///     The value type wrapper around the <see cref="TResponse"/>. 
        ///     The result type of which is <typeparamref name="TResponse"/>.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        ///     Mediator has no receiver for the <see cref="IUseCaseRequest{TResponse}"/>.
        /// </exception>
        public async ValueTask<TResponse> Do<TResponse>(IUseCaseRequest<TResponse> request, CancellationToken cancellationToken = default)
            where TResponse
            : class
        {
            if (SupportsUseCase<TResponse>())
            {
                return (TResponse)await Mediator.Send(request, cancellationToken);
            }
            else
            {
                /// Since the <see cref="IUseCase{IUseCaseRequest{TResponse}, TResponse}"/> was not supported the <see cref="Successor"/>
                if (Successor != null)
                {
                    return await Successor.Do(request, cancellationToken);
                }
            }

            throw new InvalidOperationException($"{GetType().Name} does not support request {request.GetType().Name}");
        }

        /// <summary>
        ///     Checks whether the <see cref="Actor"/> supports the <see cref="IUseCase{IUseCaseRequest{TResponse}, TResponse}"/> with the response of type <typeparamref name="TResponse"/>.
        /// </summary>
        /// <typeparam name="TResponse">
        ///     The type to check for a supported <see cref="IUseCase{TRequest, TResponse}"/>.
        /// </typeparam>
        /// <returns>
        ///     True if the <see cref="Actor"/> supports the <see cref="IUseCase{TRequest, TResponse}"/> with the return type <typeparamref name="TResponse"/>; otherwise, false.
        /// </returns>
        public bool SupportsUseCase<TResponse>()
            where TResponse
            : class
        {
            Type responseUseCaseRequestType = typeof(IUseCaseRequest<TResponse>);

            /// Check the cache <see cref="SupportedUseCaseCacheHits"/> first to save performance on type checking.
            if (!SupportedUseCaseCacheHits.TryGetValue(responseUseCaseRequestType, out bool isSupported))
            {
                isSupported = SupportedUseCases.Any(useCase => IsTypeASupportedUseCase<TResponse>(useCase));
                SupportedUseCaseCacheHits.Add(responseUseCaseRequestType, isSupported);
            }

            return isSupported;
        }

        public override bool Equals(object obj)
            => new ActorEqualityComparer().Equals(this, obj);

        public bool Equals(IActor other)
            => new ActorEqualityComparer().Equals(this, other);

        public IEnumerator<IActor> GetEnumerator()
            => new ActorEnumerator(this);

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        public int CompareTo(object obj)
            => new ActorComparer().Compare(this, obj);

        public int CompareTo(IActor other)
            => new ActorComparer().Compare(this, other);

        public override int GetHashCode()
            => new ActorEqualityComparer().GetHashCode(this);

        public override string ToString()
            => base.ToString();

        /// <summary>
        ///     Enables other <see cref="Actor"/> specialization classes to define which <see cref="Enumerable.Empty{Assembly}"/> 
        ///     with <see cref="IUseCase{TRequest, TResponse}"/> the <see cref="Actor"/> supports.
        /// </summary>
        /// <returns>
        ///     By default <see cref="Enumerable.Empty{Assembly}"/>.
        ///     It is possible to override this method to return specified <see cref="Assembly"/>.
        /// </returns>
        public virtual IEnumerable<Assembly> GetSupportedAssemblies()
            => Enumerable.Empty<Assembly>();

        /// <summary>
        ///     Enables other <see cref="Actor"/> specialization classes to define which <see cref="Enumerable.Empty{Type}"/> 
        ///     with <see cref="IUseCase{TRequest, TResponse}"/> the <see cref="Actor"/> supports.
        /// </summary>
        /// <returns>
        ///     By default <see cref="Enumerable.Empty{Type}"/>.
        ///     It is possible to override this method to return specified <see cref="Type"/>.
        /// </returns>
        public virtual IEnumerable<Type> GetSupportedUseCases()
            => Enumerable.Empty<Type>();

        /// <summary>
        ///     Checks whether the response of the <paramref name="useCase"/> is <typeparamref name="TResponse"/>.
        /// </summary>
        /// <typeparam name="TResponse">
        ///     The return type to check the <paramref name="useCase"/> for.
        /// </typeparam>
        /// <param name="useCase">
        ///     The <see cref="IUseCase{TRequest, TResponse}"/> to match response type with <typeparamref name="TResponse"/>.
        /// </param>
        /// <returns>
        ///     True if the response of the <paramref name="useCase"/> is <typeparamref name="TResponse"/>; otherwise, false.
        /// </returns>
        private bool IsTypeASupportedUseCase<TResponse>(Type useCase)
            where TResponse
            : class
        {
            try
            {
                Type responseUseCaseRequestType = typeof(IUseCaseRequest<TResponse>);

                /// Find the implemented interface <see cref="IUseCase{TRequest, TResponse}"/>
                Type useCaseType = useCase.GetInterfaces()
                    .First(@interface => @interface.IsConcretionAssignableTo(typeof(IUseCase<,>)));

                /// Find the concrete request type <see cref="IUseCaseRequest{TResponse}"/>
                Type concreteRequestType = useCaseType.GetGenericArguments()
                    .First(arg => arg.GetInterfaces()
                        .First(@interface => @interface.IsConcretionAssignableTo(typeof(IUseCaseRequest<>))) != null);

                /// Find the implemented interface <see cref="IUseCaseRequest{TResponse}"/>
                Type useCaseRequestInterfaceType = concreteRequestType.GetInterfaces()
                    .First(@interface => @interface.IsConcretionAssignableTo(typeof(IUseCaseRequest<>)));

                return useCaseRequestInterfaceType == responseUseCaseRequestType;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        ///     Retrieves all the supported <see cref="IUseCase{TRequest, TResponse}"/> by the <see cref="Actor"/>.
        /// </summary>
        /// <returns>
        ///     An <see cref="IEnumerable{Type}"/> containing all <see cref="IUseCase{TRequest, TResponse}"/> from <see cref="GetSupportedAssemblies"/> and <see cref="GetSupportedUseCases"/>.
        /// </returns>
        /// <exception cref="NullReferenceException">
        ///     An <see cref="Assembly"/> or <see cref="Type"/> from <see cref="GetSupportedAssemblies"/> 
        ///     and <see cref="GetSupportedUseCases"/> resprectivly contains a <see cref="Null"/> element.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     An <see cref="Assembly"/> from <see cref="GetSupportedAssemblies"/> contains no concretions of type <see cref="IUseCase{TRequest, TResponse}"/> or
        ///     a <see cref="Type"/> from <see cref="GetSupportedUseCases"/> is not a concretion of type <see cref="IUseCase{TRequest, TResponse}"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     A duplicate <see cref="IUseCase{TRequest, TResponse}"/> was defined.
        /// </exception>
        /// <exception cref="AggregateException">
        ///     One or more <see cref="Exception"/> were found when retrieving all supported <see cref="IUseCase{TRequest, TResponse}"/> for the <see cref="Actor"/>
        /// </exception>
        private IEnumerable<Type> GetAllUseCases()
        {
            ICollection<Exception> exceptions = new List<Exception>();

            /// Get <see cref="IEnumerable{Type}"/> from the <see cref="IEnumerable{Assembly}"/>.
            /// If none were found in the <see cref="Assembly"/> then we assume that the <see cref="Assembly"/> is incorrectly chosen.
            IEnumerable<Type> supportedAssemblyTypes = GetSupportedAssemblies()
                .SelectMany(assembly =>
                {
                    if (assembly == null)
                    {
                        exceptions.Add(new NullReferenceException($"A use case assembly was a null reference"));
                    }
                    else
                    {
                        /// Retrieve all <see cref="IEnumerable{Type}"/> from the <see cref="Assembly"/> which 
                        /// are assignable to <see cref="IUseCase{TRequest, TResponse}"/> to filter all unwanted <see cref="IEnumerable{Type}"/>
                        IEnumerable<Type> types = assembly.GetTypes()
                            .Where(type => type.IsConcretionAssignableTo(typeof(IUseCase<,>)));

                        if (types.Count() == 0)
                        {
                            exceptions.Add(new InvalidOperationException($"{assembly.FullName} does have any types of {typeof(IUseCase<,>)}"));
                        }

                        return types;
                    }

                    /// <see cref="Enumerable.Empty{Type}"/> the <see cref="Assembly"/> was null
                    return Enumerable.Empty<Type>();
                });

            /// Validate <see cref="IEnumerable{Type}"/> defined to check if they implement <see cref="IUseCase{TRequest, TResponse}"/>.
            /// We assume that it is an error/exception if a <see cref="Type"/> does not implement <see cref="IUseCase{TRequest, TResponse}"/>.
            IEnumerable<Type> supportedUseCaseTypes = GetSupportedUseCases()
                .Where(type =>
                {
                    if (type == null)
                    {
                        exceptions.Add(new NullReferenceException($"A use case type was a null reference"));
                    }
                    else
                    {
                        bool isUseCase = type.IsConcretionAssignableTo(typeof(IUseCase<,>));
                        if (!isUseCase)
                        {
                            exceptions.Add(new InvalidOperationException($"{type.Name} does not implement {typeof(IUseCase<,>)}"));
                        }
                        return isUseCase;
                    }

                    /// False because the type was null
                    return false;
                });

            /// We do not want to have duplicate <see cref="IUseCase{TRequest, TResponse}"/> therefore we expect it to 
            /// be an error/exception if the same <see cref="IUseCase{TRequest, TResponse}"/> is defined multiple times.
            IEnumerable<Type> supportedUseCases = supportedAssemblyTypes.Concat(supportedUseCaseTypes);
            IEnumerable<Type> distinctUseCases = supportedUseCases.Distinct();
            if (distinctUseCases.Count() != supportedUseCases.Count())
            {
                IEnumerable<Type> duplicateUseCases = supportedUseCases
                    .GroupBy(x => x)
                    .Where(g => g.Count() > 1)
                    .Select(y => y.Key);

                foreach (Type duplicateUseCase in duplicateUseCases)
                {
                    exceptions.Add(new ArgumentException($"{duplicateUseCase.Name} was defined multiple times"));
                }
            }

            /// Aggregate the <see cref="exceptions"/> in a <see cref="AggregateException"/> if there are more than one.
            /// This ensures maximum information output in the case of possible errors.
            if (exceptions.Count == 1)
            {
                throw exceptions.First();
            }
            else if (exceptions.Count > 1)
            {
                throw new AggregateException($"One or more exceptions were found when retrieving all supported use cases for the actor {GetType().Name}", exceptions);
            }

            return supportedUseCases;
        }

        public static bool operator !=(Actor left, Actor right)
            => !(left == right);

        public static bool operator ==(Actor left, Actor right)
        {
            if (ReferenceEquals(left, right))
            {
                return true;
            }

            if (left is null)
            {
                return false;
            }

            if (left is null)
            {
                return false;
            }

            return left.Equals(right);
        }

        public static bool operator >=(Actor left, Actor right)
            => left == right || left > right;

        public static bool operator >(Actor left, Actor right)
            => left.CompareTo(right) > 0;

        public static bool operator <=(Actor left, Actor right)
            => left == right || left < right;

        public static bool operator <(Actor left, Actor right)
            => left.CompareTo(right) < 0;
    }
}
