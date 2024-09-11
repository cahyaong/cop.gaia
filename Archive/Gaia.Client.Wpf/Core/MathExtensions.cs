// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MathExtensions.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Thursday, 30 July 2015 1:07:50 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Client.Wpf
{
    using nGratis.Cop.Gaia.Engine.Data;

    public static class MathExtensions
    {
        public static System.Windows.Point ToWindowsPoint(this Point point)
        {
            return new System.Windows.Point(point.X, point.Y);
        }

        public static System.Windows.Rect ToWindowsRectangle(this Rectangle rectangle)
        {
            return new System.Windows.Rect(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
        }
    }
}