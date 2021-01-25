using Selma.Core.PersonalInformation.Abstractions;
using System;

namespace Selma.Core.Domain
{
    /// <summary>
    ///     An attribute which is used to mark values
    ///     as personal information for a user.
    ///     This <see cref="Attribute"/> allows us
    ///     To implement GDPR more fluently.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class PersonalInformationAttribute
        : Attribute
        , IPersonalInformation
    { }
}
