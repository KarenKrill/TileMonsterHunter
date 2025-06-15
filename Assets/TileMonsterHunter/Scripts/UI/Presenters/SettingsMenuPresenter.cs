﻿using System;

using KarenKrill.UI.Presenters.Abstractions;
using KarenKrill.UI.Views.Abstractions;

namespace TileMonsterHunter.UI.Presenters
{
    using Abstractions;
    using TileMonsterHunter.Abstractions;
    using Views.Abstractions;

    public class SettingsMenuPresenter : PresenterBase<ISettingsMenuView>, ISettingsMenuPresenter, IPresenter<ISettingsMenuView>
    {
#nullable enable
        public event Action? Close;
#nullable restore

        public SettingsMenuPresenter(IViewFactory viewFactory,
            IPresenterNavigator navigator,
            GameSettings gameSettings) : base(viewFactory, navigator)
        {
            _gameSettings = gameSettings;
        }

        protected override void Subscribe()
        {
            _gameSettings.ShowFpsChanged += OnModelShowFpsChanged;
            View.ShowFps = _gameSettings.ShowFps;
            View.ApplyRequested += OnApply;
            View.CancelRequested += OnCancel;
        }
        protected override void Unsubscribe()
        {
            _gameSettings.ShowFpsChanged -= OnModelShowFpsChanged;
            View.ApplyRequested -= OnApply;
            View.CancelRequested -= OnCancel;
        }

        private readonly GameSettings _gameSettings;

        private void OnModelShowFpsChanged(bool state) => View.ShowFps = state;
        private void OnApply()
        {
            _gameSettings.ShowFps = View.ShowFps;
            Close?.Invoke();
        }
        private void OnCancel() => Close?.Invoke();
    }
}