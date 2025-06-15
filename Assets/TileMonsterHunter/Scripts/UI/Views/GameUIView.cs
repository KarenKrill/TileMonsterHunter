using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using KarenKrill.UI.Views;

namespace TileMonsterHunter.UI.Views
{    
    using Abstractions;

    public class GameUIView : ViewBehaviour, IGameUIView
    {
        public int TilesHotBarLength
        {
            set
            {
                foreach (var tileView in _tileViews)
                {
                    tileView.ShapeSprite = null;
                    tileView.FaceSprite = null;
                    tileView.Color = Color.clear;
                    tileView.gameObject.SetActive(false);
                }
                for (int i = 0; i < value && i < _tileViews.Count; i++)
                {
                    _tileViews[i].gameObject.SetActive(true);
                }
            }
        }

#nullable enable
        public event Action? RefreshRequested;
#nullable restore

        [SerializeField]
        private Button _refreshButton;
        [SerializeField]
        private List<TileView> _tileViews = new();

        private void OnEnable()
        {
            _refreshButton.onClick.AddListener(OnRefreshButtonClicked);
        }
        private void OnDisable()
        {
            _refreshButton.onClick.RemoveListener(OnRefreshButtonClicked);
        }

        private void OnRefreshButtonClicked() => RefreshRequested?.Invoke();
        public void SetHotBarTile(int index, Sprite shapeSprite, Sprite faceSprite, Color color)
        {
            _tileViews[index].ShapeSprite = shapeSprite;
            _tileViews[index].FaceSprite = faceSprite;
            _tileViews[index].Color = color;
        }
    }
}