// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDrawingCanvas.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Saturday, 30 May 2015 8:29:35 AM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Engine
{
    using System;
    using nGratis.Cop.Gaia.Engine.Core;
    using nGratis.Cop.Gaia.Engine.Data;

    public interface IDrawingCanvas : IDisposable
    {
        void BeginBatch();

        void EndBatch();

        void Clear(IColor color);

        void DrawRectangle(Pen pen, Brush brush, Rectangle rectangle);

        void DrawLine(Pen pen, Point start, Point end);

        void DrawCircle(Pen pen, Brush brush, Point center, float radius);

        void DrawText(Pen pen, Point position, string text, string font);

        TContext GetDrawingContext<TContext>() where TContext : class;
    }
}