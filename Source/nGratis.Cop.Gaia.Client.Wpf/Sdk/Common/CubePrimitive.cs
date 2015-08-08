// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CubePrimitive.cs" company="nGratis">
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
// <creation_timestamp>Monday, 27 July 2015 2:26:46 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Client.Wpf.Sdk
{
    using Microsoft.Xna.Framework;

    public class CubePrimitive : GeometricPrimitive
    {
        public CubePrimitive()
            : this(1.0F)
        {
        }

        public CubePrimitive(float length)
        {
            var normals = new[]
                {
                    new Vector3(0.0F, 0.0F, 1.0F),
                    new Vector3(0.0F, 0.0F, -1.0F),
                    new Vector3(1.0F, 0.0F, 0.0F),
                    new Vector3(-1.0F, 0.0F, 0.0F),
                    new Vector3(0.0F, 1.0F, 0.0F),
                    new Vector3(0.0F, -1.0F, 0.0F),
                };

            var numVertices = 0;

            foreach (var normal in normals)
            {
                var u = new Vector3(normal.Y, normal.Z, normal.X);
                var v = Vector3.Cross(normal, u);

                this.AddIndex(numVertices + 0);
                this.AddIndex(numVertices + 1);
                this.AddIndex(numVertices + 2);

                this.AddIndex(numVertices + 0);
                this.AddIndex(numVertices + 2);
                this.AddIndex(numVertices + 3);

                this.AddVertex((normal - u - v) * length / 2.0F, normal);
                this.AddVertex((normal - u + v) * length / 2.0F, normal);
                this.AddVertex((normal + u + v) * length / 2.0F, normal);
                this.AddVertex((normal + u - v) * length / 2.0F, normal);

                numVertices += 4;
            }
        }
    }
}