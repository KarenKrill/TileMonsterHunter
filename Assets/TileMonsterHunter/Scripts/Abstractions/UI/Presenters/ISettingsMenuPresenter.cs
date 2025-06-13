#nullable enable

using System;

using KarenKrill.UI.Presenters.Abstractions;

namespace TileMonsterHunter.UI.Presenters.Abstractions
{
    using Views.Abstractions;

    public interface ISettingsMenuPresenter : IPresenter<ISettingsMenuView>
    {
        public event Action? Close;
    }
}
