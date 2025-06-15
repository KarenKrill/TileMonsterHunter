#nullable enable

using System;

using KarenKrill.UI.Views.Abstractions;

namespace TileMonsterHunter.UI.Views.Abstractions
{
    public interface ISettingsMenuView : IView
    {
        #region Diagnostic
        bool ShowFps { get; set; }
        #endregion

        event Action? ApplyRequested;
        event Action? CancelRequested;
    }
}