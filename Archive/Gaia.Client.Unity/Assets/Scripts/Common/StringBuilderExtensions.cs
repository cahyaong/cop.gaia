// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StringBuilderExtensions.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Wednesday, 4 May 2016 9:44:39 AM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Client.Unity
{
    using System.Globalization;
    using System.Text;

    public static class StringBuilderExtensions
    {
        public static StringBuilder AppendLine(this StringBuilder builder, string format, params object[] args)
        {
            Guard.Argument.IsNull(builder);

            return builder.AppendLine(string.Format(CultureInfo.CurrentCulture, format, args));
        }
    }
}