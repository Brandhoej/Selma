using System;
using System.Reflection;

namespace Selma.Core.Application
{
    internal static class TypeExtensions
    {
        /// <summary>
        ///     Recursively invokes with the BaseType (starting from <paramref name="concretion"/>) until 
        ///     the desired abstraction (<typeparamref name="TAbstraction"/>) has been found 
        ///     or that the inheritance tree has been traveresed completly.
        /// </summary>
        /// <typeparam name="TAbstraction">
        ///     The <typeparamref name="TAbstraction"/> which we want to recursively 
        ///     look in the current BaseType <see cref="Type"/> if it has.
        /// </typeparam>
        /// <param name="concretion">
        ///     The <paramref name="concretion"/> <see cref="Type"/> which we want to
        ///     recursively look for the <paramref name="abstraction"/>.
        /// </param>
        /// <returns>
        ///     True if the <paramref name="concretion"/> contains the 
        ///     <typeparamref name="TAbstraction"/> somewhere in the inheritance tree.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="concretion"/> is null.
        /// </exception>
        /// <exception cref="TargetInvocationException">
        ///     The <see cref="Exception"/> that is thrown by methods invoked through reflection.
        ///     When created, the <see cref="TargetInvocationException"/> is passed a reference to the 
        ///     <see cref="Exception"/> thrown by the method invoked through reflection.
        ///     The <see cref="Exception.InnerException"/> property holds the underlying exception.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     The current <see cref="Type"/> is not a generic <see cref="Type"/>. 
        ///     That is, <see cref="Type.IsGenericType"/> returns False.
        ///     This should NEVER(!) be thrown because <see cref="Type.IsGenericType"/> 
        ///     is always checked before the <see cref="Type.IsGenericType"/> is called.
        /// </exception>
        /// <exception cref="NotSupportedException">
        ///     Is thrown if the <paramref name="abstraction"/> or <paramref name="concretion"/>
        ///     does not support <see cref="Type.GetGenericTypeDefinition"/>.
        /// </exception>
        internal static bool IsConcretionAssignableTo<TAbstraction>(this Type concretion)
            => concretion.IsConcretionAssignableTo(typeof(TAbstraction));

        /// <summary>
        ///     Recursively invokes with the BaseType (starting from <paramref name="concretion"/>) until 
        ///     the desired abstraction (<paramref name="abstraction"/>) has been found 
        ///     or that the inheritance tree has been traveresed completly.
        /// </summary>
        /// <param name="concretion">
        ///     The <paramref name="concretion"/> <see cref="Type"/> which we want to
        ///     recursively look for the <paramref name="abstraction"/>.
        /// </param>
        /// <param name="abstraction">
        ///     The <paramref name="abstraction"/> <see cref="Type"/> which we want to recursively 
        ///     look in the current BaseType <see cref="Type"/> if it has.
        /// </param>
        /// <returns>
        ///     True if the <paramref name="concretion"/> contains the <paramref name="abstraction"/>
        ///     somewhere in the inheritance tree.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     <paramref name="abstraction"/> or <paramref name="concretion"/> is null.
        /// </exception>
        /// <exception cref="TargetInvocationException">
        ///     The <see cref="Exception"/> that is thrown by methods invoked through reflection.
        ///     When created, the <see cref="TargetInvocationException"/> is passed a reference to the 
        ///     <see cref="Exception"/> thrown by the method invoked through reflection.
        ///     The <see cref="Exception.InnerException"/> property holds the underlying exception.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        ///     The current <see cref="Type"/> is not a generic <see cref="Type"/>. 
        ///     That is, <see cref="Type.IsGenericType"/> returns False.
        ///     This should NEVER(!) be thrown because <see cref="Type.IsGenericType"/> 
        ///     is always checked before the <see cref="Type.IsGenericType"/> is called.
        /// </exception>
        /// <exception cref="NotSupportedException">
        ///     Is thrown if the <paramref name="abstraction"/> or <paramref name="concretion"/>
        ///     does not support <see cref="Type.GetGenericTypeDefinition"/>.
        /// </exception>
        internal static bool IsConcretionAssignableTo(this Type concretion, Type abstraction)
        {

            if (abstraction == null)
            {
                throw new ArgumentNullException(nameof(abstraction));
            }

            if (concretion == null)
            {
                return false;
            }

            Type[] interfaceTypes = concretion.GetInterfaces();
            foreach (Type interfaceType in interfaceTypes)
            {
                if (interfaceType.IsGenericType &&
                    interfaceType.GetGenericTypeDefinition() == abstraction)
                {
                    return true;
                }
            }

            if (concretion.IsGenericType && 
                concretion.GetGenericTypeDefinition() == abstraction)
            {
                return true;
            }

            Type baseType = concretion.BaseType;
            if (baseType == null)
            {
                return false;
            }

            return baseType.IsConcretionAssignableTo(abstraction);
        }
    }
}
