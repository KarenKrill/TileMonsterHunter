using UnityEngine;
using KarenKrill.StateSystem.Abstractions;

namespace TileMonsterHunter.GameStates
{
    using Abstractions;
    using Input.Abstractions;
    using States;
    using UI.Presenters.Abstractions;

    public class LevelEndState : PresentableStateHandlerBase<GameState>, IStateHandler<GameState>
    {
        public override GameState State => GameState.LevelEnd;

        public LevelEndState(ILogger logger,
            IGameFlow gameFlow,
            IInputActionService inputService,
            IPlayerProfileProvider profileProvider,
            IWinMenuPresenter winMenuPresenter,
            ILoseMenuPresenter loseMenuPresenter)
        {
            _logger = logger;
            _gameFlow = gameFlow;
            _inputService = inputService;
            _playerProfileProvider = profileProvider;
            _winMenuPresenter = winMenuPresenter;
            _loseMenuPresenter = loseMenuPresenter;
        }
        public override void Enter(GameState prevState)
        {
            _winMenuPresenter.Exit += OnExit;
            _loseMenuPresenter.Exit += OnExit;
            _winMenuPresenter.Restart += OnRestart;
            _loseMenuPresenter.Restart += OnRestart;
            _winMenuPresenter.MainMenu += OnMainMenu;
            _loseMenuPresenter.MainMenu += OnMainMenu;
            base.Enter(prevState);
            var profile = _playerProfileProvider.CurrentProfile;
            if (profile.LevelCompletionInfo.LevelsRating[profile.LevelCompletionInfo.LastLevel] == 0)
            {
                _loseMenuPresenter.Enable();
            }
            else
            {
                _winMenuPresenter.Enable();
                //profile.LevelCompletionInfo.LastLevel++;
            }
            _inputService.SetActionMap(ActionMap.UI);
            _logger.Log($"{nameof(MainMenuState)}.{nameof(Enter)}()");
        }
        public override void Exit(GameState nextState)
        {
            _winMenuPresenter.Exit -= OnExit;
            _loseMenuPresenter.Exit -= OnExit;
            _winMenuPresenter.Restart -= OnRestart;
            _loseMenuPresenter.Restart -= OnRestart;
            _winMenuPresenter.MainMenu -= OnMainMenu;
            _loseMenuPresenter.MainMenu -= OnMainMenu;
            base.Exit(nextState);
            _winMenuPresenter.Disable();
            _loseMenuPresenter.Disable();
            _logger.Log($"{nameof(MainMenuState)}.{nameof(Exit)}()");
        }

        private readonly ILogger _logger;
        private readonly IGameFlow _gameFlow;
        private readonly IInputActionService _inputService;
        private readonly IPlayerProfileProvider _playerProfileProvider;
        private readonly IWinMenuPresenter _winMenuPresenter;
        private readonly ILoseMenuPresenter _loseMenuPresenter;

        private void OnRestart()
        {
            _gameFlow.RestartLevel();
        }
        private void OnMainMenu()
        {
            _gameFlow.LoadMainMenu();
        }
        private void OnExit()
        {
            _gameFlow.Exit();
        }
    }
}
