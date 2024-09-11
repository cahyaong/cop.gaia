// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IManager.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Saturday, 7 May 2016 4:21:09 AM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Client.Unity
{
    public interface IManager
    {
        void ExecuteVariableDelta(float delta);

        void ExecuteSingleTick();

        void DrawDiagnosticVisual(IDrawingCanvas canvas);
    }
}