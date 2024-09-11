// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TypeExtensions.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Wednesday, 9 September 2015 3:34:01 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable CheckNamespace
namespace System
// ReSharper restore CheckNamespace
{
    using System.Linq;
    using nGratis.Cop.Core.Contract;

    public static class TypeExtensions
    {
        public static Type FindDirectInterface(this Type type)
        {
            Guard.AgainstNullArgument(() => type);

            var interfaces = type.GetInterfaces();

            return interfaces
                .Except(interfaces.SelectMany(item => item.GetInterfaces()))
                .SingleOrDefault();
        }
    }
}