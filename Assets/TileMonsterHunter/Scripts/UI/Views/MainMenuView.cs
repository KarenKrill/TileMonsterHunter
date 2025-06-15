using System;
using UnityEngine;
using UnityEngine.UI;

using KarenKrill.UI.Views;

namespace TileMonsterHunter.UI.Views
{
    using Abstractions;

    public class MainMenuView : ViewBehaviour, IMainMenuView
    {
#nullable enable
        public event Action? NewGameRequested;
        public event Action? SettingsOpenRequested;
        public event Action? ExitRequested;
#nullable restore

        [SerializeField]
        private Button _newGameButton;
        [SerializeField]
        private Button _settingsButton;
        [SerializeField]
        private Button _exitButton;

        private void OnEnable()
        {
            _newGameButton.onClick.AddListener(OnNewGameButtonClicked);
            _settingsButton.onClick.AddListener(OnSettingsButtonClicked);
            _exitButton.onClick.AddListener(OnExitButtonClicked);
        }
        private void OnDisable()
        {
            _newGameButton.onClick.RemoveListener(OnNewGameButtonClicked);
            _settingsButton.onClick.RemoveListener(OnSettingsButtonClicked);
            _exitButton.onClick.RemoveListener(OnExitButtonClicked);
        }

        private void OnNewGameButtonClicked() => NewGameRequested?.Invoke();
        private void OnSettingsButtonClicked() => SettingsOpenRequested?.Invoke();
        private void OnExitButtonClicked() => ExitRequested?.Invoke();
    }
}