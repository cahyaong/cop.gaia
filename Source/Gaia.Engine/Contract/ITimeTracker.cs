// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITimeTracker.cs" company="nGratis">
//  The MIT License — Copyright (c) Cahya Ong
//  See the LICENSE file in the project root for more information.
// </copyright>
// <author>Cahya Ong — cahya.ong@gmail.com</author>
// <creation_timestamp>Tuesday, September 24, 2024 7:01:29 AM UTC</creation_timestamp>
// --------------------------------------------------------------------------------------------------------------------

namespace nGratis.Cop.Gaia.Engine;

public interface ITimeTracker
{
    event EventHandler<TimeChangedEventArgs> TimeChanged;

    void Start();

    void End();
}

public class TimeChangedEventArgs : EventArgs
{
    public uint Tick { get; init; }
}