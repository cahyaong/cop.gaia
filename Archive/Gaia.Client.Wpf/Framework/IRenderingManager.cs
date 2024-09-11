// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRenderingManager.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Wednesday, 29 July 2015 12:33:01 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Client.Wpf.Framework
{
    using nGratis.Cop.Gaia.Engine;

    public interface IRenderingManager
    {
        void Render();

        void SetDrawingCanvas(IDrawingCanvas drawingCanvas);
    }
}