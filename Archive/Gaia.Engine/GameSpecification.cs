// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GameSpecification.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Wednesday, 9 September 2015 1:58:11 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Engine
{
    using nGratis.Cop.Core.Contract;
    using nGratis.Cop.Gaia.Engine.Data;

    public class GameSpecification
    {
        public GameSpecification(Size mapSize, float tileLength)
        {
            Guard.AgainstDefaultArgument(() => mapSize);
            Guard.AgainstInvalidArgument(tileLength <= 0, () => tileLength);

            this.MapSize = mapSize;
            this.TileLength = tileLength;

            this.ScreenSize = mapSize * tileLength;
        }

        public Size ScreenSize { get; private set; }

        public Size MapSize { get; private set; }

        public float TileLength { get; private set; }
    }
}