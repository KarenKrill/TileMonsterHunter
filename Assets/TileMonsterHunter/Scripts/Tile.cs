using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Sprite ShapeSprite
    {
        get => _shapeSprite;
        set
        {
            if (_shapeSprite != value)
            {
                _shapeSprite = value;
                OnShapeSpriteChanged();
            }
        }
    }
    public Sprite FaceSprite
    {
        get => _faceSprite;
        set
        {
            if (_faceSprite != value)
            {
                _faceSprite = value;
                OnFaceSpriteChanged();
            }
        }
    }
    public Color Color
    {
        get => _color;
        set
        {
            if (_color != value)
            {
                _color = value;
                OnColorChanged();
            }
        }
    }

    [SerializeField]
    private Sprite _shapeSprite;
    [SerializeField]
    private Sprite _faceSprite;
    [SerializeField]
    private Color _color;
    [SerializeField]
    private SpriteRenderer _colorShapeSpriteRenderer;
    [SerializeField]
    private SpriteRenderer _backShapeSpriteRenderer;
    [SerializeField]
    private SpriteRenderer _faceSpriteRenderer;
    [SerializeField]
    private PolygonCollider2D _shapePolygonCollider;

    private readonly List<Vector2> _currPhysicsShape = new();

    private void OnValidate()
    {
        // It is not known which field exactly changed
        OnShapeSpriteChanged();
        OnFaceSpriteChanged();
        OnColorChanged();
    }
    private void OnShapeSpriteChanged()
    {
        if (_shapeSprite != null)
        {
            _colorShapeSpriteRenderer.sprite = _shapeSprite;
            _shapePolygonCollider.pathCount = 0;
            var shapesCount = _shapeSprite.GetPhysicsShapeCount();
            if (shapesCount > 0)
            {
                _currPhysicsShape.Clear();
                _ = _shapeSprite.GetPhysicsShape(0, _currPhysicsShape);
                _shapePolygonCollider.SetPath(0, _currPhysicsShape);
            }
            _backShapeSpriteRenderer.sprite = _shapeSprite;
        }
    }
    private void OnFaceSpriteChanged()
    {
        if (_faceSprite != null)
        {
            _faceSpriteRenderer.sprite = _faceSprite;
        }
    }
    private void OnColorChanged()
    {
        if (_color != null)
        {
            _colorShapeSpriteRenderer.color = _color;
        }
    }
}
