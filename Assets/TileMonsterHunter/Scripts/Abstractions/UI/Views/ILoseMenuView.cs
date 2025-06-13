#nullable enable

using System;

using KarenKrill.UI.Views.Abstractions;

namespace TileMonsterHunter.UI.Views.Abstractions
{
    public interface ILoseMenuView : IView
    {
        public event Action? RestartRequested;
        public event Action? MainMenuExitRequested;
        public event Action? ExitRequested;
    }
}