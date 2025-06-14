using System;
using System.Collections.Generic;

namespace TileMonsterHunter.Abstractions
{
    [Serializable]
    public class PlayerProfile
    {
        public float Gold { get; set; } = 0;
        public float Energy { get; set; } = 0;
        public LevelCompletionInfo LevelCompletionInfo { get; set; } = new();
    }

    [Serializable]
    public class LevelCompletionInfo
    {
        public int LastLevel { get; set; } = 0;
        public List<int> LevelsRating { get; set; } = new();
    }
}
