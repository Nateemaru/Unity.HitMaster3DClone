using System;
using _Scripts.Services.AudioSystem;
using _Scripts.Services.CoroutineRunnerService;
using _Scripts.Services.Database;
using _Scripts.Services.PauseHandlerService;
using _Scripts.Services.SceneLoadService;
using _Scripts.UI;
using UnityEngine;
using Zenject;

namespace _Scripts.Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _fader;

        public override void InstallBindings()
        {
            BindFPSUnlocker();
            BindCoroutineStarter();
            BindSceneLoadService();
            BindStorage();
            BindDataReader();
            BindAudioController();
            BindPauseHandler();
            BindFadeScreen();
        }

        private void BindFadeScreen()
        {
            Container
                .BindInterfacesAndSelfTo<IFadeScreen>()
                .FromComponentsInNewPrefab(_fader)
                .AsSingle()
                .NonLazy();
        }

        private void BindPauseHandler()
        {
            Container
                .Bind<PauseHandler>()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }

        private void BindAudioController()
        {
            Container
                .Bind<AudioController>()
                .AsSingle()
                .NonLazy();
        }

        private void BindStorage()
        {
            Container
            .Bind<IStorageService>()
            .To<JsonToFileStorage>()
            .FromNew()
            .AsSingle()
            .NonLazy();
        }

        private void BindDataReader()
        {
            Container
            .Bind<IDataReader>()
            .To<DataReader>()
            .FromNew()
            .AsSingle()
            .NonLazy();
        }

        private void BindSceneLoadService()
        {
            Container
                .Bind<ISceneLoadService>()
                .To<SceneLoader>()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }

        private void BindCoroutineStarter()
        {
            Container
            .Bind<ICoroutineRunner>()
            .To<CoroutineRunner>()
            .FromNewComponentOnNewGameObject()
            .AsSingle()
            .NonLazy();
        }

        private void BindFPSUnlocker()
        {
            Container
                .BindInterfacesAndSelfTo<FPSUnlocker>()
                .AsSingle()
                .NonLazy();
        }
    }
}
