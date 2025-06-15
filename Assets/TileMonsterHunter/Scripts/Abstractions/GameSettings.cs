using System;

namespace TileMonsterHunter.Abstractions
{
    public class GameSettings
    {
        #region Diagnostic

        public bool ShowFps
        {
            get => _showFps;
            set
            {
                if (_showFps != value)
                {
                    _showFps = value;
                    ShowFpsChanged?.Invoke(_showFps);
                }
            }
        }

        #endregion

#nullable enable

        public event Action<bool>? ShowFpsChanged;

#nullable restore

        public GameSettings(bool showFps = true)
        {
            _showFps = showFps;
        }

        private bool _showFps;
    }
}
