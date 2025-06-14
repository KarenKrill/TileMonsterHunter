#nullable enable

using System;

namespace TileMonsterHunter.Abstractions
{
    public interface ITileFieldSpawner
    {
        event Action? SpawnCompleted;
        void Spawn(ITileField tileField);
    }
}
