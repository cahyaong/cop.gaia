// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TileMapViewport.cs" company="nGratis">
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
// <creation_timestamp>Tuesday, 2 June 2015 1:12:01 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Client.Wpf
{
    using System;
    using System.Reactive.Subjects;
    using nGratis.Cop.Gaia.Engine;
    using nGratis.Cop.Gaia.Engine.Data;

    internal class TileMapViewport : ITileMapViewport
    {
        private readonly ISubject<Coordinate> coordinateUpdatedSubject = new Subject<Coordinate>();

        public TileMapViewport()
        {
            this.Column = 0;
            this.Row = 0;
            this.NumRows = 64;
            this.NumColumns = 64;
            this.MostRows = 64;
            this.MostColumns = 64;
        }

        public int Column
        {
            get;
            private set;
        }

        public int Row
        {
            get;
            private set;
        }

        public int NumRows
        {
            get;
            private set;
        }

        public int NumColumns
        {
            get;
            private set;
        }

        public int MostRows
        {
            get;
            private set;
        }

        public int MostColumns
        {
            get;
            private set;
        }

        public IObservable<Coordinate> WhenCoordinateUpdated
        {
            get { return this.coordinateUpdatedSubject; }
        }

        public void Reset()
        {
            this.Row = 0;
            this.Column = 0;

            this.coordinateUpdatedSubject.OnNext(Coordinate.Origin);
        }

        public void Resize(int numRows, int numColumns)
        {
            numRows = numRows.Clamp(0, this.MostRows);
            numColumns = numColumns.Clamp(0, this.MostColumns);

            this.Pan(
                Math.Max(-this.Row, this.NumRows - numRows),
                Math.Max(-this.Column, this.NumColumns - numColumns));

            this.NumRows = numRows;
            this.NumColumns = numColumns;

            this.coordinateUpdatedSubject.OnNext(new Coordinate() { Row = this.Row, Column = this.Column });
        }

        public void Pan(int deltaRows, int deltaColumns)
        {
            this.Row = Math.Max(0, this.Row + deltaRows);
            this.Column = Math.Max(0, this.Column + deltaColumns);

            this.coordinateUpdatedSubject.OnNext(new Coordinate() { Row = this.Row, Column = this.Column });
        }

        public bool IsTileVisible(Tile tile)
        {
            if (tile == null)
            {
                return false;
            }

            return
                tile.Row.IsBetween(this.Row, this.Row + this.NumRows - 1) &&
                tile.Column.IsBetween(this.Column, this.Column + this.NumColumns - 1);
        }
    }
}