// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SystemConstant.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Wednesday, 5 August 2015 1:48:23 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Engine
{
    public static class SystemConstant
    {
        public static class UpdatingOrders
        {
            public const int Any = 0;
            public const int DecisionMaking = 100;
            public const int Movement = 101;
            public const int Rendering = 200;
            public const int Diagnostic = 900;
        }

        public static class RenderingOrders
        {
            public const int None = 0;
            public const int Background = 100;
        }
    }
}