#nullable enable

using KarenKrill.UI.Views.Abstractions;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace TileMonsterHunter.UI.Views.Abstractions
{
    public interface IGameUIView : IView
    {
        public int TilesHotBarLength { set; }
        public event Action? RefreshRequested;

        public void SetHotBarTile(int index, Sprite shapeSprite, Sprite faceSprite, Color color);
    }
    public interface ITileView
    {
        public Sprite ShapeSprite { set; }
        public Sprite FaceSprite { set; }
        public Color Color { set; }
    }
}