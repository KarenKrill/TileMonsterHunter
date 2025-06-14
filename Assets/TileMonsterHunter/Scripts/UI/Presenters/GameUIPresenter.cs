using System;

using KarenKrill.UI.Presenters.Abstractions;
using KarenKrill.UI.Views.Abstractions;

namespace TileMonsterHunter.UI.Presenters
{
    using Abstractions;
    using Views.Abstractions;

    public class GameUIPresenter : PresenterBase<IGameUIView>, IGameUIPresenter, IPresenter<IGameUIView>
    {
#nullable enable
        public event Action? RefreshRequested;
#nullable restore

        public GameUIPresenter(IViewFactory viewFactory,
            IPresenterNavigator navigator) : base(viewFactory, navigator)
        {
        }

        protected override void Subscribe()
        {
            View.RefreshRequested += OnRefreshRequested;
        }
        protected override void Unsubscribe()
        {
            View.RefreshRequested -= OnRefreshRequested;
        }

        private void OnRefreshRequested() => RefreshRequested?.Invoke();
    }
}