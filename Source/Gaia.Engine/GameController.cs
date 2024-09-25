// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GameController.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Wednesday, September 25, 2024 5:48:49 AM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Engine;

public class GameController : IGameController
{
    private readonly ITimeTracker _timeTracker;

    private readonly IReadOnlyCollection<ISystem> _systems;

    public GameController(ITimeTracker timeTracker, IReadOnlyCollection<ISystem> systems)
    {
        this._timeTracker = timeTracker;
        this._timeTracker.TimeChanged += this.OnTimeChanged;

        this._systems = systems;
    }

    public void Start()
    {
        this._timeTracker.Start();
    }

    public void End()
    {
        this._timeTracker.End();
    }

    private void OnTimeChanged(object? _, TimeChangedEventArgs args)
    {
        this._systems.ForEach(system => system.Process(args.Tick));
    }
}