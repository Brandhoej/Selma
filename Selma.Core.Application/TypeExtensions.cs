using System;

namespace Selma.Core.Application
{
    internal static class TypeExtensions
    {
        internal static bool IsConcretionAssignableTo<TAbstraction>(this Type concretion)
            => concretion.IsConcretionAssignableTo(typeof(TAbstraction));

        internal static bool IsConcretionAssignableTo(this Type concretion, Type abstraction)
        {
            if (concretion == null)
            {
                throw new ArgumentNullException(nameof(concretion));
            }

            if (abstraction == null)
            {
                throw new ArgumentNullException(nameof(abstraction));
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

            if (concretion.IsGenericType && concretion.GetGenericTypeDefinition() == abstraction)
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
