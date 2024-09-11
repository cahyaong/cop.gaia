// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CubePrimitive.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
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