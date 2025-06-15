#nullable enable

using KarenKrill.UI.Views.Abstractions;

namespace TileMonsterHunter.UI.Views.Abstractions
{
    public interface IDiagnosticInfoView : IView
    {
        public string FpsText { set; }
    }
}