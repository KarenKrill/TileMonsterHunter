﻿using KarenKrill.Diagnostics.Abstractions;
using KarenKrill.UI.Presenters.Abstractions;
using KarenKrill.UI.Views.Abstractions;

namespace TileMonsterHunter.UI.Presenters
{
    using Views.Abstractions;

    public class DiagnosticInfoPresenter : PresenterBase<IDiagnosticInfoView>, IPresenter<IDiagnosticInfoView>
    {
        public DiagnosticInfoPresenter(IViewFactory viewFactory,
            IPresenterNavigator navigator,
            IDiagnosticsProvider diagnosticsProvider) : base(viewFactory, navigator)
        {
            _diagnosticsProvider = diagnosticsProvider;
        }
        protected override void Subscribe()
        {
            OnPerfomanceInfoChanged(_diagnosticsProvider.PerfomanceInfo);
            _diagnosticsProvider.PerfomanceInfoChanged += OnPerfomanceInfoChanged;
        }
        protected override void Unsubscribe()
        {
            _diagnosticsProvider.PerfomanceInfoChanged -= OnPerfomanceInfoChanged;
        }

        private readonly IDiagnosticsProvider _diagnosticsProvider;
        private void OnPerfomanceInfoChanged(PerfomanceInfo perfomanceInfo)
        {
            View.FpsText = $"FpsAvg: {perfomanceInfo.FpsAverage:0.0}";
        }
    }
}