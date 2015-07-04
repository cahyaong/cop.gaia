// ------------------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="TileMapViewport.cs" company="nGratis">
//  The MIT License (MIT)
//
//  Copyright (c) 2014 - 2015 Cahya Ong
//
//  Permission is hereby granted, free of charge, to any person obtaining a copy
//  of this software and associated documentation files (the "Software"), to deal
//  in the Software without restriction, including without limitation the rights
//  to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//  copies of the Software, and to permit persons to whom the Software is
//  furnished to do so, subject to the following conditions:
//
//  The above copyright notice and this permission notice shall be included in all
//  copies or substantial portions of the Software.
//
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//  SOFTWARE.
// </copyright>
// <author>Cahya Ong - cahya.ong@gmail.com</author>
// <creation_timestamp>Tuesday, 2 June 2015 1:12:01 PM UTC</creation_timestamp>
// ------------------------------------------------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Wpf
{
    using System;
    using nGratis.Cop.Gaia.Engine;

    internal class TileMapViewport : ITileMapViewport
    {
        public TileMapViewport()
        {
            this.Column = 0U;
            this.Row = 0U;
            this.NumRows = 64U;
            this.NumColumns = 64U;
            this.MaxNumRows = 64U;
            this.MaxNumColumns = 64U;
        }

        public uint Column { get; private set; }

        public uint Row { get; private set; }

        public uint NumRows { get; private set; }

        public uint NumColumns { get; private set; }

        public uint MaxNumRows { get; private set; }

        public uint MaxNumColumns { get; private set; }

        public void Reset()
        {
            this.Row = 0U;
            this.Column = 0U;
        }

        public void Resize(uint numRows, uint numColumns)
        {
            numRows = numRows.Clamp(0U, this.MaxNumRows);
            numColumns = numColumns.Clamp(0U, this.MaxNumColumns);

            this.Pan(
                Math.Max((int)-this.Row, (int)(this.NumRows - numRows)),
                Math.Max((int)-this.Column, (int)(this.NumColumns - numColumns)));

            this.NumRows = numRows;
            this.NumColumns = numColumns;
        }

        public void Pan(int deltaRows, int deltaColumns)
        {
            this.Row = (uint)(this.Row + deltaRows);
            this.Column = (uint)(this.Column + deltaColumns);
        }
    }
}