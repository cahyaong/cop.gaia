﻿// ------------------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseLayerGenerator.cs" company="nGratis">
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
// <creation_timestamp>Monday, 29 June 2015 12:31:48 PM UTC</creation_timestamp>
// ------------------------------------------------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Engine
{
    using System;
    using System.Collections.Generic;
    using nGratis.Cop.Gaia.Engine.Core;

    public abstract class BaseLayerGenerator : ILayerGenerator
    {
        public string Seed { get; private set; }

        public abstract LayerMode LayerMode { get; }

        public virtual void UpdateSeed(string seed)
        {
            Guard.AgainstNullArgument(() => seed);

            this.Seed = seed;
        }

        public void GenerateLayer(WorldMap worldMap, uint startIndex, uint endIndex)
        {
            Guard.AgainstNullArgument(() => worldMap);
            Guard.AgainstInvalidArgument(startIndex >= endIndex, () => startIndex);

            for (var index = startIndex; index <= endIndex; index++)
            {
                this.GenerateLayer(worldMap[index]);
            }
        }

        protected abstract void GenerateLayer(Region region);
    }
}