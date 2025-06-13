using UnityEngine;
using UnityEngine.SceneManagement;

using KarenKrill.StateSystem.Abstractions;

namespace TileMonsterHunter
{
    using Abstractions;

    public class GameFlow : IGameFlow
    {
        public GameState State => _stateSwitcher.State;

        public GameFlow(IStateSwitcher<GameState> stateSwitcher)
        {
            _stateSwitcher = stateSwitcher;
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
            throw new System.NotImplementedException();
        }
        public void WinLevel()
        {
            throw new System.NotImplementedException();
        }
        public void LoseLevel()
        {
            throw new System.NotImplementedException();
        }

        private readonly IStateSwitcher<GameState> _stateSwitcher;
        private AsyncOperation _loadSceneAwaiter;
        private void OnMainMenuLoadCompleted(AsyncOperation obj)
        {
            _stateSwitcher.TransitTo(GameState.MainMenu);
        }
        private void OnLevelLoadCompleted(AsyncOperation obj)
        {
            _stateSwitcher.TransitTo(GameState.Gameplay);
        }
    }
}
