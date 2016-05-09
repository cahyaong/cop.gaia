﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GameBootstrap.cs" company="nGratis">
//   The MIT License (MIT)
//
//   Copyright (c) 2014 - 2016 Cahya Ong
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
// <creation_timestamp>Saturday, 30 April 2016 6:23:56 AM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Client.Unity
{
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    public class GameBootstrap : MonoBehaviour
    {
        private bool _isTickPulsed;

        private readonly List<IManager> _managers = new List<IManager>();

        private void Start()
        {
            this._managers.AddRange(Object.FindObjectsOfType<BaseManager>());

            var temporalManager = this
                ._managers
                .OfType<TemporalManager>()
                .SingleOrDefault();

            if (temporalManager != null)
            {
                temporalManager.TickPulsed += (_, __) => this._isTickPulsed = true;
            }
        }

        private void Update()
        {
            var delta = Time.deltaTime;

            this
                ._managers
                .ForEach(manager => manager.ExecuteVariableDelta(delta));

            if (!this._isTickPulsed)
            {
                return;
            }

            this
                ._managers
                .ForEach(manager => manager.ExecuteSingleTick());

            this._isTickPulsed = false;
        }
    }
}