// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WorldViewModel.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Wednesday, 27 May 2015 12:56:08 PM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Client.Wpf
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

        private Region selectedRegion;

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

        public Region SelectedRegion
        {
            get
            {
                return this.selectedRegion;
            }

            set
            {
                this.RaiseAndSetIfChanged(ref this.selectedRegion, value);
                this.OnSelectedRegionChanged();
            }
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

        private void OnSelectedRegionChanged()
        {
            this.DiagnosticBucket.AddOrUpdateItem(
                DiagnosticKey.Region.Elevation,
                this.SelectedRegion != null ? this.SelectedRegion.Elevation : 0);
        }
    }
}