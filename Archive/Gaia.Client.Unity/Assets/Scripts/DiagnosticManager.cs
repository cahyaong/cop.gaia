// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DiagnosticManager.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Wednesday, 4 May 2016 9:17:42 AM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Client.Unity
{
    using System.Collections;
    using System.Linq;
    using System.Text;
    using UnityEngine;

    public class DiagnosticManager : BaseManager
    {
        private bool _isEnabled = true;

        private IDrawingCanvas _drawingCanvas;

        private TileMap _tileMap;

        private IManager[] _managers;

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

            foreach (var manager in this._managers)
            {
                manager.DrawDiagnosticVisual(this._drawingCanvas);
            }
        }

        public override void DrawDiagnosticVisual(IDrawingCanvas canvas)
        {
            canvas.DrawRectangle(Color.green, new Rect(-0.5f, -0.5f, 1f, 1f));
            canvas.DrawLine(Color.green, new Vector2(-0.25f, -0.25f), new Vector2(0.25f, 0.25f));
            canvas.DrawLine(Color.green, new Vector2(0.25f, -0.25f), new Vector2(-0.25f, 0.25f));
        }

        private void Start()
        {
            this._drawingCanvas = ObjectFinder.FindExactlySingleObject<UnityDrawingCanvas>();
            this._tileMap = ObjectFinder.FindExactlySingleObject<TileMap>();

            this._managers = Object
                .FindObjectsOfType<BaseManager>()
                .Cast<IManager>()
                .ToArray();

            this._temporalManager = this
                ._managers
                .OfType<TemporalManager>()
                .Single();

            this.StartCoroutine("DoSamplingCoroutine");
        }

        private void OnGUI()
        {
            if (!this._isEnabled)
            {
                return;
            }

            var screenPoint = Input.mousePosition;
            var worldPoint = this._drawingCanvas.ConvertToWorldPoint(screenPoint);
            var gridPoint = this._tileMap.ConvertToGridPoint(worldPoint);

            var statisticContent = new StringBuilder()
                .AppendLine("--simulation.total-ticks: {0:N0}", this._temporalManager.NumTicks)
                .AppendLine("--simulation.ticks-per-second: {0:N0}", this._deltaTicks)
                .AppendLine("--input.mouse-screen: ({0:N0}, {1:N0})", screenPoint.x, screenPoint.y)
                .AppendLine("--input.mouse-world: ({0:N2}, {1:N2})", worldPoint.x, worldPoint.y)
                .AppendLine("--input.mouse-grid: ({0:N2}, {1:N2})", gridPoint.x, gridPoint.y)
                .ToString();

            GUI.Label(LabelConfiguration.StatisticArea, statisticContent, LabelConfiguration.StatisticStyle);
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

        private static class LabelConfiguration
        {
            public static readonly Rect StatisticArea;

            public static readonly GUIStyle StatisticStyle;

            static LabelConfiguration()
            {
                LabelConfiguration.StatisticArea = new Rect(10, 10, 250, 100);

                var statisticTexture = new Texture2D(1, 1, TextureFormat.RGBA32, false);
                statisticTexture.SetPixel(0, 0, new Color(0.1f, 0.1f, 0.1f, 0.8f));
                statisticTexture.Apply();

                LabelConfiguration.StatisticStyle = new GUIStyle
                {
                    fontSize = 14,
                    normal = new GUIStyleState
                    {
                        textColor = new Color(0.9f, 0.9f, 0.9f, 0.8f),
                        background = statisticTexture
                    },
                    contentOffset = new Vector2(5, 5)
                };
            }
        }
    }
}