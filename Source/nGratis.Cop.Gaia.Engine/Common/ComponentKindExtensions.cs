// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ComponentKindExtensions.cs" company="nGratis">
//  The MIT License (MIT)
//
//  Copyright (c) 2014 - 2015 Cahya Ong
//
//  Permission is hereby granted, free of charge, to any person obtaining a copy
//  of this software and associated documentation files (the "Software"), to deal
//  in the Software without restriction, including without limitation the rights
//  to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//  copies of the Software, and to permit persons to whom the Software is
//  furnished to do so, subject to the following conditions:
//
//  The above copyright notice and this permission notice shall be included in all
//  copies or substantial portions of the Software.
//
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//  SOFTWARE.
// </copyright>
// <author>Cahya Ong - cahya.ong@gmail.com</author>
// <creation_timestamp>Tuesday, 18 August 2015 1:48:33 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Engine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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
                .GetCustomAttributes(false)
                .OfType<ComponentAttribute>()
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