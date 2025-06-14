using System;
using System.Text;
using System.Collections.Generic;
using UnityEngine;

namespace TileMonsterHunter
{
    using Abstractions;

    public class TileHotBar : ITileHotBar
    {
        /// <summary></summary>
        /// <remarks>
        /// This isn't a real ReadOnlyList, please don't cast it in List :D
        /// </remarks>
        public IReadOnlyList<TileInfo> Tiles => _tiles;
        public IReadOnlyCollection<TileMatchKey> TileMatchKeys => _tilesByMatchKey.Keys;
        public int MaxLength => _maxLength;

        public event Action Overflowed;
        public event Action<TileInfo> TileAdded;
        public event Action<TileInfo[]> TilesRemoved;
        public event Action TilesCleared;
        public event Action<int> MaxLengthChanged;

        public void Reset(int length)
        {
            _maxLength = length;
            Clear();
            MaxLengthChanged?.Invoke(_maxLength);
            ShowBarDebug();
        }
        public void AddTile(TileInfo tileInfo)
        {
            if (_tiles.Count < _maxLength)
            {
                AddTileInternal(tileInfo);
                TileAdded?.Invoke(tileInfo);
            }
            if (_tiles.Count >= _maxLength)
            {
                Overflowed?.Invoke();
            }
            ShowBarDebug();
        }
        /// <summary>
        /// Removes tiles from hot bar
        /// </summary>
        /// <param name="tilesInfo">Existing in hot bar tiles info (if not exist, can cause undefine behaviour)</param>
        public void RemoveTiles(params TileInfo[] tilesInfo)
        {
            RemoveTilesInternal(tilesInfo);
            TilesRemoved?.Invoke(tilesInfo);
            ShowBarDebug();
        }
        public void Clear()
        {
            ClearTilesInternal();
            TilesCleared?.Invoke();
        }

        /// <summary></summary>
        /// <remarks>
        /// This isn't a real ReadOnlyList, please don't cast it in List :D
        /// </remarks>
        public IReadOnlyList<TileInfo> GetTilesByMatchKey(TileMatchKey key) => _tilesByMatchKey[key];

        private void AddTileInternal(TileInfo tileInfo)
        {
            _tiles.Add(tileInfo);
            // Add tile to tiles by match key
            var matchKey = new TileMatchKey(tileInfo);
            if (!_tilesByMatchKey.TryGetValue(matchKey, out var tileList))
            {
                tileList = new();
                _tilesByMatchKey[matchKey] = tileList;
            }
            tileList.Add(tileInfo);
        }
        private void RemoveTilesInternal(TileInfo[] tilesInfo)
        {
            foreach (var tileInfo in tilesInfo)
            {
                _tiles.Remove(tileInfo);
                var matchKey = new TileMatchKey(tileInfo);
                _tilesByMatchKey[matchKey].Remove(tileInfo);
            }
        }
        private void ClearTilesInternal()
        {
            _tiles.Clear();
            _tilesByMatchKey.Clear();
        }
        private void ShowBarDebug()
        {
            StringBuilder sb = new();
            for (int i = 0; i < _maxLength; i++)
            {
                if (i < _tiles.Count)
                {
                    sb.Append($"{_tiles[i].ShapeSprite.name}.{_tiles[i].FaceSprite.name}.{_tiles[i].Color}");
                }
                else
                {
                    sb.Append("***");
                }
                sb.Append(" | ");
            }
            Debug.Log(sb.ToString());
        }

        private readonly List<TileInfo> _tiles = new();
        private readonly Dictionary<TileMatchKey, List<TileInfo>> _tilesByMatchKey = new();
        private int _maxLength = 0;
    }
}
