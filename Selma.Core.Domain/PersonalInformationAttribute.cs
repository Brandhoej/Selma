using System;

namespace Selma.Core.Domain
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class PersonalInformationAttribute 
        : Attribute
    { }
}
