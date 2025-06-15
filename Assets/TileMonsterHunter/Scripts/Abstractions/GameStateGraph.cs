using System.Collections.Generic;

using KarenKrill.StateSystem.Abstractions;

namespace TileMonsterHunter.Abstractions
{
    public class GameStateGraph : IStateGraph<GameState>
    {
        public GameState InitialState => GameState.Initial;
        public IDictionary<GameState, IList<GameState>> Transitions => _transitions;

        private readonly IDictionary<GameState, IList<GameState>> _transitions = new Dictionary<GameState, IList<GameState>>()
        {
            // Some transitions are temporary (due to unimplemented SceneLoad)
            { GameState.Initial, new List<GameState> { GameState.SceneLoad, GameState.MainMenu } },
            { GameState.SceneLoad, new List<GameState> { GameState.MainMenu, GameState.CutScene, GameState.Gameplay } },
            { GameState.MainMenu, new List<GameState> { GameState.SceneLoad, GameState.Exit, GameState.Gameplay } },
            { GameState.CutScene, new List<GameState> { GameState.SceneLoad, GameState.Gameplay, GameState.MainMenu } },
            { GameState.Gameplay, new List<GameState> { GameState.Pause, GameState.CutScene, GameState.LevelEnd } },
            { GameState.LevelEnd, new List<GameState> { GameState.SceneLoad, GameState.CutScene, GameState.MainMenu, GameState.GameEnd, GameState.Exit, GameState.Gameplay } },
            { GameState.GameEnd, new List<GameState> { GameState.SceneLoad, GameState.CutScene, GameState.MainMenu, GameState.Exit } },
            { GameState.Pause, new List<GameState> { GameState.Gameplay, GameState.MainMenu, GameState.Exit, GameState.SceneLoad } },
            { GameState.Exit, new List<GameState>() }
        };
    }
}
