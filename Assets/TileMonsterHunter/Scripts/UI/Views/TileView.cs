using UnityEngine;
using UnityEngine.UI;

namespace TileMonsterHunter.UI.Views
{
    using Abstractions;

    public class TileView : MonoBehaviour, ITileView
    {
        public Sprite ShapeSprite
        {
            set
            {
                if (value == null)
                {
                    _shapeImage.enabled = false;
                    _backShapeImage.enabled = false;
                }
                else
                {
                    _shapeImage.sprite = value;
                    _backShapeImage.sprite = value;
                    _shapeImage.enabled = true;
                    _backShapeImage.enabled = true;
                }
            }
        }
        public Sprite FaceSprite
        {
            set
            {
                if (value == null)
                {
                    _faceImage.enabled = false;
                }
                else
                {
                    _faceImage.sprite = value;
                    _faceImage.enabled = true;
                }
            }
        }
        public Color Color { set => _shapeImage.color = value; }

        [SerializeField]
        private Image _shapeImage;
        [SerializeField]
        private Image _backShapeImage;
        [SerializeField]
        private Image _faceImage;
    }
}
