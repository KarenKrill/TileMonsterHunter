#nullable enable

using System;

using KarenKrill.UI.Views.Abstractions;

namespace TileMonsterHunter.UI.Views.Abstractions
{
    public interface IGameUIView : IView
    {
        public event Action? RefreshRequested;
    }
}