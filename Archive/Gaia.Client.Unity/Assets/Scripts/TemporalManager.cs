// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TemporalManager.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Saturday, 7 May 2016 4:18:37 AM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

using UnityEngine;

namespace nGratis.Cop.Gaia.Client.Unity
{
    using System;

    public class TemporalManager : BaseManager
    {
        private enum GameSpeed
        {
            Stop = 0,
            Normal,
            Fast,
            UberFast
        }

        private const int DefaultNumTicksPerSecond = 1;

        private GameSpeed _gameSpeed;

        private float _speedMultiplier;

        private int _numTicksPerSecond;

        private float _unprocessedPeriod;

        private float _tickPeriod;

        public uint NumTicks
        {
            get;
            private set;
        }

        public event EventHandler TickPulsed;

        public override void ExecuteVariableDelta(float delta)
        {
            this.HandleUserInput();

            if (this._gameSpeed == GameSpeed.Stop)
            {
                return;
            }

            this._unprocessedPeriod += delta;

            for (var tick = 0; this._unprocessedPeriod > this._tickPeriod && tick < this._numTicksPerSecond; tick++)
            {
                this.NumTicks++;
                this.TickPulsed.Raise();

                this._unprocessedPeriod -= this._tickPeriod;
            }
        }

        private void Start()
        {
            this.AdjustTickRate(GameSpeed.Normal);
        }

        private void HandleUserInput()
        {
            var gameSpeed = this._gameSpeed;

            if (Input.GetKeyDown(KeyCode.BackQuote))
            {
                gameSpeed = gameSpeed == GameSpeed.Stop
                    ? GameSpeed.Normal
                    : GameSpeed.Stop;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                gameSpeed = GameSpeed.Normal;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                gameSpeed = GameSpeed.Fast;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                gameSpeed = GameSpeed.UberFast;
            }

            this.AdjustTickRate(gameSpeed);
        }

        private void AdjustTickRate(GameSpeed gameSpeed)
        {
            if (this._gameSpeed == gameSpeed)
            {
                return;
            }

            this._gameSpeed = gameSpeed;

            switch (this._gameSpeed)
            {
                case GameSpeed.Stop:
                    this._speedMultiplier = 0;
                    break;

                case GameSpeed.Normal:
                    this._speedMultiplier = 1;
                    break;

                case GameSpeed.Fast:
                    this._speedMultiplier = 4;
                    break;

                case GameSpeed.UberFast:
                    this._speedMultiplier = 8;
                    break;

                default:
                    this._speedMultiplier = 1;
                    break;
            }

            this._numTicksPerSecond = (int)(this._speedMultiplier * TemporalManager.DefaultNumTicksPerSecond);

            this._tickPeriod = this._gameSpeed == GameSpeed.Stop
                ? 0
                : 1 / (float)this._numTicksPerSecond;
        }
    }
}