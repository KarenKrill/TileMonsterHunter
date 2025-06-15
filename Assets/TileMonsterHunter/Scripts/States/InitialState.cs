﻿using UnityEngine;

using KarenKrill.StateSystem.Abstractions;
using KarenKrill.UI.Presenters.Abstractions;

namespace TileMonsterHunter.GameStates
{
    using Abstractions;
    using UI.Views.Abstractions;

    public class InitialState : IStateHandler<GameState>
    {
        public GameState State => GameState.Initial;

        public InitialState(ILogger logger,
            IGameFlow gameFlow,
            IPresenter<IDiagnosticInfoView> diagnosticInfoPresenter,
            GameSettings gameSettings)
        {
            _logger = logger;
            _gameFlow = gameFlow;
            _diagnosticInfoPresenter = diagnosticInfoPresenter;
            gameSettings.ShowFpsChanged += OnShowFpsChanged;
        }
        public void Enter(GameState prevState)
        {
            _logger.Log($"{nameof(InitialState)}.{nameof(Enter)}()");
            _gameFlow.LoadMainMenu();
        }
        public void Exit(GameState nextState)
        {
            _logger.Log($"{nameof(InitialState)}.{nameof(Exit)}()");
        }

        private readonly ILogger _logger;
        private readonly IGameFlow _gameFlow;
        private readonly IPresenter<IDiagnosticInfoView> _diagnosticInfoPresenter;

        private void OnShowFpsChanged(bool state)
        {
            if (state)
            {
                _diagnosticInfoPresenter.Enable();
            }
            else
            {
                _diagnosticInfoPresenter.Disable();
            }
        }
    }
}