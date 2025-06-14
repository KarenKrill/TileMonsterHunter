using System.Collections.Generic;
using UnityEngine;

public interface ITileRepository
{
    List<Sprite> FaceSprites { get; }
    List<Sprite> ShapeSprites { get; }
    List<Color> Colors { get; }
    PhysicsMaterial2D DefaultPhysicsMaterial { get; }
    float DefaultMass { get; }
}
