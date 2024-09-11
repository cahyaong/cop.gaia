// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Size.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Tuesday, 25 August 2015 12:54:38 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Engine.Data
{
    using nGratis.Cop.Gaia.Engine.Core;

    public struct Size
    {
        public static readonly Size Zero = new Size(0, 0);

        public static readonly Size One = new Size(1, 1);

        public Size(float width, float height)
            : this()
        {
            RapidGuard.AgainstNegativeArgument(width);
            RapidGuard.AgainstNegativeArgument(height);

            this.Width = width;
            this.Height = height;
        }

        public float Width
        {
            get;
            private set;
        }

        public float Height
        {
            get;
            private set;
        }

        public static Size operator *(Size size, float factor)
        {
            return new Size(size.Width * factor, size.Height * factor);
        }
    }
}