namespace TileMonsterHunter.Abstractions
{
    public interface IGameFlow
    {
        GameState State { get; }

        void LoadMainMenu();
        void LoadLevel(long index);
        void Exit();
        void StartGame();
        void RestartGame();
        void EndGame();
        void PlayLevel();
        void PauseLevel();
        void RestartLevel();
        void WinLevel();
        void LoseLevel();
    }
}
