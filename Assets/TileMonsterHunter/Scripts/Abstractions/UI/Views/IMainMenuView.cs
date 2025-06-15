#nullable enable

using System;

using KarenKrill.UI.Views.Abstractions;

namespace TileMonsterHunter.UI.Views.Abstractions
{
    public interface IMainMenuView : IView
    {
        public event Action? NewGameRequested;
        public event Action? SettingsOpenRequested;
        public event Action? ExitRequested;
    }
}