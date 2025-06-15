#nullable enable

using System;
using System.Collections.Generic;
using UnityEngine;

namespace TileMonsterHunter.Abstractions
{
    public interface ITileHotBar
    {
        IReadOnlyList<TileInfo> Tiles { get; }
        IReadOnlyCollection<TileMatchKey> TileMatchKeys { get; }
        int MaxLength { get; }

        event Action? Overflowed;
        event Action<TileInfo>? TileAdded;
        event Action<TileInfo[]>? TilesRemoved;
        event Action? TilesCleared;
        event Action<int>? MaxLengthChanged;

        void Reset(int length);
        void AddTile(TileInfo tileInfo);
        void RemoveTiles(params TileInfo[] tilesInfo);
        void Clear();

        IReadOnlyList<TileInfo> GetTilesByMatchKey(TileMatchKey key);
    }
}
