// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITileMapViewport.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Tuesday, 2 June 2015 1:11:35 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Client.Wpf
{
    using System;
    using nGratis.Cop.Gaia.Engine;
    using nGratis.Cop.Gaia.Engine.Data;

    public interface ITileMapViewport
    {
        int Row { get; }

        int Column { get; }

        int NumRows { get; }

        int NumColumns { get; }

        int MostRows { get; }

        int MostColumns { get; }

        IObservable<Coordinate> WhenCoordinateUpdated { get; }

        void Reset();

        void Resize(int numRows, int numColumns);

        void Pan(int deltaRows, int deltaColumns);

        bool IsTileVisible(Tile tile);
    }
}