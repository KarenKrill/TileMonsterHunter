#nullable enable

using System;
using System.Collections.Generic;

namespace TileMonsterHunter.Abstractions
{
    public interface ITileField
    {
        IReadOnlyList<TileInfo> Tiles { get; }

        event Action<TileInfo>? TileChosen;
        event Action<TileInfo>? TileRemoved;

        void Generate(TileFieldGenerationOptions options);
        void ChooseTile(int id);
        void RemoveTile(int id);
    }
    public class TileFieldGenerationOptions
    {
        public ITileRepository TileRepository { get; }
        public int MaxTilesCount { get; }
        public int MatchCount { get; }

        public TileFieldGenerationOptions(ITileRepository tileRepository, int maxTilesCount, int matchCount = 3)
        {
            TileRepository = tileRepository;
            MaxTilesCount = maxTilesCount;
            MatchCount = matchCount;
        }
    }
}
