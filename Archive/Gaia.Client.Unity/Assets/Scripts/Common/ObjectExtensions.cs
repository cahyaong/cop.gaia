// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ObjectExtensions.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Wednesday, 11 May 2016 10:37:34 AM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Client.Unity
{
    using System.Linq;
    using UnityEngine;

    internal static class ObjectFinder
    {
        public static T FindExactlySingleObject<T>()
            where T : Object
        {
            var objects = Object.FindObjectsOfType<T>();
            Guard.Operation.IsInvalidState(objects.Length != 1);

            return objects.Single();
        }
    }
}