using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace TileMonsterHunter
{
    using Abstractions;

    public class TileField : ITileField
    {
        public IReadOnlyList<TileInfo> Tiles => _tiles.AsReadOnly();
        public event System.Action<TileInfo> TileChosen;
        public event System.Action<TileInfo> TileRemoved;

        public void Generate(TileFieldGenerationOptions options)
        {
            _tilesMap.Clear();
            var tilesMatchGroupCount = options.MaxTilesCount / options.MatchCount;
            for (int i = 0; i < tilesMatchGroupCount; i++)
            {
                var shapeIndex = Random.Range(0, options.TileRepository.ShapeSprites.Count);
                var shapeSprite = options.TileRepository.ShapeSprites[shapeIndex];
                var faceIndex = Random.Range(0, options.TileRepository.FaceSprites.Count);
                var faceSprite = options.TileRepository.FaceSprites[faceIndex];
                var colorIndex = Random.Range(0, options.TileRepository.Colors.Count);
                var color = options.TileRepository.Colors[colorIndex];
                for (int j = 0; j < options.MatchCount; j++)
                {
                    var id = i * options.MatchCount + j;
                    _tilesMap[id] = new TileInfo(id, shapeSprite, faceSprite, color, options.TileRepository.DefaultPhysicsMaterial, options.TileRepository.DefaultMass);
                }
            }
            MixTiles();
        }
        public void ChooseTile(int id)
        {
            _choosenTile = _tilesMap[id];
            TileChosen?.Invoke(_choosenTile);
        }
        public void RemoveTile(int id)
        {
            if (_tilesMap.Remove(id, out var tileInfo))
            {
                MixTiles();
                TileRemoved?.Invoke(_choosenTile);
            }
        }

        private readonly Dictionary<int, TileInfo> _tilesMap = new();
        private List<TileInfo> _tiles = new();
        private TileInfo _choosenTile = null;
        private void MixTiles()
        {
            _tiles = _tilesMap.Values.OrderBy(item => Random.Range(0, item.ID)).ToList();
        }
    }
}
