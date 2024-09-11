// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GameBootstrap.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
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