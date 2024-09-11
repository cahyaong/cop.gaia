// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Range.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Wednesday, 8 July 2015 11:05:30 AM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Engine.Data
{
    public class Range<TValue>
    {
        public Range(TValue startValue, TValue endValue)
        {
            this.StartValue = startValue;
            this.EndValue = endValue;
        }

        public TValue StartValue { get; private set; }

        public TValue EndValue { get; private set; }
    }
}