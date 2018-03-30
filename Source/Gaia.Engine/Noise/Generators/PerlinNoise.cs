// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PerlinNoise.cs" company="nGratis">
//   The MIT License (MIT)
//
//   Copyright (c) 2014 - 2015 Cahya Ong
//
//   Permission is hereby granted, free of charge, to any person obtaining a copy of this software and
//   associated documentation files (the "Software"), to deal in the Software without restriction, including
//   without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//   copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the
//   following conditions:
//
//   The above copyright notice and this permission notice shall be included in all copies or substantial
//   portions of the Software.
//
//   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT
//   LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO
//   EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
//   IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR
//   THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// </copyright>
// <author>Cahya Ong - cahya.ong@gmail.com</author>
// <creation_timestamp>Monday, 18 May 2015 1:27:10 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Engine.Noise
{
    public class PerlinNoise : BaseGradientNoise
    {
        public PerlinNoise()
            : this(42)
        {
        }

        public PerlinNoise(int seed)
            : this(0.05, 2.0, 0.5, 6, seed, Engine.Quality.Medium)
        {
        }

        public PerlinNoise(double frequency, double lacunarity, double persistence, int numOctaves, int seed, Quality quality)
            : base(frequency, lacunarity, persistence, numOctaves, seed, quality)
        {
        }

        public override double GetValue(double x, double y, double z)
        {
            var value = 0.0;
            var currentPersistence = 1.0;

            x *= this.Frequency;
            y *= this.Frequency;
            z *= this.Frequency;

            for (var index = 0; index < this.NumOctaves; index++)
            {
                var nx = x.ToInt32();
                var ny = y.ToInt32();
                var nz = z.ToInt32();

                var agitator = (this.Seed + index) & 0xFFFFFFFF;
                var signal = this.GetCoherentValue(nx, ny, nz, agitator);

                value += signal * currentPersistence;

                x *= this.Lacunarity;
                y *= this.Lacunarity;
                z *= this.Lacunarity;

                currentPersistence *= this.Persistence;
            }

            return value;
        }
    }
}