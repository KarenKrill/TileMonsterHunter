using System;
using UnityEngine;
using UnityEngine.UI;

using KarenKrill.UI.Views;

namespace TileMonsterHunter.UI.Views
{
    using Abstractions;

    public class WinMenuView : ViewBehaviour, IWinMenuView
    {
#nullable enable
        public event Action? RestartRequested;
        public event Action? MainMenuExitRequested;
        public event Action? ExitRequested;
#nullable restore

        [SerializeField]
        private Button _restartButton;
        [SerializeField]
        private Button _mainMenuExitButton;
        [SerializeField]
        private Button _exitButton;

        private void OnEnable()
        {
            _restartButton.onClick.AddListener(OnRestartButtonClicked);
            _mainMenuExitButton.onClick.AddListener(OnMainMenuExitButtonClicked);
            _exitButton.onClick.AddListener(OnExitButtonClicked);
        }
        private void OnDisable()
        {
            _restartButton.onClick.RemoveListener(OnRestartButtonClicked);
            _mainMenuExitButton.onClick.RemoveListener(OnMainMenuExitButtonClicked);
            _exitButton.onClick.RemoveListener(OnExitButtonClicked);
        }

        private void OnRestartButtonClicked() => RestartRequested?.Invoke();
        private void OnMainMenuExitButtonClicked() => MainMenuExitRequested?.Invoke();
        private void OnExitButtonClicked() => ExitRequested?.Invoke();
    }
}