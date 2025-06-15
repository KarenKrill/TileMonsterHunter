namespace TileMonsterHunter.Abstractions
{
    public enum EffectType
    {
        Hourglass
    }
    public interface IEffectManager
    {
        void StartEffect(EffectType effectType);
        void StopEffect(EffectType effectType);
    }
    public interface IEffectManagerProvider
    {
        IEffectManager Current { get; }
    }
}
