// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DiagnosticManager.cs" company="nGratis">
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
// <creation_timestamp>Wednesday, 4 May 2016 9:17:42 AM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Client.Unity
{
    using System.Collections;
    using System.Text;
    using UnityEngine;

    public class DiagnosticManager : BaseManager
    {
        private const int MarginInPixel = 20;

        private bool _isEnabled = true;

        private IDrawingCanvas _drawingCanvas;

        private TemporalManager _temporalManager;

        private uint _previousNumTicks;

        private uint _deltaTicks;

        public override void ExecuteVariableDelta(float delta)
        {
            this.HandleUserInput();

            if (!this._isEnabled)
            {
                return;
            }

            this._drawingCanvas.DrawRectangle(Color.green, new Rect(-0.5f, -0.5f, 1f, 1f));
            this._drawingCanvas.DrawLine(Color.green, new Vector2(-0.25f, 0f), new Vector2(0.25f, 0f));
            this._drawingCanvas.DrawLine(Color.green, new Vector2(0f, -0.25f), new Vector2(0f, 0.25f));
        }

        private void Start()
        {
            this._drawingCanvas = ObjectFinder.FindExactlySingleObject<UnityDrawingCanvas>();
            this._temporalManager = ObjectFinder.FindExactlySingleObject<TemporalManager>();

            this.StartCoroutine("DoSamplingCoroutine");
        }

        private void OnGUI()
        {
            if (!this._isEnabled)
            {
                return;
            }

            var labelArea = new Rect(
                DiagnosticManager.MarginInPixel,
                DiagnosticManager.MarginInPixel,
                Screen.width - DiagnosticManager.MarginInPixel,
                Screen.height - DiagnosticManager.MarginInPixel);

            var labelBuilder = new StringBuilder()
                .AppendLine("--simulation.total-ticks: {0:N0}", this._temporalManager.NumTicks)
                .AppendLine("--simulation.ticks-per-second: {0:N0}", this._deltaTicks);

            GUI.Label(labelArea, labelBuilder.ToString());
        }

        private void HandleUserInput()
        {
            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.D))
            {
                this._isEnabled = !this._isEnabled;
                this._previousNumTicks = this._temporalManager.NumTicks;
                this._deltaTicks = 0;

                if (this._isEnabled)
                {
                    this.StartCoroutine("DoSamplingCoroutine");
                }
                else
                {
                    this.StopCoroutine("DoSamplingCoroutine");
                }
            }
        }

        private IEnumerator DoSamplingCoroutine()
        {
            while (this._isEnabled)
            {
                var currentNumTicks = this._temporalManager.NumTicks;
                this._deltaTicks = currentNumTicks - this._previousNumTicks;
                this._previousNumTicks = currentNumTicks;

                yield return new WaitForSeconds(1);
            }
        }
    }
}