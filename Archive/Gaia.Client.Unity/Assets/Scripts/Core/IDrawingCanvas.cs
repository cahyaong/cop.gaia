// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDrawingCanvas.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Wednesday, 11 May 2016 10:25:21 AM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Client.Unity
{
    using UnityEngine;

    public interface IDrawingCanvas
    {
        void DrawLine(Color color, Vector2 start, Vector2 end);

        void DrawRectangle(Color color, Rect rectangle);

        Vector2 ConvertToWorldPoint(Vector2 screenPoint);
    }
}