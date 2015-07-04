// ------------------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="WorldViewModel.cs" company="nGratis">
//  The MIT License (MIT)
//
//  Copyright (c) 2014 - 2015 Cahya Ong
//
//  Permission is hereby granted, free of charge, to any person obtaining a copy
//  of this software and associated documentation files (the "Software"), to deal
//  in the Software without restriction, including without limitation the rights
//  to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//  copies of the Software, and to permit persons to whom the Software is
//  furnished to do so, subject to the following conditions:
//
//  The above copyright notice and this permission notice shall be included in all
//  copies or substantial portions of the Software.
//
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//  SOFTWARE.
// </copyright>
// <author>Cahya Ong - cahya.ong@gmail.com</author>
// <creation_timestamp>Wednesday, 27 May 2015 12:56:08 PM UTC</creation_timestamp>
// ------------------------------------------------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Wpf
{
    using System.ComponentModel.Composition;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using nGratis.Cop.Core.Wpf;
    using nGratis.Cop.Gaia.Engine;
    using ReactiveUI;

    [Export]
    public class WorldViewModel : BaseFormPageViewModel
    {
        private bool isBusy;

        private WorldMap worldMap;

        private IWorldGenerator worldGenerator;

        private IWorldMapRenderer worldMapRenderer;

        private IDiagnosticBucket diagnosticBucket;

        private string seed;

        [ImportingConstructor]
        public WorldViewModel(IWorldGenerator worldGenerator, IWorldMapRenderer worldMapRenderer, IDiagnosticBucket diagnosticBucket)
        {
            this.WorldGenerator = worldGenerator;
            this.WorldMapRenderer = worldMapRenderer;
            this.DiagnosticBucket = diagnosticBucket;

            this.GenerateWorldCommand = ReactiveCommand.CreateAsyncTask(
                this.WhenAnyValue(vm => vm.IsBusy, isBusy => !isBusy),
                async _ => await this.GenerateWorldAsync());
        }

        public bool IsBusy
        {
            get { return this.isBusy; }
            private set { this.RaiseAndSetIfChanged(ref this.isBusy, value); }
        }

        public WorldMap WorldMap
        {
            get { return this.worldMap; }
            private set { this.RaiseAndSetIfChanged(ref this.worldMap, value); }
        }

        public IWorldGenerator WorldGenerator
        {
            get { return this.worldGenerator; }
            private set { this.RaiseAndSetIfChanged(ref this.worldGenerator, value); }
        }

        public IWorldMapRenderer WorldMapRenderer
        {
            get { return this.worldMapRenderer; }
            private set { this.RaiseAndSetIfChanged(ref this.worldMapRenderer, value); }
        }

        public IDiagnosticBucket DiagnosticBucket
        {
            get { return this.diagnosticBucket; }
            private set { this.RaiseAndSetIfChanged(ref this.diagnosticBucket, value); }
        }

        [AsField(FieldMode.Input, FieldType.Text, "Seed:")]
        public string Seed
        {
            get { return this.seed; }
            set { this.RaiseAndSetIfChanged(ref this.seed, value); }
        }

        public ICommand GenerateWorldCommand { get; private set; }

        private async Task GenerateWorldAsync()
        {
            this.IsBusy = true;

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            this.WorldMap = await this.WorldGenerator.GenerateMapAsync(this.Seed ?? string.Empty);

            stopwatch.Stop();
            this.DiagnosticBucket.AddOrUpdateItem(DiagnosticKey.GenerationTime, stopwatch.ElapsedMilliseconds);

            this.IsBusy = false;
        }
    }
}