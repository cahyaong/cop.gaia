// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseManager.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Saturday, 7 May 2016 4:50:49 AM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

using UnityEngine;

namespace nGratis.Cop.Gaia.Client.Unity
{
    public class BaseManager : MonoBehaviour, IManager
    {
        public virtual void ExecuteVariableDelta(float delta)
        {
        }

        public virtual void ExecuteSingleTick()
        {
        }

        public virtual void DrawDiagnosticVisual(IDrawingCanvas canvas)
        {
        }
    }
}