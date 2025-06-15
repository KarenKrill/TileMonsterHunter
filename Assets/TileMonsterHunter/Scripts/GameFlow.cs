using UnityEngine;
using UnityEngine.SceneManagement;

using KarenKrill.StateSystem.Abstractions;

namespace TileMonsterHunter
{
    using Abstractions;

    public class GameFlow : IGameFlow
    {
        public GameState State => _stateSwitcher.State;

        public GameFlow(IStateSwitcher<GameState> stateSwitcher,
            IPlayerProfileProvider playerProfileProvider)
        {
            _stateSwitcher = stateSwitcher;
            _playerProfileProvider = playerProfileProvider;
        }
        public void LoadMainMenu()
        {
            _loadSceneAwaiter = SceneManager.LoadSceneAsync("MenuScene", LoadSceneMode.Single);
            _loadSceneAwaiter.completed += OnMainMenuLoadCompleted;
            //_stateSwitcher.TransitTo(GameState.SceneLoad);
        }
        public void LoadLevel(long index)
        {
            throw new System.NotImplementedException();
        }
        public void Exit()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.ExitPlaymode();
#else // для мобильных и веба не сработает, надо предусмотреть
            Application.Quit();
#endif
        }
        public void StartGame()
        {
            _loadSceneAwaiter = SceneManager.LoadSceneAsync("LevelScene", LoadSceneMode.Single);
            _loadSceneAwaiter.completed += OnLevelLoadCompleted;
            //_stateSwitcher.TransitTo(GameState.SceneLoad);
        }
        public void RestartGame()
        {
            throw new System.NotImplementedException();
        }
        public void EndGame()
        {
            _stateSwitcher.TransitTo(GameState.GameEnd);
        }
        public void PlayLevel()
        {
            _stateSwitcher.TransitTo(GameState.Gameplay);
        }
        public void PauseLevel()
        {
            _stateSwitcher.TransitTo(GameState.Pause);
        }
        public void RestartLevel()
        {
            _stateSwitcher.TransitTo(GameState.Gameplay);
        }
        public void WinLevel()
        {
            EndLevel(1);
            _stateSwitcher.TransitTo(GameState.LevelEnd);
        }
        public void LoseLevel()
        {
            EndLevel(0);
            _stateSwitcher.TransitTo(GameState.LevelEnd);
        }

        private readonly IStateSwitcher<GameState> _stateSwitcher;
        private readonly IPlayerProfileProvider _playerProfileProvider;
        private AsyncOperation _loadSceneAwaiter;
        private void OnMainMenuLoadCompleted(AsyncOperation obj)
        {
            _stateSwitcher.TransitTo(GameState.MainMenu);
        }
        private void OnLevelLoadCompleted(AsyncOperation obj)
        {
            _stateSwitcher.TransitTo(GameState.Gameplay);
        }
        private void EndLevel(int rating)
        {
            var profile = _playerProfileProvider.CurrentProfile;
            if (profile.LevelCompletionInfo.LevelsRating.Count <= profile.LevelCompletionInfo.LastLevel)
            {
                profile.LevelCompletionInfo.LevelsRating.Add(rating);
            }
            else
            {
                profile.LevelCompletionInfo.LevelsRating[profile.LevelCompletionInfo.LastLevel] = rating;
            }
        }
    }
}
