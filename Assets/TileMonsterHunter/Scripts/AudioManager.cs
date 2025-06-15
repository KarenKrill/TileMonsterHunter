using KarenKrill.StateSystem.Abstractions;
using System;
using System.Collections;
using TileMonsterHunter.Abstractions;
using UnityEngine;
using UnityEngine.Audio;
using Zenject;

namespace TileMonsterHunter
{
    public class AudioManager : MonoBehaviour
    {
        [Inject]
        public void Initialize(ITileHotBar tileHotBar, IStateMachine<GameState> gameStateMachine)
        {
            _tileHotBar = tileHotBar;
            _gameStateMachine = gameStateMachine;
        }

        [SerializeField]
        private AudioSource _musicAudioSource;
        [SerializeField]
        private AudioSource _sfxAudioSource;
        [SerializeField]
        private AudioClip _mainMenuTheme;
        [SerializeField]
        private AudioClip _combatTheme;
        [SerializeField]
        private AudioClip _tilesMatchSound;
        [SerializeField]
        private AudioClip _tileClickSound;

        private ITileHotBar _tileHotBar;
        private IStateMachine<GameState> _gameStateMachine;
        private void OnEnable()
        {
            _tileHotBar.TileAdded += OnHotBarTileAdded;
            _tileHotBar.TilesRemoved += OnHotBarTilesRemoved;
            _gameStateMachine.StateEnter += OnGameStateEnter;
            _gameStateMachine.StateExit += OnGameStateExit;
        }
        private void OnDisable()
        {
            _tileHotBar.TileAdded -= OnHotBarTileAdded;
            _tileHotBar.TilesRemoved -= OnHotBarTilesRemoved;
            _gameStateMachine.StateEnter -= OnGameStateEnter;
            _gameStateMachine.StateExit -= OnGameStateExit;
        }
        private void OnHotBarTileAdded(TileInfo obj)
        {
            if (_sfxAudioSource.isPlaying)
            {
                _sfxAudioSource.Stop();
            }
            _sfxAudioSource.PlayOneShot(_tileClickSound);
        }
        private void OnHotBarTilesRemoved(TileInfo[] obj)
        {
            if (_sfxAudioSource.isPlaying)
            {
                _sfxAudioSource.Stop();
            }
            _sfxAudioSource.PlayOneShot(_tilesMatchSound);
        }
        private void OnGameStateEnter(GameState fromState, GameState toState)
        {
            if (toState == GameState.MainMenu)
            {
                StopAllCoroutines();
                StartCoroutine(SwitchMusicCoroutine(_mainMenuTheme));
            }
            else if (toState == GameState.Gameplay)
            {
                StopAllCoroutines();
                StartCoroutine(SwitchMusicCoroutine(_combatTheme));
            }
        }
        private void OnGameStateExit(GameState fromState, GameState toState)
        {
            if (fromState == GameState.MainMenu || fromState == GameState.Gameplay)
            {
                StopAllCoroutines();
                StartCoroutine(SwitchMusicCoroutine(null));
            }
        }
        private IEnumerator SwitchMusicCoroutine(AudioClip nextClip)
        {
            if (_musicAudioSource.isPlaying)
            {
                yield return FadeOutCoroutine(5);
            }
            if (nextClip != null)
            {
                _musicAudioSource.clip = nextClip;
                yield return FadeInCoroutine(5, .7f);
            }
        }
        private IEnumerator FadeOutCoroutine(float fadeDuration)
        {
            Debug.Log("FadeOut");
            float startVolume = _musicAudioSource.volume;
            float timeElapsed = 0f;
            while (timeElapsed < fadeDuration)
            {
                timeElapsed += Time.deltaTime;
                float t = timeElapsed / fadeDuration;
                _musicAudioSource.volume = Mathf.Lerp(startVolume, 0f, t);
                yield return null;
            }
            _musicAudioSource.volume = 0f;
            _musicAudioSource.Stop();
        }
        private IEnumerator FadeInCoroutine(float fadeDuration, float targetVolume)
        {
            Debug.Log("FadeIn");
            _musicAudioSource.volume = 0f;
            _musicAudioSource.Play();
            float timeElapsed = 0f;
            while (timeElapsed < fadeDuration)
            {
                timeElapsed += Time.deltaTime;
                float t = timeElapsed / fadeDuration;
                _musicAudioSource.volume = Mathf.Lerp(0f, targetVolume, t);
                yield return null;
            }
            _musicAudioSource.volume = targetVolume;
        }
    }
}
