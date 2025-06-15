using UnityEngine;

namespace TileMonsterHunter.Abstractions
{
    public class TileInfo
    {
        public int ID { get; }
        public Sprite ShapeSprite { get; }
        public Sprite FaceSprite { get; }
        public Color Color { get; }
        public PhysicsMaterial2D PhysicsMaterial { get; }
        public float Mass { get; }
        public TileInfo(int id, Sprite shapeSprite, Sprite faceSprite, Color color, PhysicsMaterial2D physicsMaterial, float mass)
        {
            ID = id;
            ShapeSprite = shapeSprite;
            FaceSprite = faceSprite;
            Color = color;
            PhysicsMaterial = physicsMaterial;
            Mass = mass;
        }
    }
}
