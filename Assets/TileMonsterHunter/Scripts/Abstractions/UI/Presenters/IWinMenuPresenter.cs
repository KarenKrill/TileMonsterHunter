#nullable enable

using System;

using KarenKrill.UI.Presenters.Abstractions;

namespace TileMonsterHunter.UI.Presenters.Abstractions
{
    using Views.Abstractions;

    public interface IWinMenuPresenter : IPresenter<IWinMenuView>
    {
        public event Action? Restart;
        public event Action? MainMenu;
        public event Action? Exit;
    }
}
