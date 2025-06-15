﻿using System;
using UnityEngine;
using UnityEngine.UI;

using KarenKrill.UI.Views;

namespace TileMonsterHunter.UI.Views
{
    using Abstractions;

    public class SettingsMenuView : ViewBehaviour, ISettingsMenuView
    {
        #region Diagnostic
        public bool ShowFps { get => _showFpsToggle.isOn; set => _showFpsToggle.isOn = value; }
        #endregion

#nullable enable
        public event Action? ApplyRequested;
        public event Action? CancelRequested;
#nullable restore

        [SerializeField]
        private Toggle _showFpsToggle;
        [SerializeField]
        private Button _applyButton;
        [SerializeField]
        private Button _cancelButton;

        private void OnEnable()
        {
            _applyButton.onClick.AddListener(OnApplyButtonClicked);
            _cancelButton.onClick.AddListener(OnCancelButtonClicked);
        }
        private void OnDisable()
        {
            _applyButton.onClick.RemoveListener(OnApplyButtonClicked);
            _cancelButton.onClick.RemoveListener(OnCancelButtonClicked);
        }

        private void OnApplyButtonClicked() => ApplyRequested?.Invoke();
        private void OnCancelButtonClicked() => CancelRequested?.Invoke();
    }
}