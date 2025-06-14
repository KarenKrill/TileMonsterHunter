using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using KarenKrill.UI.Presenters.Abstractions;
using KarenKrill.UI.Views.Abstractions;

namespace TileMonsterHunter.UI.Presenters
{
    using Abstractions;
    using TileMonsterHunter.Abstractions;
    using Views.Abstractions;

    public class GameUIPresenter : PresenterBase<IGameUIView>, IGameUIPresenter, IPresenter<IGameUIView>
    {
#nullable enable
        public event Action? RefreshRequested;
#nullable restore

        public GameUIPresenter(IViewFactory viewFactory,
            IPresenterNavigator navigator,
            ITileHotBar tileHotBar) : base(viewFactory, navigator)
        {
            _tileHotBar = tileHotBar;
        }

        protected override void Subscribe()
        {
            View.RefreshRequested += OnRefreshRequested;
            _tileHotBar.MaxLengthChanged += OnHotBarMaxLengthChanged;
            _tileHotBar.TileAdded += OnHotBarTileAdded;
            _tileHotBar.TilesRemoved += OnHotBarTilesRemoved;
            _tileHotBar.TilesCleared += OnHotBarTilesCleared;
        }
        protected override void Unsubscribe()
        {
            View.RefreshRequested -= OnRefreshRequested;
            _tileHotBar.MaxLengthChanged -= OnHotBarMaxLengthChanged;
            _tileHotBar.TileAdded -= OnHotBarTileAdded;
            _tileHotBar.TilesRemoved -= OnHotBarTilesRemoved;
        }

        private readonly ITileHotBar _tileHotBar;

        private readonly Dictionary<TileInfo, int> _hotBarTileIndices = new();
        private int _lastHotBarIndex = -1;
        private void OnRefreshRequested() => RefreshRequested?.Invoke();
        private void OnHotBarMaxLengthChanged(int maxLength)
        {
            View.TilesHotBarLength = maxLength;
        }
        private void OnHotBarTileAdded(TileInfo tileInfo)
        {
            _hotBarTileIndices[tileInfo] = ++_lastHotBarIndex;
            View.SetHotBarTile(_lastHotBarIndex, tileInfo.ShapeSprite, tileInfo.FaceSprite, tileInfo.Color);
        }
        private void OnHotBarTilesRemoved(TileInfo[] tilesInfo)
        {
            foreach (var tileInfo in tilesInfo)
            {
                var index = _hotBarTileIndices[tileInfo];
                View.SetHotBarTile(index, null, null, Color.clear);
                _hotBarTileIndices.Remove(tileInfo);
            }
            RefreshIndices();
        }
        private void OnHotBarTilesCleared()
        {
            foreach (var tileIndex in _hotBarTileIndices.Values)
            {
                View.SetHotBarTile(tileIndex, null, null, Color.clear);
            }
            _hotBarTileIndices.Clear();
            _lastHotBarIndex = -1;
        }

        private void RefreshIndices()
        {
            // Clear old positions:
            foreach (var index in _hotBarTileIndices.Values)
            {
                View.SetHotBarTile(index, null, null, Color.clear);
            }
            var tilesInfo = _hotBarTileIndices.Keys.ToList();
            _hotBarTileIndices.Clear();
            _lastHotBarIndex = -1;
            // Show new positions:
            foreach (var tileInfo in tilesInfo)
            {
                _hotBarTileIndices[tileInfo] = ++_lastHotBarIndex;
                View.SetHotBarTile(_lastHotBarIndex, tileInfo.ShapeSprite, tileInfo.FaceSprite, tileInfo.Color);
            }
        }
    }
}