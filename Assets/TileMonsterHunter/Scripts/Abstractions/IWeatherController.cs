using System;

namespace TileMonsterHunter.Abstractions
{
    public enum WeatherType
    {
        None,
        Dry,
        Rain
    }
    public interface IWeatherController
    {
        event Action LightningStrikeEnded;
        WeatherType Type { get; set; }
        void LightningStrike();
    }
}
