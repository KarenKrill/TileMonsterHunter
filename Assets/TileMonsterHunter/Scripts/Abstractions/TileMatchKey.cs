using UnityEngine;

namespace TileMonsterHunter.Abstractions
{
    public struct TileMatchKey
    {
        public Sprite ShapeSprite { get; }
        public Sprite FaceSprite { get; }
        public Color Color { get; }
        public TileMatchKey(Sprite shapeSprite, Sprite faceSprite, Color color)
        {
            ShapeSprite = shapeSprite;
            FaceSprite = faceSprite;
            Color = color;
        }
        public TileMatchKey(TileInfo tileInfo)
        {
            ShapeSprite = tileInfo.ShapeSprite;
            FaceSprite = tileInfo.FaceSprite;
            Color = tileInfo.Color;
        }
    }
}
