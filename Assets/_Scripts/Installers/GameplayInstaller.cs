using _Scripts.Player;
using UnityEngine;
using Zenject;

namespace _Scripts.Installers
{
    public class GameplayInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindTarget();
        }

        private void BindTarget()
        {
            Container
                .Bind<ITarget>()
                .To<PlayerController>()
                .FromComponentInHierarchy()
                .AsSingle()
                .NonLazy();
        }
    }
}