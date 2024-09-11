// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ComponentKindExtensions.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Tuesday, 18 August 2015 1:48:33 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Engine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using nGratis.Cop.Gaia.Engine.Core;

    internal static class ComponentKindExtensions
    {
        // TODO: Consider making this lookup thread safe?
        private static readonly IDictionary<Type, ComponentKind> ComponentKindLookup;

        static ComponentKindExtensions()
        {
            ComponentKindLookup = new Dictionary<Type, ComponentKind>();
        }

        public static ComponentKind ToComponentKind(this Type type)
        {
            RapidGuard.AgainstNullArgument(type);

            var kind = ComponentKind.None;

            if (ComponentKindLookup.TryGetValue(type, out kind))
            {
                return kind;
            }

            var attribute = type
                .GetCustomAttributes<ComponentAttribute>()
                .SingleOrDefault();

            if (attribute != null)
            {
                kind = attribute.ComponentKind;
            }

            ComponentKindLookup.Add(type, kind);

            return kind;
        }

        public static ComponentKinds ToComponentKinds(this IEnumerable<IComponent> components)
        {
            if (components == null)
            {
                return ComponentKinds.None;
            }

            var kinds = components
                .Select(component => component.GetType().ToComponentKind())
                .ToArray();

            return new ComponentKinds(kinds);
        }
    }
}