// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GameManager.cs" company="nGratis">
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
    using UnityEngine;

    public class GameManager : MonoBehaviour
    {
        private enum SimulationSpeed
        {
            Stop = 0,
            Normal,
            Fast,
            UberFast
        }

        private const int DefaultNumTicksPerSecond = 1;

        private float _unprocessedTime;

        private SimulationSpeed _simulationSpeed;

        private float _simulationRateMultiplier;

        private int _numTicksPerSecond;

        private float _tickPeriod;

        public uint NumTicks
        {
            get;
            private set;
        }

        private void Start()
        {
            this.AdjustSimulationRate(SimulationSpeed.Normal);
        }

        private void Update()
        {
            this.HandleUserInput();

            if (this._simulationSpeed == SimulationSpeed.Stop)
            {
                return;
            }

            this._unprocessedTime += Time.deltaTime;

            for (var tick = 0; this._unprocessedTime > this._tickPeriod && tick < this._numTicksPerSecond; tick++)
            {
                this.ExecuteSingleTick();
                this.NumTicks++;
                this._unprocessedTime -= this._tickPeriod;
            }
        }

        private void ExecuteSingleTick()
        {
        }

        private void HandleUserInput()
        {
            var simulationSpeed = this._simulationSpeed;

            if (Input.GetKeyDown(KeyCode.BackQuote))
            {
                simulationSpeed = simulationSpeed == SimulationSpeed.Stop
                    ? SimulationSpeed.Normal
                    : SimulationSpeed.Stop;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                simulationSpeed = SimulationSpeed.Normal;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                simulationSpeed = SimulationSpeed.Fast;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                simulationSpeed = SimulationSpeed.UberFast;
            }

            this.AdjustSimulationRate(simulationSpeed);
        }

        private void AdjustSimulationRate(SimulationSpeed simulationSpeed)
        {
            if (this._simulationSpeed == simulationSpeed)
            {
                return;
            }

            this._simulationSpeed = simulationSpeed;

            switch (this._simulationSpeed)
            {
                case SimulationSpeed.Stop:
                    this._simulationRateMultiplier = 0;
                    break;

                case SimulationSpeed.Normal:
                    this._simulationRateMultiplier = 1;
                    break;

                case SimulationSpeed.Fast:
                    this._simulationRateMultiplier = 4;
                    break;

                case SimulationSpeed.UberFast:
                    this._simulationRateMultiplier = 8;
                    break;

                default:
                    this._simulationRateMultiplier = 1;
                    break;
            }

            this._numTicksPerSecond = (int)(this._simulationRateMultiplier * GameManager.DefaultNumTicksPerSecond);

            this._tickPeriod = this._simulationSpeed == SimulationSpeed.Stop
                ? 0
                : 1 / (float)this._numTicksPerSecond;
        }
    }
}