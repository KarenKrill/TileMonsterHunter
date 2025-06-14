using System.Collections.Generic;
using UnityEngine;

namespace TileMonsterHunter
{
    [CreateAssetMenu(fileName = nameof(TileRepository), menuName = nameof(TileMonsterHunter) + "/" + nameof(TileRepository))]
    public class TileRepository : ScriptableObject, ITileRepository
    {
        [field: SerializeField]
        public List<Sprite> FaceSprites { get; set; } = new();
        [field: SerializeField]
        public List<Sprite> ShapeSprites { get; set; } = new();
        [field: SerializeField]
        public List<Color> Colors { get; set; } = new();
        [field: SerializeField]
        public PhysicsMaterial2D DefaultPhysicsMaterial { get; set; } = null;
        [field: SerializeField]
        public float DefaultMass { get; set; } = 1;
    }
}
