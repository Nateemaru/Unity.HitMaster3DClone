using _Scripts.Services;
using Zenject;

namespace _Scripts.Installers
{
    public class BootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindFPSUnlocker();
            BindCoroutineStarter();
            BindSceneLoader();
        }

        private void BindSceneLoader()
        {
            Container
                .Bind<SceneLoader>()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }

        private void BindCoroutineStarter()
        {
            Container
            .Bind<CoroutineStarter>()
            .FromNewComponentOnNewGameObject()
            .AsSingle()
            .NonLazy();
        }

        private void BindFPSUnlocker()
        {
            Container
                .Bind<FPSUnlocker>()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }
    }
}
