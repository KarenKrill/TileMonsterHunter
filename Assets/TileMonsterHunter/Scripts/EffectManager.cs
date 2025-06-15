using UnityEngine;

namespace TileMonsterHunter
{
    using Abstractions;
    using System.Collections.Generic;

    public class EffectManager : MonoBehaviour, IEffectManager
    {
        public void StartEffect(EffectType effectType)
        {
            _effectsToShow.Enqueue(effectType);
        }
        public void StopEffect(EffectType effectType)
        {
            _effectsToHide.Enqueue(effectType);
        }

        [SerializeField]
        private GameObject _hourglassEffect;
        private readonly Queue<EffectType> _effectsToShow = new();
        private readonly Queue<EffectType> _effectsToHide = new();
        void Update()
        {
            while(_effectsToShow.Count > 0)
            {
                var effectType = _effectsToShow.Dequeue();
                switch (effectType)
                {
                    case EffectType.Hourglass:
                        _hourglassEffect.SetActive(true);
                        break;
                    default:
                        break;
                }
            }
            while (_effectsToHide.Count > 0)
            {
                var effectType = _effectsToHide.Dequeue();
                switch (effectType)
                {
                    case EffectType.Hourglass:
                        _hourglassEffect.SetActive(false);
                        break;
                    default:
                        break;
                }
            }
        }
    }

    public class EffectManagerProvider: IEffectManagerProvider
    {
        public IEffectManager Current
        {
            get
            {
                return Object.FindFirstObjectByType<EffectManager>(FindObjectsInactive.Include);
            }
        }
    }
}
