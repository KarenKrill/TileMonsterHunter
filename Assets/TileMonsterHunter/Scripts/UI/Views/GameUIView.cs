using System;
using UnityEngine;
using UnityEngine.UI;

using KarenKrill.UI.Views;

namespace TileMonsterHunter.UI.Views
{    
    using Abstractions;

    public class GameUIView : ViewBehaviour, IGameUIView
    {
#nullable enable
        public event Action? RefreshRequested;
#nullable restore

        [SerializeField]
        private Button _refreshButton;

        private void OnEnable()
        {
            _refreshButton.onClick.AddListener(OnRefreshButtonClicked);
        }
        private void OnDisable()
        {
            _refreshButton.onClick.RemoveListener(OnRefreshButtonClicked);
        }

        private void OnRefreshButtonClicked() => RefreshRequested?.Invoke();
    }
}