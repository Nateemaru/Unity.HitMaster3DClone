using _Scripts.UI.UIInfrastructure;
using Zenject;

namespace _Scripts.Installers
{
    public class UIInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindSettingsViewController();
            BindWinScreenController();
            BindLoseScreenController();
            BindLoadSceneButtonController();
            BindLevelProgressController();
        }

        private void BindLevelProgressController()
        {
            Container
                .Bind<LevelProgressController>()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }

        private void BindLoadSceneButtonController()
        {
            Container
                .Bind<LoadSceneButtonController>()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }

        private void BindWinScreenController()
        {
            Container
                .Bind<WinScreenController>()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }

        private void BindLoseScreenController()
        {
            Container
                .Bind<LoseScreenController>()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }

        private void BindSettingsViewController()
        {
            Container
                .Bind<SettingsScreenController>()
                .FromNew()
                .AsSingle()
                .NonLazy();
        }
    }
}