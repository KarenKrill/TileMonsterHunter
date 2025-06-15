namespace TileMonsterHunter.Abstractions
{
    public enum GameState
    {
        Initial,
        SceneLoad, // Load scene (with progress bar), show arts/tips
        MainMenu, // New, load, settings, [exit]
        CutScene, // Show some plot intro or cutscene
        Gameplay,
        LevelEnd, // show cutscene, win/lose, next level, menu, exit
        GameEnd, // show statistics/outro/credits, exit to menu
        Pause,
        Exit
    }
}
