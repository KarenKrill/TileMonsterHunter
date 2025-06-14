using System.Linq;
using UnityEngine;

using KarenKrill.StateSystem.Abstractions;

namespace TileMonsterHunter.GameStates
{
    using Abstractions;
    using Input.Abstractions;
    using States;
    using UI.Presenters.Abstractions;

    public class GameplayState : PresentableStateHandlerBase<GameState>, IStateHandler<GameState>
    {
        public override GameState State => GameState.Gameplay;

        public GameplayState(ILogger logger,
            IGameFlow gameFlow,
            IInputActionService inputService,
            IPlayerProfileProvider playerProfileProvider,
            ITileField tileField,
            ITileHotBar tileHotBar,
            ITileRepository tileRepository,
            ITileFieldSpawner tileFieldSpawner,
            IGameUIPresenter gameUIPresenter) : base(gameUIPresenter)
        {
            _logger = logger;
            _gameFlow = gameFlow;
            _inputService = inputService;
            _playerProfileProvider = playerProfileProvider;
            _tileField = tileField;
            _tileHotBar = tileHotBar;
            _tileFieldSpawner = tileFieldSpawner;
            _tileRepository = tileRepository;
            _gameUIPresenter = gameUIPresenter;
        }
        public override void Enter(GameState prevState)
        {
            base.Enter(prevState);

            _inputService.Disable();
            _inputService.Pause += OnPause;
            _gameUIPresenter.RefreshRequested += OnRefreshRequested;
            _tileField.TileChosen += OnTileChosen;
            _tileFieldSpawner.SpawnCompleted += OnTileFieldSpawnCompleted;
            _tileHotBar.Overflowed += OnHotBarOverflowed;
            _tileHotBar.TileAdded += OnHotBarTileAdded;

            _logger.Log($"{nameof(MainMenuState)}.{nameof(Enter)}()");
            if (prevState != GameState.Pause)
            {
                OnLevelStarts();
            }
        }
        public override void Exit(GameState nextState)
        {
            base.Exit(nextState);

            _inputService.Pause -= OnPause;
            _gameUIPresenter.RefreshRequested -= OnRefreshRequested;
            _tileField.TileChosen -= OnTileChosen;
            _tileFieldSpawner.SpawnCompleted -= OnTileFieldSpawnCompleted;
            _tileHotBar.Overflowed -= OnHotBarOverflowed;
            _tileHotBar.TileAdded -= OnHotBarTileAdded;
            _inputService.SetActionMap(ActionMap.UI);
            _logger.Log($"{nameof(MainMenuState)}.{nameof(Exit)}()");
        }

        private readonly ILogger _logger;
        private readonly IGameFlow _gameFlow;
        private readonly IInputActionService _inputService;
        private readonly IPlayerProfileProvider _playerProfileProvider;
        private readonly ITileField _tileField;
        private readonly ITileHotBar _tileHotBar;
        private readonly ITileFieldSpawner _tileFieldSpawner;
        private readonly ITileRepository _tileRepository;
        private readonly IGameUIPresenter _gameUIPresenter;

        private void OnPause()
        {
            _gameFlow.PauseLevel();
        }
        private void OnLevelStarts()
        {
            GenerateLevel();
        }
        private void OnTileChosen(TileInfo tileInfo)
        {
            _tileField.RemoveTile(tileInfo.ID);
            _tileHotBar.AddTile(tileInfo);
        }
        private void OnRefreshRequested()
        {
            GenerateLevel();
        }
        private void OnTileFieldSpawnCompleted()
        {
            _inputService.SetActionMap(ActionMap.Player);
        }
        private void OnHotBarTileAdded(TileInfo tileInfo)
        {
            RemoveMatchTiles();
            //Task.Run(async () => await RemoveMatchTilesAsync());
        }
        private void RemoveMatchTiles()
        //private async Task RemoveMatchTilesAsync()
        {
            //await Task.Delay(1000);
            var matchCount = 3;
            foreach (var matchKey in _tileHotBar.TileMatchKeys)
            {
                var matchesList = _tileHotBar.GetTilesByMatchKey(matchKey);
                if (matchesList.Count >= matchCount)
                {
                    _tileHotBar.RemoveTiles(matchesList.ToArray()[0..matchCount]);
                }
            }
            FinishGameIfTileFieldEmpty();
        }
        private void FinishGameIfTileFieldEmpty()
        {
            if (_tileField.Tiles.Count == 0)
            {
                bool isWin = _tileHotBar.Tiles.Count == 0;
                FinishGame(isWin);
            }
        }
        private void FinishGame(bool isWin)
        {
            if (isWin)
            {
                _gameFlow.WinLevel();
            }
            else
            {
                _gameFlow.LoseLevel();
            }
        }
        private void OnHotBarOverflowed()
        {
            _gameFlow.LoseLevel();
        }
        private void GenerateLevel()
        {
            var lastLevel = _playerProfileProvider.CurrentProfile.LevelCompletionInfo.LastLevel;
            var tilesCount = (lastLevel + 1) * 20; // TODO: replace by LevelInfoProvider, which configured by SO
            var matchCount = 3;
            var options = new TileFieldGenerationOptions(_tileRepository, tilesCount, matchCount);
            _tileHotBar.Reset(7);
            _tileField.Generate(options);
            _tileFieldSpawner.Spawn(_tileField);
        }
    }
}