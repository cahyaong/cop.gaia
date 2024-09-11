// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Rectangle.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Thursday, 30 July 2015 1:04:19 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Engine.Data
{
    using nGratis.Cop.Gaia.Engine.Core;

    public struct Rectangle
    {
        public Rectangle(float width, float height)
            : this(0.0F, 0.0F, width, height)
        {
        }

        public Rectangle(float x, float y, float width, float height)
            : this()
        {
            RapidGuard.AgainstNegativeArgument(width);
            RapidGuard.AgainstNegativeArgument(height);

            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
        }

        public float X { get; set; }

        public float Y { get; set; }

        public float Width { get; private set; }

        public float Height { get; private set; }
    }
}