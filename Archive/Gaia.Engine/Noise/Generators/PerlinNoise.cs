// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PerlinNoise.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
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