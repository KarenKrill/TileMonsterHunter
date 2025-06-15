using System.Collections.Generic;
using UnityEngine;
using Zenject;

using KarenKrill.StateSystem.Abstractions;
using KarenKrill.UI.Presenters.Abstractions;
using KarenKrill.StateSystem;
using KarenKrill.UI.Presenters;
using KarenKrill.UI.Views;
using KarenKrill.Logging;
using KarenKrill.Diagnostics;
using KarenKrill.Utilities;

namespace TileMonsterHunter
{
    using Abstractions;
    using Input.Abstractions;
    using Input;
    using UnityEngine.UI;

    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            InstallSettings();
            Container.BindInterfacesAndSelfTo<PlayerProfileProvider>().FromNew().AsSingle();
            Container.Bind<IInputActionService>().To<InputActionService>().FromNew().AsSingle();
#if DEBUG
            Container.Bind<ILogger>().To<Logger>().FromNew().AsSingle().WithArguments(new DebugLogHandler());
#else
            Container.Bind<ILogger>().To<StubLogger>().FromNew().AsSingle();
#endif
            Container.BindInterfacesAndSelfTo<GameFlow>().AsSingle();
            Container.BindInterfacesAndSelfTo<EffectManagerProvider>().AsSingle();
            InstallGameStateMachine();
            InstallViewFactory();
            Container.BindInterfacesAndSelfTo<DiagnosticsProvider>().FromInstance(_diagnosticsProvider).AsSingle();
            InstallPresenterBindings();
            Container.BindInterfacesAndSelfTo<TileField>().AsSingle();
            Container.BindInterfacesAndSelfTo<TileHotBar>().AsSingle();
            Container.BindInterfacesAndSelfTo<TileRepository>().FromInstance(_tileRepository).AsSingle();
            Container.BindInterfacesAndSelfTo<TileFieldSpawner>().FromComponentInHierarchy(true).AsSingle();
        }

        [SerializeField]
        GameObject _uiRoot;
        [SerializeField]
        List<GameObject> _uiPrefabs;
        [SerializeField]
        DiagnosticsProvider _diagnosticsProvider;
        [SerializeField]
        TextAsset _storyInkJson;
        [SerializeField]
        TileRepository _tileRepository;
        private void InstallSettings()
        {
            var showFps = PlayerPrefs.GetInt("Settings.Diagnostic.ShowFps", 0);
            GameSettings gameSettings = new(showFps != 0);
            Container.Bind<GameSettings>().To<GameSettings>().FromInstance(gameSettings);
        }
        private void InstallGameStateMachine()
        {
            Container.Bind<IStateMachine<GameState>>()
                .To<StateMachine<GameState>>()
                .AsSingle()
                .WithArguments(new GameStateGraph())
                .OnInstantiated((context, instance) =>
                {
                    if (instance is IStateMachine<GameState> stateMachine)
                    {
                        context.Container.Bind<IStateSwitcher<GameState>>().FromInstance(stateMachine.StateSwitcher);
                    }
                })
                .NonLazy();
            var stateTypes = ReflectionUtilities.GetInheritorTypes(typeof(IStateHandler<GameState>));
            foreach (var stateType in stateTypes)
            {
                Container.BindInterfacesTo(stateType).AsSingle();
            }
            Container.BindInterfacesTo<ManagedStateMachine<GameState>>().AsSingle().OnInstantiated((context, target) =>
            {
                if (target is ManagedStateMachine<GameState> managedStateMachine)
                {
                    managedStateMachine.Start();
                }
            }).NonLazy();
        }
        private void InstallViewFactory()
        {
            if (_uiRoot == null)
            {
                var uiRootCanvas = FindFirstObjectByType<Canvas>(FindObjectsInactive.Exclude);
                if (uiRootCanvas == null)
                {
                    var canvasGO = new GameObject(nameof(Canvas));
                    uiRootCanvas = canvasGO.AddComponent<Canvas>();
                    uiRootCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
                    canvasGO.AddComponent<CanvasScaler>();
                    canvasGO.AddComponent<GraphicRaycaster>();
                }
                _uiRoot = uiRootCanvas.gameObject;
            }
            Container.BindInterfacesAndSelfTo<ViewFactory>().AsSingle().WithArguments(_uiRoot, _uiPrefabs);
        }
        private void InstallPresenterBindings()
        {
            Container.BindInterfacesAndSelfTo<PresenterNavigator>().AsTransient();
            var presenterTypes = ReflectionUtilities.GetInheritorTypes(typeof(IPresenter));
            foreach (var presenterType in presenterTypes)
            {
                Container.BindInterfacesTo(presenterType).FromNew().AsSingle();
            }
        }
    }
}